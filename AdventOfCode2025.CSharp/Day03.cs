namespace AdventOfCode2025.CSharp;

public sealed class Day03 : ISolver<Day03>
{
    public static int DayNumber => 3;
    
    public (object? PartOne, object? PartTwo) Solve(FileInfo inputFile)
    {
        long partOne = 0; 
        long partTwo = 0;
        
        foreach (var line in File.ReadLines(inputFile.FullName))
        {
            var digits = line.Select((c, i) => new Digit(int.Parse(c.ToString()), i)).ToList();
            partOne += Solve(digits, 2);
            partTwo += Solve(digits, 12);
        }

        return (partOne, partTwo);
    }

    private static long Solve(List<Digit> digits, int targetDigitCount)
    {
        var selectedDigits = new List<int>();
        var startingPosition = 0;

        while (selectedDigits.Count < targetDigitCount)
        {
            var remainingDigitCount = targetDigitCount - selectedDigits.Count;
                
            var max = digits.Where(x => x.Position >= startingPosition && x.Position < digits.Count - remainingDigitCount + 1).MaxBy(x => x.Value);

            if (max == null)
                break;

            startingPosition = max.Position + 1;
            selectedDigits.Add(max.Value);
        }

        return long.Parse(string.Join("", selectedDigits));
    }
    private sealed record Digit(int Value, int Position);
}