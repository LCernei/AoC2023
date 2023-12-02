namespace Day02;

public static class Solver
{
    private const StringSplitOptions splitOptions =
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
    private static Dictionary<string, int> max = new()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 },
    };
        
    public static object Solve1(string input) => Solve(input, ProcessLine1);
    
    public static object Solve2(string input) => Solve(input, ProcessLine2);

    private static object Solve(string input, Func<string, int> processor)
    {
        var lines = input.Split(Environment.NewLine)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();
        return lines.Select(processor).Sum();
    }
    
    private static int ProcessLine1(string line)
    {
        var (gameName, gameRounds) = line.Split(':', splitOptions);
        var (_, gameId) = gameName.Split(" ", splitOptions);
        var rounds = gameRounds.Split(";", splitOptions);

        foreach (var round in rounds)
        {
            var cubes = round.Split(",", splitOptions);
            foreach (var cube in cubes)
            {
                var (value, color) = cube.Split(" ", splitOptions);
                if (!int.TryParse(value, out var valueInt) || valueInt > max[color])
                    return 0;
            }
        }

        return int.Parse(gameId);
    }
    
    private static int ProcessLine2(string line)
    {
        var (gameName, gameRounds) = line.Split(':', splitOptions);
        var (_, gameId) = gameName.Split(" ", splitOptions);
        var rounds = gameRounds.Split(";", splitOptions);

        var aggregator = max.Keys.ToDictionary(x => x, x => 0);
        foreach (var round in rounds)
        {
            var cubes = round.Split(",", splitOptions);
            foreach (var cube in cubes)
            {
                var (value, color) = cube.Split(" ", splitOptions);
                var valueInt = int.Parse(value);
                if (valueInt > aggregator[color])
                    aggregator[color] = valueInt;
            }
        }

        return aggregator.Aggregate(1, (x, y) => x * y.Value);
    }
    
    private static void Deconstruct<T>(this T[] array, out T first, out T second)
    {
        first = array[0];
        second = array[1];
    }
}