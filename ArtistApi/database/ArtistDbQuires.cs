using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ArtistApi.Interfaces;
using ArtistApi.Models;
using System.Data;

namespace ArtistApi.DataBase
{
    /// <summary>
    /// API for accesing Artist DB
    /// </summary>
    public class ArtistDbQuires : IArtistQueries, ICrudOperations
    {
        private readonly string connectionString;

        public ArtistDbQuires(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(ArtistInfo artistInfo)
        {
            artistInfo.ArtistName.ToLower();
            string artistName = artistInfo.ArtistName;
            if (!ArtistExists(artistName))
            {
                InsertNewArtist(artistName);
            }

            int artistId = GetArtistPrimaryKey(artistName);
            InsertAlbums(artistInfo.Albums, artistId);
        }

        public ArtistInfo Read(int artistId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString =
                    "select * " +
                    "from Artists " +
                    "inner join Albums " +
                    "on Artists.ArtistId = Albums.ArtistId " +
                    "where Artists.ArtistId = @id";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@id", SqlDbType.Int).Value = artistId;

                ArtistInfo artistInfo = new ArtistInfo();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    bool firstRow = true;
                    while (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (firstRow)
                            {
                                artistInfo.ArtistName = reader.GetString(1);
                                artistInfo.Albums.Add(GetAlbumInfo(reader));
                                firstRow = false;
                            }
                            else
                            {
                                artistInfo.Albums.Add(GetAlbumInfo(reader));
                            }
                        }
                        reader.NextResult();
                    }
                }
                return artistInfo;
            }
        }

        public void Update(AlbumInfo albumInfo, int artistId)
        {

        }

        public void Delete(int artistId)
        {
            DeleteFromAlbumsTable(artistId);
            DeleteFromArtistsTable(artistId);
        }

        public int GetArtistPrimaryKey(string artistName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString = 
                    @"select ArtistId " +
                    "from dbo.Artists " +
                    "where ArtistName = @artistName";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.Parameters.Add("@artistName", SqlDbType.NVarChar, 128).Value = artistName;
                int artistId = (int)command.ExecuteScalar();
                return artistId;
            }
        }

        public List<Artist> GetAllArtist()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString = "select * from Artists";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.CommandType = CommandType.Text;
                List<Artist> artists = new List<Artist>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Artist value = new Artist()
                            {
                                Id = reader.GetInt32(0),
                                ArtistName = reader.GetString(1)
                            };
                            artists.Add(value);
                        }
                        reader.NextResult();
                    }
                }
                return artists;
           }
        }

        public List<string> GetAlbumsFromArtist(int artistId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString =
                    "Select AlbumName from Albums where ArtistId = @ArtistId";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ArtistId", SqlDbType.Int).Value = artistId;
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

        private void InsertAlbums(List<AlbumInfo> albums, int artistId)
        {
            foreach (AlbumInfo info in albums)
            {
                info.AlbumName.ToLower();
                info.Genre.ToLower();
                info.Year.ToLower();
                InsertNewAlbum(info, artistId);
            }
        }

        public void InsertNewAlbum(AlbumInfo albumInfo, int artistId)
        {
            string albumName = albumInfo.AlbumName;
            string genre = albumInfo.Genre;
            string year = albumInfo.Year;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString =
                    @"Insert Into Albums " +
                    "Values (@artistId, @albumName, @genre, @albumYear)";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@artistId", SqlDbType.Int).Value = artistId;
                command.Parameters.Add("@albumName", SqlDbType.NVarChar, 128).Value = albumName;
                command.Parameters.Add("@genre", SqlDbType.NVarChar, 128).Value = genre;
                command.Parameters.Add("@albumYear", SqlDbType.NVarChar, 128).Value = year;
                command.ExecuteNonQuery();
            }
        }

        public void InsertNewArtist(string artistName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString =
                    @"Insert Into Artists " + 
                    "Values (@artistName)";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@artistName", SqlDbType.NVarChar, 128).Value = artistName;
                command.ExecuteNonQuery();
            }
        }

        private void DeleteFromArtistsTable(int artistId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString = "delete from artists where ArtistId = @id";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = artistId;
                command.ExecuteNonQuery();
            }
        }

        private void DeleteFromAlbumsTable(int artistId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString = "delete from albums where ArtistId = @id";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = artistId;
                command.ExecuteNonQuery();
            }
        }

        private AlbumInfo GetAlbumInfo(SqlDataReader reader)
        {
            return new AlbumInfo()
            {
                AlbumName = reader.GetString(4),
                Genre = reader.GetString(5),
                Year = reader.GetString(6)
            };
        }

        private bool ArtistExists(string artistName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandString =
                    "Select ArtistId " +
                    "From Artists " +
                    "Where ArtistName = @artistName";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.Parameters.Add("@artistName", SqlDbType.NVarChar).Value = artistName;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
