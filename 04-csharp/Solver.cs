namespace Day04;

public static class Solver
{
    private const StringSplitOptions splitOptions =
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
        
    public static object Solve1(string input) => Solve(input, ProcessLine1);
    
    private static object Solve(string input, Func<string, int> processor)
    {
        var lines = input.Split(Environment.NewLine, splitOptions).ToList();
        return lines.Select(processor).Sum();
    }
    
    private static int ProcessLine1(string line)
    {
        var wins = GetWins(line);
        return wins > 0 ? (int)Math.Pow(2, wins - 1) : 0;
    }
    
    public static object Solve2(string input)
    {
        var lines = input.Split(Environment.NewLine, splitOptions).ToList();
        var wins = lines.Select(GetWins).ToArray();
        var copies = Enumerable.Repeat(1, wins.Length).ToArray();
        for (var i = 0; i < wins.Length; i++)
        {
            for (var j = 0; j < wins[i]; j++)
            {
                copies[j + i + 1] += copies[i];
            }
        }

        return copies.Sum();
    }

    private static int GetWins(string line)
    {
        var (expected, actual) = line
            .Split(":", splitOptions)[1]
            .Split("|", splitOptions)
            .Select(x => x.Split(" ", splitOptions).Select(int.Parse))
            .ToArray();
        return expected.Intersect(actual).Count();
    }
    
    private static void Deconstruct<T>(this T[] array, out T first, out T second)
    {
        first = array[0];
        second = array[1];
    }
}