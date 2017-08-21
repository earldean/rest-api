using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistApi.Models
{
    public class AlbumInfo
    {
        public AlbumInfo() { }

        public string AlbumName { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
    }
}
