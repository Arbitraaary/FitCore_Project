using System.Security.Cryptography;
using FitCore_API.Abstractions.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace FitCore_API.Services;

public class PasswordService : IPasswordService
{
    public (string Hash, string Salt) HashPassword(string password)
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(128 / 8);
        string salt = Convert.ToBase64String(saltBytes);

        string hash = ComputeHash(password, saltBytes);

        return (hash, salt);
    }

    public bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        try 
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            string computedHash = ComputeHash(password, saltBytes);
            return storedHash == computedHash;
        }
        catch 
        {
            return false;
        }
    }

    private string ComputeHash(string password, byte[] saltBytes)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8 
        ));
    }
}