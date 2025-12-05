var filePath = args.Length > 0 ? args[0] : Path.Combine("..", "inputs", "day04.txt");

if (!File.Exists(filePath))
{
    Console.WriteLine($"File '{filePath}' was not found.");
    return;
}

var lines = File.ReadAllLines(filePath);
var rolls = ParseInput(lines).ToList();

var removedCount = RemoveRolls();
var totalCount = removedCount;

Console.WriteLine($"Part one: {totalCount}");

while ((removedCount = RemoveRolls()) > 0)
    totalCount += removedCount;

Console.WriteLine($"Part two: {totalCount}");

bool CanRemove(PaperRoll roll)
{
    return rolls.Count(other => other.IsAdjacentTo(roll)) < 4;
}

int RemoveRolls()
{
    var removable = rolls.Where(CanRemove).ToHashSet();
    return rolls.RemoveAll(removable.Contains);
}

static IEnumerable<PaperRoll> ParseInput(string[] lines)
{
    for (var row = 0; row < lines.Length; row++)
    for (var col = 0; col < lines[row].Length; col++)
    if (lines[row][col] == '@')
        yield return new PaperRoll(col, row);
}

file sealed record PaperRoll(int Col, int Row)
{
    public bool IsAdjacentTo(PaperRoll other) => 
        other != this &&
        other.Col >= Col - 1 &&
        other.Col <= Col + 1 &&
        other.Row >= Row - 1 &&
        other.Row <= Row + 1;
}
