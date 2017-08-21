using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistApi.Models
{
    /// <summary>
    /// Data type to return artistName and 
    /// unique Id
    /// </summary>
    public class Artist
    {
        public Artist() { }

        public int Id { get; set; } // primay key in artist DB
        public string ArtistName { get; set; }
    }
}
