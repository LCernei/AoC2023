namespace Day01;

public static class Solver
{
    private static readonly Dictionary<string, string> codes = new()
    {
        { "zero", "0" },
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" },
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
        var first = line.First(char.IsDigit);
        var last = line.Last(char.IsDigit);
        return first.ToInt() * 10 + last.ToInt();
    }
    
    private static int ProcessLine2(string line)
    {
        var first = GetDigits(line, (x, y) => x.IndexOf(y)).First();
        var last = GetDigits(line, (x, y) => x.LastIndexOf(y)).Last();
        return first.ToInt() * 10 + last.ToInt();
    }

    private static char[] GetDigits(string line, Func<string, string, int> selector)
    {
        return codes.Values.Select(x => new { Index = selector(line, x), Value = x[0] })
            .Concat(
                codes.Select(x => new { Index = selector(line, x.Key), Value = x.Value[0] }))
            .Where(x => x.Index >= 0)
            .OrderBy(x => x.Index)
            .Select(x => x.Value)
            .ToArray();
    }

    private static int ToInt(this char c) => c - '0';
}