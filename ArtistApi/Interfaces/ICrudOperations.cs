using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtistApi.Models;

namespace ArtistApi.Interfaces
{
    public interface ICrudOperations
    {
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

        void Update(AlbumInfo albumInfo, int artistId);

        /// <summary>
        /// Delete operation for albums controller
        /// </summary>
        /// <param name="artistId"></param>
        void Delete(int artistId);
    }
}
