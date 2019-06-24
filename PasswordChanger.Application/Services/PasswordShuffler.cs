using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PasswordChanger.Domain;

namespace PasswordChanger.Application.Services
{
    public class PasswordShuffler
    {
        private const int PASSWORD_LENGTH = 12;
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["EdgeDb"].ConnectionString;
        
        public IEnumerable<OpenOrderUser> Shuffle()
        {
            var users = ImportAllUsers();
            ExportAllUsers(users);
            return users;
        }

        private List<OpenOrderUser> ImportAllUsers()
        {
            var users = new List<OpenOrderUser>();
            // Get all entities from Open Order User table.
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // TODO: create the appropriate query string.
                string queryString =
                    "SELECT " +
                    "o.OpenOrderUsersID as Id, " +
                    "o.username as Username, " +
                    "o.password as Password, " +
                    "c.id as CompanyId, " +
                    "c.CompanyName as CompanyName " +
                    "FROM openOrderUsers o " +
                    "LEFT JOIN " +
                    "Companies c " +
                    "ON o.companyID = c.id";
                SqlCommand command = new SqlCommand(queryString, connection)
                {
                    CommandType = System.Data.CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(
                        new OpenOrderUser(
                            (int)reader["Id"],
                            (string)reader["Username"],
                            (string)reader["Password"],
                            GenerateNewPassword(),
                            new Company(
                                (int)reader["CompanyId"],
                                (string)reader["CompanyName"]
                            )
                        ));
                }

                reader.Close();
                connection.Close();
            }

            return users;
        }

        private void ExportAllUsers(IEnumerable<OpenOrderUser> users)
        {
            foreach (var user in users)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // TODO: create the appropriate query string.
                    string queryString =
                        "UPDATE openOrderUsers SET password = @newPassword WHERE openOrderUsersID = @id";

                    SqlCommand command = new SqlCommand(queryString, connection)
                    {
                        CommandType = System.Data.CommandType.Text
                    };

                    command.Parameters.AddWithValue("@id", user.Id);
                    command.Parameters.AddWithValue("@newPassword", user.NewPassword);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }

        private string GenerateNewPassword()
        {
            Random rand = new Random();
            List<int> stringKeys = new List<int>();
            for (int i = 0; i < PASSWORD_LENGTH; i++)
            {
                stringKeys.Add(rand.Next(0, 100)); // 100 is an arbitrary number over the max count of characters in our arrays. Will mod this on the other side.
            }

            GenType genType = new GenType(rand.Next(1,3), stringKeys);
            
            return PasswordGenerator.GeneratePassword(genType);
        }
        
    }

}
