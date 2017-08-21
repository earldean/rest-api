using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ArtistApi.Interfaces;
using ArtistApi.DataBase;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArtistApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ArtistController : Controller
    {
        private IArtistQueries artistQueries;

        //public ArtistController(IArtistQueries artistQueries)
        //{
        //    this.artistQueries = artistQueries;
        //}

        public ArtistController()
        {
            artistQueries = new ArtistDbQuires("Data Source=.\\SQLEXPRESS;Initial Catalog=Artist;Integrated Security=True");
        }

        // GET: api/values
        [HttpGet]
        public IActionResult All()
        {
            try
            {
                return Json(artistQueries.GetAllArtist());
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Albums(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                return Json(artistQueries.GetAlbumsFromArtist(id));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
