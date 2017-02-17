using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DTOLibrary;
namespace SmartSynchro
{
    public partial class Service1 : ServiceBase
    {
        private System.Timers.Timer timer;
        private ServiceReference.ServiceWCFSmartClient clientService;
        
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Démarrage de mon service", EventLogEntryType.Information);

            this.timer = new System.Timers.Timer(10000D);  // 30000 milliseconds = 30 seconds
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            this.timer.Enabled = true;
            this.timer.Start();
            //mer.Start();

        }
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            //EventLog.WriteEntry("Timer ", EventLogEntryType.Information);
            Console.WriteLine("\nLet's go!");
            clientService = new ServiceReference.ServiceWCFSmartClient();

            this.requestFilms();
            this.disposeFilms();

            clientService.Close();

        }

        private void disposeFilms()
        {
            List<RequeteDTO> listRequete = new List<RequeteDTO>();
            listRequete = BLLVideotheque.getAllWaintingDisposal().ToList();
            Console.WriteLine("Nombre d'éléments à renvoyer : " + listRequete.Count);

            foreach (RequeteDTO item in listRequete)
            {
                Console.WriteLine("\tRenvoie de : " + item.idFilm);
                try
                {
                    clientService.RetourFilm(item.idFilm);
                    BLLVideotheque.setDisposed(item.idFilm);
                    Console.WriteLine("\tFilm " + item.idFilm + " renvoyé");

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Une erreur est survenue : " + ex.ToString());
                }

            }
        }

        private void requestFilms()
        {
            List<RequeteDTO> listRequete = new List<RequeteDTO>();
            listRequete = BLLVideotheque.getAllRequest().Concat(BLLVideotheque.getAllWaiting()).ToList();

            FilmDTO tmpFilm;
            Console.WriteLine("Nombre d'éléments à demander : " + listRequete.Count);

            foreach (RequeteDTO item in listRequete)
            {
                Console.WriteLine("\tTentative pour : " + item.idFilm);
                if (clientService.ReserveFilm(item.idFilm))
                {
                    tmpFilm = clientService.GetFilmInfo(item.idFilm);
                    BLLVideotheque.setOwned(item.idFilm);

                    if (BLLVideotheque.saveFilm(tmpFilm))
                        Console.WriteLine("\t\tLe film a été enregistré");
                    else
                        Console.WriteLine("\t\tLe film est déjà dans la base de données");

                    EventLog.WriteEntry("Le film " + item.idFilm + "est disponible et a été réservé.", EventLogEntryType.Information);
                    Console.WriteLine("\tLe film est disponible et a été réservé.");
                }
                else
                {
                    EventLog.WriteEntry("Le film " + item.idFilm + "n'est pas disponible et est en attente.", EventLogEntryType.Information);
                    Console.WriteLine("\tLe film n'est pas disponible et n'a pas été réservé.");

                    BLLVideotheque.setWaiting(item.idFilm);
                }
            }
        }


        protected override void OnStop()
        {
            EventLog.WriteEntry("Arrêt de mon service", EventLogEntryType.Information);
        }
        internal void TestStartupAndStop()
        {
            this.OnStart(null);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
