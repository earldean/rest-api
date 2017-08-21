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

        /// <summary>
        /// Artist Index to return all artist names and asscoiated artistId
        /// </summary>
        /// <returns>List of Artist</returns>
        List<Artist> GetAllArtist();

        List<string> GetAlbumsFromArtist(int artistId);

        /// <summary>
        /// Create operation 
        /// </summary>
        /// <param name=""></param>
        void Create(ArtistInfo artistInfo);

        /// <summary>
        /// Read operation for Albums Controller
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        ArtistInfo Read(int artistId);

        /// <summary>
        /// Delete operation for albums controller
        /// </summary>
        /// <param name="artistId"></param>
        void Delete(int artistId);
    }
}
