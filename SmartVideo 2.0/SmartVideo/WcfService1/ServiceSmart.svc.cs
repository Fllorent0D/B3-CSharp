using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DTOLibrary;
using BusinessLogicLayer;


namespace ServiceWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceSmart : IServiceWCFSmart
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public List<FilmDTO> GetFilmsPage(int page)
        {
            return BLLFilm.getFilmPage(page);
        }
        public List<FilmDTO> GetAllFilmsDBFilm()
        {
            return BLLFilm.getAllFilms();   
        }
        public Boolean ReserveFilm(int id)
        {
            return BLLFilm.reserveFilm(id);
        }

        public FilmDTO GetFilmInfo(int id)
        {
            return BLLFilm.getFilmInfo(id);
        }
       
        public void RetourFilm(int id)
        {
            BLLFilm.RetourFilm(id) ;
        }
        public List<FilmDTO> rechercherFilms(string table, string critere)
        {
            return BLLFilm.rechercheFilms(table, critere);
        }
        public List<ActorDTO> GetAllActors()
        {
            return BLLFilm.getAllActors();
        }

        public List<GenreDTO> GetAllGenres()
        {
            return BLLFilm.getAllGenres();
        }
        public List<RealisateurDTO> GetAllRealisators()
        {
            return BLLFilm.getAllRealisators();

        }
    }
}
