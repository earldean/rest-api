using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistApi.Interfaces
{
    public interface IArtistQueries
    {
        Guid GetArtistPrimaryKey(string artistName);

        Guid InsertNewArtist(string artist);

        void InsertNewAlbum(string[] albumInfo);

        List<string> GetAllArtist();

        List<string> GetAlbums(string artistName);
    }
}
