namespace AdventOfCode2025.Day01;

public sealed class Solver : ISolver<Solver>
{
    public static int DayNumber => 1;
    
    public (string? PartOne, string? PartTwo) Solve(FileInfo inputFile)
    {
        var current = 50;
        var exactlyZeroCount = 0;
        var passThroughZeroCount = 0;
        
        foreach (var line in File.ReadLines(inputFile.FullName))
        {
            var number = int.Parse(line[1..]);
            var increment = line.StartsWith('L') ? -1 : 1;

            for (var i = 0; i < number; i++)
            {
                current += increment;
                if (current == 100) current = 0;
                if (current == -1) current = 99;
                if (current == 0) passThroughZeroCount++;
            }

            if (current == 0) exactlyZeroCount++;
        }
        
        return (exactlyZeroCount.ToString(), passThroughZeroCount.ToString());
    }
}