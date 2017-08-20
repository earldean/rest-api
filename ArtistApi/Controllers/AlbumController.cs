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
        public AlbumController()
        {

        }

        // GET: api/album
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //DbSeed dbSeed = new DbSeed();
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
        public void Delete(int id)
        {
        }
    }
}
