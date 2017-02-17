using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOLibrary;
using System.Data.Linq;


namespace DataAccessLayerDBFilm
{
    public class DBFilmDAL
    {
        private DBFilmDataContext instanceDC = null;
        private static DBFilmDAL singleton;
        public static DBFilmDAL Singleton(String servername, String dbname)
        {
            return singleton ?? (singleton = new DBFilmDAL(servername, dbname));
        }
        /*instanceDC = new DBVideothequeDataContextDataContext());*/
        /* */
        private DBFilmDAL(String servername, String dbname)
        {
            if (dbname == null || dbname == "")
                instanceDC = new DBFilmDataContext();
            else
            {
                String connectionstring = "Data Source = " + servername + " ; Initial Catalog =" + dbname + "; Integrated Security = True";
                instanceDC = new DBFilmDataContext(connectionstring);

            }
            if (!instanceDC.DatabaseExists())  // Vérifier si la DB existe
                instanceDC.CreateDatabase();   // Si elle n'existe pas, on la crée
        }

        public void reserveFilm(int id)
        {
            Film a = instanceDC.Films.Where(d => d.id == id).SingleOrDefault();
            a.available = 0;
            instanceDC.SubmitChanges();
        }

        public void retourFilm(int id)
        {
            Film a = instanceDC.Films.Where(d => d.id == id).SingleOrDefault();
            a.available = 1;
            instanceDC.SubmitChanges();
        }

        public List<FilmDTO> SelectAllFilms()
        {
            List<Film> data = instanceDC.Films.OrderBy(d => d.id).Take(50).ToList();

            List<FilmDTO> listBLL = new List<FilmDTO>();
            foreach (var d in data)
            {
                FilmDTO fDTO = new FilmDTO();
                fDTO.id = d.id;
                fDTO.titre = d.title;
                fDTO.original_title = d.original_title;
                fDTO.runtime = (int)d.runtime;
                fDTO.poster_path = d.posterpath;
                fDTO.available = Convert.ToBoolean(d.available);
                listBLL.Add(fDTO);
            }
            return listBLL;
        }

        public Boolean filmAvailability(int id)
        {
            FilmDTO f = SelectFilmById(id);
            return f.available;
        }


        public List<FilmDTO> SelectFilmsPage(int page)
        {
            List<Film> data = instanceDC.Films.OrderBy(d => d.id).Skip(10*page).Take(10).ToList();

            List<FilmDTO> listBLL = new List<FilmDTO>();
            foreach (var d in data)
            {
                listBLL.Add(toFilmDTO(d));
            }

            return listBLL;
        }

