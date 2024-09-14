using System.Security.Cryptography;

namespace Task3;

public class RandomKeyGenerator
{
    public byte[] GenerateKey(int keyLength)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] key = new byte[keyLength / 8]; // 256 bits = 32 bytes
            rng.GetBytes(key);
            return key;
        }
    }
}