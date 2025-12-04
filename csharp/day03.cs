var filePath = args.Length > 0 ? args[0] : Path.Combine("..", "inputs", "day03.txt");

if (!File.Exists(filePath))
{
    Console.WriteLine($"File '{filePath}' was not found.");
    return;
}

var (partOne, partTwo) = File
    .ReadLines(filePath)
    .Select(line => line.Select((c, i) => (int.Parse(c.ToString()), i)).ToList())
    .Aggregate(
        seed: (PartOne: 0L, PartTwo: 0L), 
        func: (curr, next) => (curr.PartOne + Solve(next, 2), curr.PartTwo + Solve(next, 12))
    );

Console.WriteLine($"Part one: {partOne}");
Console.WriteLine($"Part two: {partTwo}");

static long Solve(List<(int Value, int Position)> digits, int targetDigitCount)
{
    var selectedDigits = new List<int>();
    var startingPosition = 0;

    while (selectedDigits.Count < targetDigitCount)
    {
        var remainingDigitCount = targetDigitCount - selectedDigits.Count;
            
        var max = digits
            .Where(x => x.Position >= startingPosition && x.Position < digits.Count - remainingDigitCount + 1)
            .MaxBy(x => x.Value);

        if (max == default)
            break;

        startingPosition = max.Position + 1;
        selectedDigits.Add(max.Value);
    }

    return long.Parse(string.Join("", selectedDigits));
}