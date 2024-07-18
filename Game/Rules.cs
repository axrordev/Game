using ConsoleTables;

public static class Rules
{
    public static void DisplayHelpTable(string[] moves)
    {
        int numMoves = moves.Length;
        var table = new ConsoleTable(new string[] { "v PC/User >" }.Concat(moves).ToArray());

        for (int i = 0; i < numMoves; i++)
        {
            var row = new List<string> { moves[i] };
            for (int j = 0; j < numMoves; j++)
            {
                if (i == j)
                {
                    row.Add("Draw");
                }
                else if ((j > i && j <= i + numMoves / 2) || (j < i && j + numMoves > i + numMoves / 2))
                {
                    row.Add("Win");
                }
                else
                {
                    row.Add("Lose");
                }
            }
            table.AddRow(row.ToArray());
        }

        table.Write();
    }
}
