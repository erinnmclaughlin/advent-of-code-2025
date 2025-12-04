namespace AdventOfCode2025.CSharp;

public sealed class Day02 : ICSharpSolver<Day02>
{
    public static int DayNumber => 2;

    public (string? PartOne, string? PartTwo) Solve(FileInfo inputFile)
    {
        long partOneSum = 0;
        long partTwoSum = 0;

        foreach (var (min, max) in ParseInput(inputFile))
        {
            for (var id = min; id <= max; id++)
            {
                var idString = id.ToString();
                if (!IsValid(idString, 2)) partOneSum += id;
                if (!IsValid(idString, idString.Length)) partTwoSum += id;
            }
        }
        
        return (partOneSum.ToString(), partTwoSum.ToString());
    }
    
    private static bool IsValid(string idString, int maxMod)
    {
        var length = idString.Length;

        if (length == 1) return true;

        var mod = 2;

        while (mod <= maxMod)
        {
            if (length % mod != 0)
            {
                mod++;
                continue;
            }

            var partLength = length / mod;
            var part = idString[..partLength];

            if (idString == string.Join("", Enumerable.Repeat(part, mod)))
            {
                return false;
            }
                    
            mod++;
        }

        return true;
    }

    private static IEnumerable<(long MinValue, long MaxValue)> ParseInput(FileInfo inputFile) => File
        .ReadAllText(inputFile.FullName)
        .Split(',')
        .Select(line => line.Split('-'))
        .Select(parts => (long.Parse(parts[0]), long.Parse(parts[1])));
}