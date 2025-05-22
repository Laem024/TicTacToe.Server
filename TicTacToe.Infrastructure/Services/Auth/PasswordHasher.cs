using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Application.Interfaces.Auth.Services;

namespace TicTacToe.Infrastructure.Services.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();
        private static readonly int saltSize = 24;
        private static readonly int hashSize = 32;
        private static readonly int iterations = 100000;
        private static readonly HashAlgorithmName algorithm = HashAlgorithmName.SHA256;

        public string HashPassword(string password)
        {
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);

            using var pbkdf2  = new Rfc2898DeriveBytes(password, salt, iterations, algorithm);
            byte[] hash = pbkdf2.GetBytes(hashSize);

            byte[] hashBytes = new byte[saltSize + hashSize];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, saltSize);
            Buffer.BlockCopy(hash, 0, hashBytes, saltSize, hashSize);

            //base64Hast
            return Convert.ToBase64String(hashBytes);

        }

        public bool VerifyPassword(string password, string base64Hast)
        {
            byte[] hashBytes = Convert.FromBase64String(base64Hast); // password hasheada tomada de la base de datos

            byte[] salt = new byte[saltSize];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, saltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, algorithm); // hasheando la password enviada en el login
            byte[] hash = pbkdf2.GetBytes(hashSize);

            byte[] storedHash = new byte[hashSize];
            Buffer.BlockCopy(hashBytes, saltSize, storedHash, 0, hashSize);

            return CryptographicOperations.FixedTimeEquals(hash, storedHash);
        }
    }
}