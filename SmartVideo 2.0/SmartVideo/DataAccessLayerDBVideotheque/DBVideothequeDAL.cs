using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOLibrary;
using System.Data.Linq;

namespace DataAccessLayerDBVideotheque
{
    public class DBVideothequeDAL
    {
        private DBVideothequeDataContext instanceDC = null;
        private static DBVideothequeDAL singleton;
        public static DBVideothequeDAL Singleton(String servername, String dbname)
        {
            return singleton ?? (singleton = new DBVideothequeDAL(servername, dbname));
        }
        /*instanceDC = new DBVideothequeDataContextDataContext());*/
        /* */
        private DBVideothequeDAL(String servername, String dbname)
        {
            if (dbname == null || dbname == "")
                instanceDC = new DBVideothequeDataContext();
            else
            {
                String connectionstring = "Data Source = " + servername + " ; Initial Catalog =" + dbname + "; Integrated Security = True";
                instanceDC = new DBVideothequeDataContext(connectionstring);
                
            }
            if (!instanceDC.DatabaseExists())  // Vérifier si la DB existe
                instanceDC.CreateDatabase();   // Si elle n'existe pas, on la crée
        }
        public List<FilmDTO> SelectAllFilms()
        {
            if (instanceDC == null)
                throw new Exception("DAL not connected");

            List<Film> data = instanceDC.Films.ToList();
            List<FilmDTO> listBLL = new List<FilmDTO>();
            

            foreach (var d in data)
            {
                listBLL.Add(toFilmDTO(d));
            }
            
            return listBLL;

        }

