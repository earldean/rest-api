using System;
using System.Collections.Generic;
using System.IO;
using ArtistApi.Models;

namespace ArtistApi.DataBase
{
    /// <summary>
    /// Populate initial DB from provided csv
    /// </summary>
    public class DbSeed
    {
        private const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Artist;Integrated Security=True";
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
                    var line = reader.ReadLine();
                    string[] albumData = ParseAlbumInfo(line);
                    string artistName = albumData[1];

                    if (!artists.Contains(artistName))
                    {
                        artistQuieries.InsertNewArtist(artistName);
                        artists.Add(artistName);
                    }

                    int artistId = artistQuieries.GetArtistPrimaryKey(artistName);
                    AlbumInfo albumInfo = new AlbumInfo()
                    {
                        AlbumName = albumData[0],
                        Genre = albumData[2],
                        Year = Int32.Parse(albumData[3])
                    };
                    artistQuieries.InsertNewAlbum(albumInfo, artistId);
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
