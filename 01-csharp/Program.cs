using Day01;

var path = Path.Combine(Environment.SystemDirectory, "file.in");
using var reader = new StreamReader(path);
var input = reader.ReadToEnd();
Console.WriteLine(Solver.Solve1(input));
Console.WriteLine(Solver.Solve2(input));
