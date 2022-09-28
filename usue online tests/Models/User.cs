using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace usue_online_tests.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _password = string.Empty;
                    return;
                }
                MD5 md5Hasher = MD5.Create();
                byte[] hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
                _password = Encoding.UTF8.GetString(hashed);
            }
        }

        public Roles Role { get; set; }
        public string Group { get; set; }
        public bool IsDark { get; set; }
    }
}
