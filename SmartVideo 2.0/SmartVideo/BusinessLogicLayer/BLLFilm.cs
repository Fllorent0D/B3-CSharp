using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOLibrary;
using DataAccessLayerDBFilm;
using DataAccessLayerDBVideotheque;

namespace BusinessLogicLayer
{
    public static class BLLFilm
    {
        private static DBFilmDAL dal = DBFilmDAL.Singleton(null, null);
        public static List<FilmDTO> getAllFilms()
        {
            List<FilmDTO> listDAL = dal.SelectAllFilms();
            return listDAL;
        }

        public static List<FilmDTO> getFilmPage(int page)
        {
            List<FilmDTO> listDal = dal.SelectFilmsPage(page);
            return listDal;
        }
        public static Boolean reserveFilm(int id)
        {
            if(dal.filmAvailability(id))
            {
                dal.reserveFilm(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static FilmDTO getFilmInfo(int id)
        {
            FilmDTO film;
            film = dal.SelectFilmById(id);
            film.actors = dal.SelectActorsForFilm(id);
            film.genres = dal.SelectGenreForFilm(id);
            film.realisateurs = dal.SelectRealisatorsForFilm(id);
            return film;
        }

        public static void RetourFilm(int id)
        {
            dal.retourFilm(id);
        }

        public static List<FilmDTO> rechercheFilms(string table, string critere)
        {
            return dal.rechercheFilm(table, critere);

        }

        public static List<ActorDTO> getAllActors()
        {
            return dal.SelectAllActors();
        }

        public static List<GenreDTO> getAllGenres()
        {
            return dal.SelectAllGenres();
        }

        public static List<RealisateurDTO> getAllRealisators()
        {
            return dal.SelectAllRealisators();
        }
    }
}
