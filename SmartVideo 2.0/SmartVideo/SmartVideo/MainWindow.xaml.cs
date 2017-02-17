using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DTOLibrary;
using BusinessLogicLayer;
using System.Collections.ObjectModel;

namespace SmartVideo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private int loaded = 0;
        ServiceReference.ServiceWCFSmartClient clientService;
        ObservableCollection<FilmDTO> listDBVideo;
        ObservableCollection<FilmDTO> listDBFilms;
        public MainWindow()
        {
            RegistryHelper registry = new RegistryHelper();
            if (registry.Read("dbHost") == null || registry.Read("dbName") == null || registry.Read("dbInstance") == null)
            {
                SettingsWindow sw = new SettingsWindow();
                sw.ShowDialog();
            }

            InitializeComponent();
            
            listDBFilms = new ObservableCollection<FilmDTO>();
            listDBVideo = new ObservableCollection<FilmDTO>();

            clientService = new ServiceReference.ServiceWCFSmartClient();
            DBFilmsLV.ItemsSource = listDBFilms;
            DBVideothequeLV.ItemsSource = listDBVideo;
        }

        private void loadDBFilm(object sender, RoutedEventArgs e)
        {
            List<FilmDTO> Films = clientService.GetFilmsPage(loaded).ToList();
            Films.ForEach(listDBFilms.Add);
            
            loaded++;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            clientService.Close();
        }


        private void requestFilm(object sender, RoutedEventArgs e)
        {
            if(DBFilmsLV.SelectedItem == null)
            {
                MessageBox.Show("Vous devez sélectionner un film dans la liste des films disponibles", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            FilmDTO selected = (FilmDTO)DBFilmsLV.SelectedItem;
            //Console.WriteLine(selected);
            try
            {
                BLLVideotheque.setRequest(selected.id);
                MessageBox.Show("La requete à bien été effectuée.");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void loadDBVideo(object sender, RoutedEventArgs e)
        {
            List<FilmDTO> film = BusinessLogicLayer.BLLVideotheque.getStock();
            if (film.Count() == 0)
                MessageBox.Show("Il n'y a aucun film dans la videothque");
            else
            {
                Console.WriteLine("test");
                listDBVideo.Clear();
                film.ForEach(listDBVideo.Add);
            }

        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            RequestsWindow OP = new RequestsWindow();
            OP.Show();
        }

        private void disposeFilm(object sender, RoutedEventArgs e)
        {
            if (DBVideothequeLV.SelectedItem == null)
            {
                MessageBox.Show("Vous devez sélectionner un film dans la liste des films disponibles dans la videotheque", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            FilmDTO selected = (FilmDTO)DBVideothequeLV.SelectedItem;
            //Console.WriteLine(selected);
           try
            {
                BLLVideotheque.setWaitingDisposal(selected.id);
                MessageBox.Show("Le film est en attente de renvoi au dépot.");
                listDBVideo.Remove(DBVideothequeLV.SelectedItem as FilmDTO);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NewsWindows nw = new NewsWindows();
            nw.Show();
        }

        private void DBFilmsLV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(sender);
            ListView lw = sender as ListView;
            if(lw != null && lw.SelectedItem != null)
            {
                InfosWindows iw = new InfosWindows(lw.SelectedItem as FilmDTO, clientService);
                iw.Show();
            }
        }

        private void reloadDBFilm_Click(object sender, RoutedEventArgs e)
        {
            listDBFilms.Clear();
            loaded = 0;

        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow sw = new SearchWindow(this);
            sw.Show();
        }
        public ServiceReference.ServiceWCFSmartClient getClientWFC()
        {
            return clientService;
        }
        public void setFilms(List<FilmDTO> films)
        {
            listDBFilms.Clear();
            films.ForEach(listDBFilms.Add);
            loaded = 0;
        }
    }
}
