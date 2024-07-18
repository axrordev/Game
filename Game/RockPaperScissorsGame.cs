using Game;

public static class RockPaperScissorsGame
{
    public static void Run(string[] args)
    {
        if (args.Length < 3 || args.Length % 2 == 0 || args.Distinct().Count() != args.Length)
        {
            Console.WriteLine("Error: You must provide an odd number of non-repeating arguments (≥ 3) that represent the moves.");
            Console.WriteLine("Example: dotnet run rock paper scissors lizard Spock");
            return;
        }

        // Generate key and computer move
        var key = KeyGenerator.GenerateKey();
        var computerMove = GenerateComputerMove(args);
        var hmac = HMACGenerator.CalculateHMAC(key, computerMove);

        // Display HMAC
        Console.WriteLine($"HMAC: {hmac}");

        // Display moves
        for (int i = 0; i < args.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {args[i]}");
        }
        Console.WriteLine("0 - exit");
        Console.WriteLine("? - help");

        while (true)
        {
            Console.Write("Enter your move: ");
            var input = Console.ReadLine();

            if (input == "?")
            {
                Rules.DisplayHelpTable(args);
                continue;
            }
            else if (input == "0")
            {
                break;
            }

            if (int.TryParse(input, out int userMoveIndex) && userMoveIndex > 0 && userMoveIndex <= args.Length)
            {
                var userMove = args[userMoveIndex - 1];
                Winner.DetermineWinner(userMove, computerMove, key, args);
                break;
            }
            else
            {
                Console.WriteLine("Invalid move. Please try again.");
            }
        }
    }

    private static string GenerateComputerMove(string[] moves)
    {
        var random = new Random();
        int index = random.Next(moves.Length);
        return moves[index];
    }
}
