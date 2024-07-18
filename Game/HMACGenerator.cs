using System.Security.Cryptography;
using System.Text;

public static class HMACGenerator
{
    public static string CalculateHMAC(byte[] key, string message)
    {
        using (var hmac = new HMACSHA256(key))
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            var hash = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
