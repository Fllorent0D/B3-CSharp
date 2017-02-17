using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOLibrary;
using DataAccessLayerDBVideotheque;
namespace BusinessLogicLayer
{
    public class BLLVideotheque
    {
        private static DBVideothequeDAL _singleton = null;
        private static DBVideothequeDAL getDal()
        {
            if(_singleton == null)
            {
                //récupérer la host et db ici
                RegistryHelper registry = new RegistryHelper();

                if (registry.Read("dbHost") == null ||registry.Read("dbName" ) == null || registry.Read("dbInstance") == null)
                    throw new Exception("Les informations pour accéder à la base de données ne sont pas configurées. Merci de configurer ceci dans l'application SmartVideo.");
                
                _singleton = DBVideothequeDAL.Singleton(registry.Read("dbHost")+"\\"+ registry.Read("dbInstance"), registry.Read("dbName"));
            }

            return _singleton;
        }
        public static List<FilmDTO> getAllFilms()
        {
            
            List<FilmDTO> listDAL = getDal().SelectAllFilms();
            return listDAL;
        }
        public static void initiateRequest(int idFilm)
        {
            getDal().AddRequete(idFilm);
        }
        public static List<RequeteDTO> getAllRequest()
        {
            return getDal().SelectRequeteByStatus("Request_Film_Created");
        }
        public static List<RequeteDTO> getAllWaiting()
        {
            return getDal().SelectRequeteByStatus("Waiting_Film_Available");
        }
        public static List<RequeteDTO> getAllOwned()
        {
            return getDal().SelectRequeteByStatus("Film_Owned");
        }
        public static List<RequeteDTO> getAllWaintingDisposal()
        {
            return getDal().SelectRequeteByStatus("Request_Film_Disposal");
        }
        public static List<RequeteDTO> getAllDisposed()
        {
            return getDal().SelectRequeteByStatus("Film_Disposed");
        }
        public static void setRequest(int id)
        {
            string currentStatus = getDal().GetStatus(id);
            if(currentStatus != null)
            {
                if (currentStatus == "Request_Film_Disposal")
                {
                    getDal().SetStatus(id, "Film_Owned");
                    throw new Exception("Le film est en attente pour retourner au dépot. Il va être remis dans le stock de la videotheque.");
                }
                if (currentStatus == "Film_Owned")
                    throw new Exception("Le film est déjà dans la videotheque.");
                if (currentStatus == "Waiting_Film_Available")
                    throw new Exception("Le film n'est toujours pas disponible plusieurs requetes ont déjà été effectuées.");
                if (currentStatus == "Request_Film_Created")
                    throw new Exception("La requete a déjà été effectuée.");
            }



            getDal().SetStatus(id, "Request_Film_Created");
        }
        public static void setWaiting(int id)
        {
            getDal().SetStatus(id, "Waiting_Film_Available");
        }
        public static void setOwned(int id)
        {
            getDal().SetStatus(id, "Film_Owned");
        }
        public static void setWaitingDisposal(int id)
        {
            getDal().SetStatus(id, "Request_Film_Disposal");
        }
        public static void setDisposed(int id)
        {
            getDal().SetStatus(id, "Film_Disposed");
        }
        public static List<FilmDTO> getStock()
        {
            List<FilmDTO> films = getDal().getStock();
            foreach (var item in films)
            {
                item.actors = getDal().SelectActorsForFilm(item.id);
                item.genres = getDal().SelectGenreForFilm(item.id);
                item.realisateurs = getDal().SelectRealisatorsForFilm(item.id);

            }
            
            return films;
        }
        public static bool saveFilm(FilmDTO f)
        {
            return getDal().AddFilm(f);
        }
        public static List<RequeteDTO> getAllWaitingRequest()
        {
            return getAllWaiting().Concat(getAllWaintingDisposal()).Concat(getAllRequest()).ToList();
        }
        public static PostDTO addPost(string titre, string contenu)
        {
            return getDal().AddPost(titre, contenu);
        }
        public static PostDTO updateNews(int id, string titre, string contenu)
        {
            return getDal().UpdatePost(id, titre, contenu);
        }
        public static void deleteNews(int id)
        {
            getDal().RemovePost(id);
        }
        public static List<PostDTO> getNews()
        {
            return getDal().SelectAllArticles();
        }

    }
}
