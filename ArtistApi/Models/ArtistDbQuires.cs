using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;
using ArtistApi.Interfaces;
using System.Data;

namespace ArtistApi.Models
{
    /// <summary>
    /// API for accesing Artist DB
    /// </summary>
    public class ArtistDbQuires : IArtistQueries
    {
        private readonly string connectionString;

        public ArtistDbQuires(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Guid GetArtistPrimaryKey(string artistName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString = 
                    @"select ArtistId " +
                    "from dbo.ArtistName " +
                    "where ArtistName = @artistName";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.Parameters.Add("@artistName", SqlDbType.NVarChar, 128).Value = artistName;
                Guid artistId = (Guid)command.ExecuteScalar();
                return artistId;
            }
        }

        public List<string> GetAllArtist()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString = "select ArtistName from ArtistName";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.CommandType = CommandType.Text;
                List<string> artists = new List<string>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            artists.Add(reader.GetString(0));
                        }
                        reader.NextResult();
                    }
                }
                return artists;
           }
        }

        public List<string> GetAlbums(string artistName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString =
                    "Select AlbumName from Albums where artistName = @ArtistName";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ArtistName", SqlDbType.NVarChar).Value = artistName;
                List<string> albums = new List<string>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            albums.Add((string)reader.GetValue(0));
                        }
                        reader.NextResult();
                    }
                }
                return albums;
            }
        }

        public void InsertNewAlbum(string[] albumInfo)
        {
            string albumName = albumInfo[0];
            string artistName = albumInfo[1];
            string genre = albumInfo[2];
            string year = albumInfo[3];
            Guid artistId = GetArtistPrimaryKey(artistName);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString =
                    @"Insert Into Albums " +
                    "Values (@artistId, @artistName, @albumName, @genre, @year)";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.Parameters.Add("@artistId", SqlDbType.UniqueIdentifier).Value = artistId;
                command.Parameters.Add("@artistName", SqlDbType.NVarChar, 128).Value = artistName;
                command.Parameters.Add("@albumName", SqlDbType.NVarChar, 128).Value = albumName;
                command.Parameters.Add("@genre", SqlDbType.NVarChar, 128).Value = genre;
                command.Parameters.Add("@year", SqlDbType.NVarChar, 128).Value = year;
                command.ExecuteScalar();
            }
        }

        public Guid InsertNewArtist(string artist)
        {
            Guid guid = Guid.NewGuid();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString =
                    @"Insert Into ArtistName " + 
                    "Values (@guid, @artistName)";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.Parameters.Add("@guid", SqlDbType.UniqueIdentifier).Value = guid;
                command.Parameters.Add("@artistName", SqlDbType.NVarChar, 128).Value = artist;
                command.ExecuteScalar();
            }
            return guid;
        }
    }
}
