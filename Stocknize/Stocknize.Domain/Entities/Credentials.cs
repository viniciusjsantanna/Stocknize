using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Stocknize.Domain.Entities
{
    public class Credentials
    {
        public string Login { get; private set; }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                Salt = GenerateRandomSalt();
                password = GenerateHash(value, Salt);
            }
        }
        public string Salt { get; private set; }

        public bool isAuthentic(string password)
        {
            var hashedPassword = GenerateHash(password, Salt);
            return Password.Equals(hashedPassword);
        }

        private string GenerateHash(string password, string salt)
        {
            var algorithm = SHA256.Create();
            var bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password + Salt));
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        private string GenerateRandomSalt()
        {
            var regex = new Regex("[a-z A-Z 0-9]");
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return string.Join("", regex.Matches(Convert.ToBase64String(salt)).Cast<Match>().Select(e => e.Value).ToArray());
        }
    }
}
