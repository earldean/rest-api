using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtistApi.Types;

namespace ArtistApi.Interfaces
{
    public interface IArtistQueries
    {
        int GetArtistPrimaryKey(string artistName);

        void InsertNewArtist(string artist);

        void InsertNewAlbum(string[] albumInfo);

        List<ArtistValues> GetAllArtist();

        List<string> GetAlbums(string artistName);

        void DeleteArtistInfo(int artistId);
    }
}
