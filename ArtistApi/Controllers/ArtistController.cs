using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArtistApi.Interfaces;
using ArtistApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArtistApi.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult Get()
        {
            return Json(artistQueries.GetAllArtist());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Json(artistQueries.GetAlbums("oasis"));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
