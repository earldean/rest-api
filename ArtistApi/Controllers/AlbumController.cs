using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArtistApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArtistApi.Controllers
{
    /// <summary>
    /// CRUD operations for albums
    /// </summary>
    [Route("api/[controller]")]
    public class AlbumController : Controller
    {
        private readonly ArtistDbQuires artistQueries;

        public AlbumController()
        {
            artistQueries = new ArtistDbQuires("Data Source=.\\SQLEXPRESS;Initial Catalog=Artist;Integrated Security=True");
        }

        // GET: api/album
        [HttpGet]
        public IEnumerable<string> Get()
        {
            DbSeed dbSeed = new DbSeed();
            return new string[] { "value1", "value2" };
        }

        // GET api/Album/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/album
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/album/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/album
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("ArtistId must be int greater than zero.  " +
                    "Check ArtistIndex for associated artistId.");
            }
            try
            {
                artistQueries.DeleteArtistInfo(id);
            }
            catch (Exception e) 
            {
                StatusCode(500);
            }

            return Ok("Delete Successfull");
        }
    }
}
