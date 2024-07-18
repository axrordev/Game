using System.Security.Cryptography;

public static class KeyGenerator
{
    public static byte[] GenerateKey()
    {
        var key = new byte[32]; // 256 bits
        RandomNumberGenerator.Fill(key);
        return key;
    }
}
