using System.Security.Cryptography;

namespace Game;

public static class Winner
{
    public static void DetermineWinner(string userMove, string computerMove, byte[] key, string[] moves)
    {
        int userMoveIndex = Array.IndexOf(moves, userMove);
        int computerMoveIndex = Array.IndexOf(moves, computerMove);
        int halfMoves = moves.Length / 2;

        Console.WriteLine($"Your move: {userMove}");
        Console.WriteLine($"Computer move: {computerMove}");

        if (userMoveIndex == computerMoveIndex)
        {
            Console.WriteLine("Draw!");
        }
        else if ((userMoveIndex > computerMoveIndex && userMoveIndex <= computerMoveIndex + halfMoves) ||
                 (userMoveIndex < computerMoveIndex && userMoveIndex + moves.Length > computerMoveIndex + halfMoves))
        {
            Console.WriteLine("You win!");
        }
        else
        {
            Console.WriteLine("Computer wins!");
        }

        var keyString = BitConverter.ToString(key).Replace("-", "").ToLower();
        Console.WriteLine($"HMAC key: {keyString}");

        // Provide a link to the internal verification process
        Console.WriteLine($"To verify the HMAC, use the following details:");
        Console.WriteLine($"Message: {computerMove}");
        Console.WriteLine($"Key: {keyString}");

        // Optional: Call the internal verification function
        VerifyHMAC(HMACGenerator.CalculateHMAC(key, computerMove), keyString, computerMove);
    }

    private static void VerifyHMAC(string originalHMAC, string keyString, string message)
    {
        var keyBytes = Enumerable.Range(0, keyString.Length / 2)
                                  .Select(x => Convert.ToByte(keyString.Substring(x * 2, 2), 16))
                                  .ToArray();
        var recalculatedHMAC = HMACGenerator.CalculateHMAC(keyBytes, message);
        if (originalHMAC == recalculatedHMAC)
        {
            Console.WriteLine("HMAC verified successfully!");
        }
        else
        {
            Console.WriteLine("HMAC verification failed.");
        }
    }
}