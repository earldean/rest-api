using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtistApi.Models;

namespace ArtistApi.Interfaces
{
    public interface IArtistQueries
    {
        int GetArtistPrimaryKey(string artistName);

        void InsertNewArtist(string artist);

        void InsertNewAlbum(AlbumInfo albumInfo, int artistId);

        /// <summary>
        /// Artist Index to return all artist names and asscoiated artistId
        /// </summary>
        /// <returns>List of Artist</returns>
        List<Artist> GetAllArtist();

        List<string> GetAlbumsFromArtist(int artistId);
    }
}
