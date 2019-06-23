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
        public string OldPassword { get; }
        public string NewPassword { get; }
        public Company Company { get; }

        public OpenOrderUser(int id, string username, string oldPassword, string newPassword, Company company)
        {
            Id = id;
            Username = username;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            Company = company;
        }
    }
}
