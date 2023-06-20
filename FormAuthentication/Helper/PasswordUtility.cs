using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BankingSystem.Helper
{
    public static class PasswordUtility
    {
        public static string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            // Create a new instance of the hashing algorithm (e.g., bcrypt or SHA-256)
            using (var hashAlgorithm = new SHA256Managed())
            {
                // Convert the password string to bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Concatenate the salt and password bytes
                byte[] saltedPasswordBytes = new byte[saltBytes.Length + passwordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, saltedPasswordBytes, 0, saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, saltBytes.Length, passwordBytes.Length);

                // Compute the hash value
                byte[] hashBytes = hashAlgorithm.ComputeHash(saltedPasswordBytes);

                // Convert the hash value to a base64-encoded string
                string hash = Convert.ToBase64String(hashBytes);

                return hash;
            }
        }

        public static bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            string newHash = HashPassword(password, salt);

            // Compare the newly computed hash with the stored hashed password
            return newHash.Equals(hashedPassword);
        }

        public static string GenerateSalt()
        {
            // Generate a new salt value
            byte[] saltBytes = new byte[16];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(saltBytes);
            }

            // Convert the salt value to a base64-encoded string
            string salt = Convert.ToBase64String(saltBytes);

            return salt;
        }
    }

}