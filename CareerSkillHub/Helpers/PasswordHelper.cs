using System;
using System.Security.Cryptography;
using System.Text;

namespace CareerSkillHub.Helpers
{
    
    public static class PasswordHelper
    {
        
        public static string GenerateSalt()
        {
            byte[] bytes = new byte[16];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            return ToHex(bytes);
        }

        public static string Hash(string password, string salt)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] combined = Encoding.UTF8.GetBytes((password ?? "") + salt);
                byte[] hash = sha.ComputeHash(combined);
                return ToHex(hash);
            }
        }

        public static bool Verify(string password, string salt, string expectedHash)
        {
            string actualHash = Hash(password, salt);
            return string.Equals(actualHash, expectedHash, StringComparison.OrdinalIgnoreCase);
        }

        private static string ToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}