using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public class PasswordHash
    {
        public static string GenerateHash(string password, string salt, int iteration) // Recursive function
        {
            if (iteration <= 0)
            {
                return password;
            }
            // SHA512 SHA512 = SHA512.Create; // 64 bit 
            SHA256 sHA256 = SHA256.Create(); // 32 bit
            var PassSalt = $"{password}{salt}";
            var byteValue = Encoding.UTF8.GetBytes(PassSalt);
            var byteHash = sHA256.ComputeHash(byteValue);
            var hash = Convert.ToBase64String(byteHash);
            return GenerateHash(hash, salt, iteration - 1); // Recursive call
        }
        public static string GenerateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var byteSalt = new byte[32];
            rng.GetBytes(byteSalt);
            return Convert.ToBase64String(byteSalt);
        }
    }
}
