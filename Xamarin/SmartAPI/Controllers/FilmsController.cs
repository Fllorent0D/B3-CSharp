using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogicLayer;

namespace SmartAPI.Controllers
{
    public class FilmsController : ApiController
    {
        // GET: Films
        [HttpGet]
        public IEnumerable<DTOLibrary.FilmDTO> GetAll()
        {
            return BLLVideotheque.getStock();
        }

        [HttpGet]
        public IEnumerable<DTOLibrary.FilmDTO> Actors(int id)
        {
            return BLLVideotheque.getStock();
        }

    }
}
