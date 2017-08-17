using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data;
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
        public DbSeed()
        {
            PopulateDb();
            
        }

        public void PopulateDb()
        {
            string dir = AppContext.BaseDirectory;
            using (var reader = new StreamReader(new FileStream(Path.Combine(dir, "albums.csv"), FileMode.Open)))
            {
                var firstline = reader.ReadLine(); // read the first line which just has colum names
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    string[] values;
                    if (!line.Contains("\""))
                    {
                        values = line.Split(',');
                        InsertArtist(values);
                    }
                    else
                    {
                        ParseString(line);
                    }
                    
                }
            }
        }

        private void InsertArtist(string[] data)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Guid guid = Guid.NewGuid();
                connection.Open();
                string commandString = @"Insert Into ArtistName Values (" + "'" + guid + "'" + 
                    ", " + "'" + data[1].ToLower().Replace(" ", "-") + "'" + ")";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.ExecuteScalar();
            }
        }

        private void InsertAlbum()
        {

        }

        private string[] ParseString(string data)
        {
            string[] returnVal = new string[4];
            int index = data.LastIndexOf("\"");

            string albumName = data.Substring(1, index + 1); // start at one to not include first comma
            string rest = data.Substring(index + 2); // + 2 to skip over coma
            return returnVal;
        }
    }
}
