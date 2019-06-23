using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordChanger.Domain
{
    public class OpenOrderUser
    {
        public int Id { get; }
        public string Username { get; }
        public string Password { get; }
        public Company Company { get; }

        public OpenOrderUser(int id, string username, string password, Company company)
        {
            Id = id;
            Username = username;
            Password = password;
            Company = company;
        }
    }
}
