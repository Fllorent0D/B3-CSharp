using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DTOLibrary;

namespace ServiceWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceWCFSmart
    {

        [OperationContract]
        string GetData(int value);

        /* */
        [OperationContract]
        List<FilmDTO> GetAllFilmsDBFilm();
        /* */


        [OperationContract]
        FilmDTO GetFilmInfo(int id);

        [OperationContract]
        List<FilmDTO> GetFilmsPage(int page);
        [OperationContract]
        List<FilmDTO> rechercherFilms(string table, string critere);

        [OperationContract]
        Boolean ReserveFilm(int id);

        [OperationContract]
        List<ActorDTO> GetAllActors();
        [OperationContract]
        List<GenreDTO> GetAllGenres();
        [OperationContract]
        List<RealisateurDTO> GetAllRealisators();

        [OperationContract]
        void RetourFilm(int id);
    }
}