        public void SetStatus(int id, string v)
        {
            Requete a = instanceDC.Requetes.Where(d => d.film == id).SingleOrDefault();
            if(a == null)
            {
                a = new Requete
                {
                    film = id,
                    status = v

                };
                instanceDC.Requetes.InsertOnSubmit(a);
            }
            else
            {
                a.status = v;
            }
            instanceDC.SubmitChanges();

        }
        public string GetStatus(int id)
        {
            Requete r =  instanceDC.Requetes.Where(d => d.film == id).SingleOrDefault();
            if (r == null)
                return null;
            else
                return r.status;
        }
        public List<RequeteDTO> SelectRequeteByStatus(string status)
        {
            if (instanceDC == null)
                throw new Exception("DAL not connected");

            List<Requete> data = instanceDC.Requetes.Where(d => d.status == status).ToList();
            List<RequeteDTO> listBLL = new List<RequeteDTO>();
            foreach (var d in data)
            {
                listBLL.Add(toRequeteDTO(d));
            }
            Console.WriteLine(status +" : " + listBLL.Count);
            
            return listBLL;
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
        public List<FilmDTO> getStock()
        {
            List<Film> list= (from R in instanceDC.Requetes
                            join F in instanceDC.Films
                                on R.film equals F.id
                                where R.status == "Film_Owned"
                            select F).ToList();
            //return list;
            List<FilmDTO> listdto = new List<FilmDTO>();
            foreach (var item in list)
            {
                listdto.Add(toFilmDTO(item));
            }
            return listdto;
        }

        public  Film SelectFilmById(int id)
        {
            return instanceDC.Films.Where(d => d.id == id).SingleOrDefault();
        }

        /* Posts et Infos */
        public List<PostDTO> SelectAllArticles()
        {
            List<Post> posts = instanceDC.Posts.OrderBy(d => d.id).ToList();
            List<PostDTO> listdto = new List<PostDTO>();
            foreach (var item in posts)
            {
                listdto.Add(toPostDTO(item));
            }
            return listdto;
        }
        public Post SelectArticleForId(int id)
        {
            return instanceDC.Posts.Where(d => d.id == id).SingleOrDefault();
        }
        public  Info SelectInfoForKey(string key)
        {
            return instanceDC.Infos.Where(d => d.clef == key).SingleOrDefault();
        }
        public List<Info> SelectAllInfos()
        {
            return instanceDC.Infos.OrderBy(d => d.id).ToList();
        }
        public void AddRequete(int idFilm)
        {

            if (instanceDC.Requetes.Where(d => d.film == idFilm).Count() > 0)
                throw new Exception("Une requete pour ce film est déjà en cours");
            
            Requete a = new Requete
            {
                film = idFilm,
                status = "Request_Film_Created"
            };

            instanceDC.Requetes.InsertOnSubmit(a);
            instanceDC.SubmitChanges();
            
        }
        public void RemovePost(int id)
        {
            Post p = instanceDC.Posts.Where(po => po.id == id).Single();
            instanceDC.Posts.DeleteOnSubmit(p);
            instanceDC.SubmitChanges();

        }
        public PostDTO AddPost(string title, string content)
        {
            Post a = new Post
            {
                titre = title,
                contenu = content,
                date_publication = DateTime.Now
            };

            instanceDC.Posts.InsertOnSubmit(a);
            instanceDC.SubmitChanges();
            return new PostDTO() { Id = a.id, Contenu = a.contenu, Date = (DateTime)a.date_publication, Titre = a.titre };
        }
        public void AddInfo(string c, string v)
        {
            Info i = new Info
            {
                clef = c,
                valeur = v,
            };
            instanceDC.Infos.InsertOnSubmit(i);
            instanceDC.SubmitChanges();
        }
        public PostDTO UpdatePost(int id, string t, string c)
        {
            Post a = instanceDC.Posts.Where(d => d.id == id).SingleOrDefault();
            
            a.titre = t;
            Console.WriteLine(t);
            Console.WriteLine(c);

            a.contenu = c;
            
            instanceDC.SubmitChanges();
            return new PostDTO() { Id = a.id, Contenu = a.contenu, Date = (DateTime)a.date_publication, Titre = a.titre };

        }
        public bool Add<T>(T rec, Func<T, bool> expr) where T : class
        {
            if (instanceDC == null)
                throw new Exception("DAL not connected");
            try
            {
                IQueryable<T> query = ((Table<T>)instanceDC.GetType().GetProperty(typeof(T).Name + "s").GetValue(instanceDC));
                if (!query.Where(expr).Any())
                {
                    ((Table<T>)instanceDC.GetType().GetProperty(typeof(T).Name + "s").GetValue(instanceDC)).InsertOnSubmit(rec);
                    instanceDC.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddFilm(FilmDTO f)
        {
            Film fi = new Film
            {
                id = f.id,
                title = f.titre,
                original_title = f.original_title,
                runtime = f.runtime,
                posterpath = f.poster_path
            };

            if (Add<Film>(fi, d => d.id == fi.id))
            {
                foreach (var actor in f.actors)
                    addRelationActor(actor, f.id);

                foreach (var genre in f.genres)
                    addRelationGenre(genre, f.id);

                foreach (var realisator in f.realisateurs)
                    addRelationRealisateur(realisator, f.id);

                return true;
            }
            return false;
            
        }
        public bool addRelationActor(ActorDTO a, int idFilm)
        {
            Actor ac = new Actor
            {
                id = a.id,
                name = a.name,
                character = a.character

               
            };
            Add<Actor>(ac, d => d.id == ac.id);

            FilmActor fa = new FilmActor
            {
                id_film = idFilm,
                id_actor = ac.id
            };
            return Add<FilmActor>(fa, d => d.id_film == idFilm && d.id_actor == ac.id);
            
        }
        public bool addRelationGenre(GenreDTO a, int idFilm)
        {
            Genre ac = new Genre
            {
                id = a.id,
                name = a.Name
            };
            Add<Genre>(ac, d => d.id == ac.id);

            FilmGenre fa = new FilmGenre
            {
                id_film = idFilm,
                id_genre = ac.id
            };
            return Add<FilmGenre>(fa, d => d.id_film == idFilm && d.id_genre == ac.id);
        }

        public bool addRelationRealisateur(RealisateurDTO a, int idFilm)
        {
            Realisateur ac = new Realisateur
            {
                id = a.id,
                name = a.Name
            };
            Add<Realisateur>(ac, d => d.id == ac.id);

            FilmRealisateur fa = new FilmRealisateur
            {
                id_film = idFilm,
                id_realisateur = ac.id
            };
            return Add<FilmRealisateur>(fa, d => d.id_film == idFilm && d.id_realisateur == ac.id);
        }

        private FilmDTO toFilmDTO(Film f)
        {
            FilmDTO fDTO = new FilmDTO();
            fDTO.id = f.id;
            fDTO.titre = f.title;
            fDTO.original_title = f.original_title;
            fDTO.runtime = (int)f.runtime;
            fDTO.poster_path = f.posterpath;
            //fDTO.available = Convert.ToBoolean(f.available);

            return fDTO;
        }
        private PostDTO toPostDTO(Post p)
        {
            PostDTO pd = new PostDTO();
            pd.Id = p.id;
            pd.Titre = p.titre;
            pd.Contenu = p.contenu;
            pd.Date = (DateTime)p.date_publication;
            return pd;
        }
        private RequeteDTO toRequeteDTO(Requete r)
        {
            RequeteDTO rd = new RequeteDTO();
            rd.id = r.id;
            rd.idFilm = r.film;
            rd.status = r.status;
            return rd;
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
