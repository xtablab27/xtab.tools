using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace xTab.Tools.Helpers
{
    public static class CryptoHelper
    {
        public static string GetPasswordHash(string email, string password, string salt)
        {
            var result = String.Empty;
            var sha = SHA256.Create();
            var hash = CreateHash(email + password + salt, sha);

            for (var i = 0; i < 500; i++)
            {
                hash = CreateHash(hash, sha);
            }

            return hash;
        }

        public static string CreateHash(String source, SHA256 encrypter)
        {
            var hash = encrypter.ComputeHash(Encoding.Default.GetBytes(source));

            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }
    }
}
