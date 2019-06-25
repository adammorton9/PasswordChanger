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
                    "FROM edge.dbo.openOrderUsers o " +
                    "JOIN " +
                    "Companies c " +
                    "ON o.companyID = c.id " +
                    "WHERE o.companyID NOT IN (919, 687, 1029, 1492, 583, 350, 376, 116, 351, 70, 74, 395)";
                SqlCommand command = new SqlCommand(queryString, connection)
                {
                    CommandType = System.Data.CommandType.Text
                };

                SqlDataReader reader = command.ExecuteReader();
                Random rand = new Random(DateTime.Now.Second);
                while (reader.Read())
                {
                    users.Add(
                        new OpenOrderUser(
                            (int)reader["Id"],
                            (string)reader["Username"],
                            (string)reader["Password"],
                            GenerateNewPassword(rand),
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
                        "UPDATE edge.dbo.openOrderUsers SET password = @newPassword WHERE openOrderUsersID = @id";

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

        public static string GenerateNewPassword(Random random)
        {
            return Guid.NewGuid().ToString("d").Replace("-", "").Substring(1, 15);
        }
        
    }

}
