using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistApi.Models
{
    public class ArtistInfo
    {
        public ArtistInfo()
        {
            Albums = new List<AlbumInfo>();
        }

        public string ArtistName { get; set; }

        public List<AlbumInfo> Albums { get; set; }
    }
}
