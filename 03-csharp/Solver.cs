namespace Day03;

public static class Solver
{
    private const StringSplitOptions splitOptions =
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
    private static List<string> map;
    
    public static object Solve1(string input) => Solve(input, ProcessLine1);
    
    public static object Solve2(string input) => Solve(input, ProcessLine2);

    private static object Solve(string input, Func<string, int, int> processor)
    {
        // bad - map is global
        map = input.Split(Environment.NewLine, splitOptions).ToList();
        return map.Select(processor).Sum();
    }
    
    private static int ProcessLine1(string line, int i)
    {
        var sum = 0;
        for (var j = 0; j < map[i].Length; j++)
        {
            if (!char.IsDigit(map[i][j]))
                continue;
            
            var number = map[i][j].ToInt();
            var isEnginePart = IsEngineDigit(i, j);
            while (j+1 < map[i].Length && char.IsDigit(map[i][j+1]))
            {
                j += 1;
                number *= 10;
                number += map[i][j].ToInt();
                isEnginePart |= IsEngineDigit(i, j);
            }

            if (isEnginePart)
                sum += number;
        }

        return sum;
    }
    
    private static int ProcessLine2(string line, int i)
    {
        var sum = 0;
        for (var j = 0; j < map[i].Length; j++)
        {
            if (map[i][j] != '*')
                continue;

            var numbers = GetNumbers(i, j);
            if (numbers.Count == 2)
                sum += numbers[0] * numbers[1];
        }

        return sum;
    }

    private static List<int> GetNumbers(int i, int j)
    {
        var numbers = new List<int>();
        if (i - 1 >= 0 && j - 1 > 0 && map[i - 1][j - 1].IsDigit())
            numbers.Add(GetNumber(i - 1, j - 1));
        if (i - 1 >= 0 && map[i - 1][j].IsDigit() && !(i - 1 >= 0 && j - 1 > 0 && map[i - 1][j - 1].IsDigit()) )
            numbers.Add(GetNumber(i - 1, j));
        if (i - 1 >= 0 && j + 1 < map[i].Length && map[i - 1][j + 1].IsDigit() && !(i - 1 >= 0 && map[i - 1][j].IsDigit()))
            numbers.Add(GetNumber(i - 1, j + 1));
        if (j - 1 >= 0 && map[i][j - 1].IsDigit())
            numbers.Add(GetNumber(i, j - 1));
        if (j + 1 < map[i].Length && map[i][j + 1].IsDigit())
            numbers.Add(GetNumber(i, j + 1));
        if (i + 1 < map.Count && j - 1 >= 0 && map[i + 1][j - 1].IsDigit())
            numbers.Add(GetNumber(i + 1, j - 1));
        if (i + 1 < map.Count && map[i + 1][j].IsDigit() && !(i + 1 < map.Count && j - 1 >= 0 && map[i + 1][j - 1].IsDigit()))
            numbers.Add(GetNumber(i + 1, j));
        if (i + 1 < map.Count && j + 1 < map[i].Length && map[i + 1][j + 1].IsDigit() && !(i + 1 < map.Count && map[i + 1][j].IsDigit()))
            numbers.Add(GetNumber(i + 1, j + 1));
        return numbers;
    }

    private static int GetNumber(int i, int j)
    {
        while (j > 0 && map[i][j-1].IsDigit())
            j--;
        var number = map[i][j].ToInt();
        while (j + 1 < map[i].Length && map[i][j+1].IsDigit())
        {
            j += 1;
            number *= 10;
            number += map[i][j].ToInt();
        }

        return number;
    }

    private static bool IsEngineDigit(int i, int j)
    {
        if (i - 1 >= 0 && j - 1 > 0 && map[i - 1][j - 1].IsSymbol())
            return true;
        if (i -1 >= 0 && map[i - 1][j].IsSymbol())
            return true;
        if (i - 1 >= 0 && j + 1 < map[i].Length && map[i - 1][j + 1].IsSymbol())
            return true;
        if (j - 1 >= 0 && map[i][j - 1].IsSymbol())
            return true;
        if (j + 1 < map[i].Length && map[i][j + 1].IsSymbol())
            return true;
        if (i + 1 < map.Count && j - 1 >= 0 && map[i + 1][j - 1].IsSymbol())
            return true;
        if (i + 1 < map.Count && map[i + 1][j].IsSymbol())
            return true;
        if (i + 1 < map.Count && j + 1 < map[i].Length && map[i + 1][j + 1].IsSymbol())
            return true;
        return false;
    }

    private static bool IsSymbol(this char c) => c != '.' && !char.IsDigit(c);
    private static bool IsDigit(this char c) => char.IsDigit(c);
    private static int ToInt(this char c) => c - '0';
}