        public FilmDTO SelectFilmById(int id)
        {
            return this.toFilmDTO(instanceDC.Films.Where(d => d.id == id).SingleOrDefault());
        }
        public List<FilmDTO> rechercheFilm(string table, string criteria)
        {
            List<Film> listFilm =null;
            List<FilmDTO> listDTO = new List<FilmDTO>();
            switch (table)
            {
                case "Film":
                    listFilm = (from fi in instanceDC.Films
                                where fi.title.Contains(criteria) || fi.original_title.Contains(criteria)
                                select fi).ToList();
                    break;
                case "Acteur":
                    listFilm = (from ac in instanceDC.Actors
                               join fiac in instanceDC.FilmActors on ac.id equals fiac.id_actor
                               join fi in instanceDC.Films on fiac.id_film equals fi.id
                               where ac.name.Contains(criteria)
                               select fi).ToList();

                    break;
                case "Genre":
                    listFilm = (from ac in instanceDC.Genres
                                join fiac in instanceDC.FilmGenres on ac.id equals fiac.id_genre
                                join fi in instanceDC.Films on fiac.id_film equals fi.id
                                where ac.name.Contains(criteria)

                                select fi).ToList();

                    break;
                case "Réalisateur":
                    listFilm = (from ac in instanceDC.Realisateurs
                                join fiac in instanceDC.FilmRealisateurs on ac.id equals fiac.id_realisateur
                                join fi in instanceDC.Films on fiac.id_film equals fi.id
                                where ac.name.Contains(criteria)
                                select fi).ToList();
                    break;
            }

            foreach (Film item in listFilm)
                listDTO.Add(toFilmDTO(item));

            return listDTO;

        }
        public List<GenreDTO> SelectGenreForFilm(int id)
        {
            List<Genre> listRelaGenre = (from G in instanceDC.Genres
                                            join R in instanceDC.FilmGenres
                                                on G.id equals R.id_genre
                                            join F in instanceDC.Films
                                                on R.id_film equals F.id
                                         where F.id == id
                                         select G).ToList();
            List<GenreDTO> result = new List<GenreDTO>();
            foreach (Genre item in listRelaGenre)
                result.Add(toGenreDTO(item));

            return result;
        }
        public List<ActorDTO> SelectActorsForFilm(int id)
        {
            List<Actor> listRelaAc = (from A in instanceDC.Actors
                                         join R in instanceDC.FilmActors
                                             on A.id equals R.id_actor
                                         join F in instanceDC.Films
                                             on R.id_film equals F.id
                                         where R.id_film == id
                                         select A).ToList();
            List<ActorDTO> result = new List<ActorDTO>();
            foreach (Actor item in listRelaAc)
                result.Add(toActorDTO(item));

            return result;
        }
        public List<RealisateurDTO> SelectRealisatorsForFilm(int id)
        {
            List<Realisateur> listRea = (from A in instanceDC.Realisateurs
                                         join R in instanceDC.FilmRealisateurs
                                             on A.id equals R.id_realisateur
                                         join F in instanceDC.Films
                                             on R.id_film equals F.id
                                         where F.id == id
                                         select A).ToList();
            List<RealisateurDTO> result = new List<RealisateurDTO>();
            foreach (Realisateur item in listRea)
                result.Add(toRealisatorDTO(item));

            return result;
        }
        public List<GenreDTO> SelectAllGenres()
        {
            List<GenreDTO> listDTO = new List<GenreDTO>();
            var list = instanceDC.Genres.OrderBy(d => d.name).ToList();
            foreach (var item in list)
                listDTO.Add(toGenreDTO(item));
            return listDTO;
        }
        public Genre SelectGenreById(int id)
        {
            return instanceDC.Genres.Where(d => d.id == id).SingleOrDefault();
        }
        public List<ActorDTO> SelectAllActors()
        {
            List<ActorDTO> listDTO = new List<ActorDTO>();
            var list = instanceDC.Actors.OrderBy(d => d.name).ToList();
            foreach (var item in list)
                listDTO.Add(toActorDTO(item));
            return listDTO;
        }
        public Actor SelectActorById(int id)
        {
            return instanceDC.Actors.Where(d => d.id == id).SingleOrDefault();
        }
        public List<RealisateurDTO> SelectAllRealisators()
        {
            List<RealisateurDTO> listDTO = new List<RealisateurDTO>();
            var list = instanceDC.Realisateurs.OrderBy(d => d.name).ToList();
            foreach (var item in list)
                listDTO.Add(toRealisatorDTO(item));
            return listDTO;
        }
        public Realisateur SelectRealisatorById(int id)
        {
            return instanceDC.Realisateurs.Where(d => d.id == id).SingleOrDefault();
        }

        private FilmDTO toFilmDTO(Film f)
        {

            FilmDTO fDTO = new FilmDTO();
            fDTO.id = f.id;
            fDTO.titre = f.title;
            fDTO.original_title = f.original_title;
            fDTO.runtime = (int)f.runtime;
            fDTO.poster_path = f.posterpath;
            fDTO.available = Convert.ToBoolean(f.available);
            return fDTO;
        }
        private GenreDTO toGenreDTO(Genre g)
        {
            GenreDTO gdto = new GenreDTO();
            gdto.id = g.id;
            gdto.Name = g.name;
            return gdto;
        }
        private ActorDTO toActorDTO(Actor g)
        {
            ActorDTO gdto = new ActorDTO();
            gdto.id = g.id;
            gdto.name = g.name;
            gdto.character = g.character;
            return gdto;
        }
        private RealisateurDTO toRealisatorDTO(Realisateur g)
        {
            RealisateurDTO gdto = new RealisateurDTO();
            gdto.id = g.id;
            gdto.Name = g.name;
            return gdto;
        }
    }

}
