using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistApi.Types
{
    public class ArtistValues
    {
        // parameterless constructor for serealization
        public ArtistValues() { }

        public int Id { get; set; }
        public string ArtistName { get; set; }
    }
}
