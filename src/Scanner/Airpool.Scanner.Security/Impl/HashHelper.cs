using System;
using System.Security.Cryptography;

namespace Airpool.Scanner.Security.Impl
{
    public static class HashHelper
    {
        // https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129

        public static bool Compare(string savedHash, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(savedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }

        public static string Encrypt(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
