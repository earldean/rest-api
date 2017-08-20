using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace ArtistApi.Models
{
    /// <summary>
    /// Populate initial DB from provided csv
    /// </summary>
    public class DbSeed
    {
        private const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Artist;Integrated Security=True";
        private string pathToCsv = @"C:/users/edean/github/ArtistApi/ArtistApi/albums.csv";
        ArtistDbQuires artistQuieries;

        public DbSeed()
        {
            artistQuieries = new ArtistDbQuires(connectionString);
            PopulateDb();
        }

        public void PopulateDb()
        {
            string dir = AppContext.BaseDirectory;
            using (var reader = new StreamReader(new FileStream(Path.Combine(dir, "albums.csv"), FileMode.Open)))
            {
                HashSet<string> artists = new HashSet<string>();
                var firstline = reader.ReadLine(); // read the first line which just has colum names
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().ToLower();
                    string[] albumInfo = ParseAlbumInfo(line);
                    string artist = albumInfo[1];

                    if (!artists.Contains(artist))
                    {
                        artistQuieries.InsertNewArtist(artist);
                        artists.Add(artist);
                    }
                    artistQuieries.InsertNewAlbum(albumInfo);
                }
            }
        }

        private string[] ParseAlbumInfo(string line)
        {
            string[] albumInfo = new string[4];
            // album name might have commas in it, so have to do some extra parsing
            if (line.Contains("\""))
            {
                int index = line.LastIndexOf("\"");
                string albumName = line.Substring(1, index - 1); // start at 1 to not include first comma
                string rest = line.Substring(index + 2); // + 2 to skip over comma
                string[] splitRest = rest.Split(',');
                albumInfo[0] = albumName;
                albumInfo[1] = splitRest[0];
                albumInfo[2] = splitRest[1];
                albumInfo[3] = splitRest[2];
            }
            else // standard csv format so just slpit on a comma
            {
                albumInfo = line.Split(',');
            }
            return albumInfo;
        }
    }
}
