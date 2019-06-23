using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PasswordChanger.Domain;

namespace PasswordChanger.Application.Services
{
    public class PasswordShuffler
    {
        private int _passwordLength = 12;

        private List<OpenOrderUser> users;

        public bool Shuffle()
        {
            ImportAllUsers();
            ModifyPasswords();
            ExportAllUsers();
            return false;
        }

        private void ImportAllUsers()
        {
            // Get all entities from Open Order User table.
            SqlConnection connection = new SqlConnection("");
            connection.Open();

            // TODO: create the appropriate query string.
            string queryString = "";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.CommandType = System.Data.CommandType.Text;

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                OpenOrderUser userToAdd = new OpenOrderUser((int)reader["Id"], (string)reader["Username"], (string)reader["Password"], new Company((int)reader["Company"], ""));
                users.Add(userToAdd);
            }
            reader.Close();

        }

        private void ExportAllUsers()
        {
            // Save the changed users passwords back into the table.
        }

        private void ModifyPasswords()
        {
            // shuffle passwords for users.
            foreach (OpenOrderUser user in users)
            {
                Random rand = new Random();
                List<int> stringKeys = new List<int>();
                for (int i = 0; i < _passwordLength; i++)
                {
                    stringKeys.Add(rand.Next(0, 100)); // 100 is an arbitrary number over the max count of characters in our arrays. Will mod this on the other side.
                }

                GenType genType = new GenType(rand.Next(1,3), stringKeys);

                string oldPassword = user.Password;
                string newPassword = PasswordGenerator.GeneratePassword(genType);
            }
        }
    }

}
