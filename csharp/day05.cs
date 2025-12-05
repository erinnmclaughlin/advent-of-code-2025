var filePath = args.Length > 0 ? args[0] : Path.Combine("..", "inputs", "day05.txt");

if (!File.Exists(filePath))
{
    Console.WriteLine($"File '{filePath}' was not found.");
    return;
}

var ranges = new List<Range>();
var validIdCount = 0;

foreach (var line in File.ReadLines(filePath))
{
    if (string.IsNullOrEmpty(line))
        continue;

    if (line.Contains('-'))
    {
        ranges.Add(Range.Parse(line));
        continue;
    }

    var id = long.Parse(line);

    if (ranges.Any(r => r.Contains(id)))
        validIdCount++;
}

var sum = 0L;
var lastProcessed = 0L;

foreach (var range in ranges.OrderBy(x => x.Min).ThenBy(x => x.Max))
{
    var min = Math.Max(range.Min, lastProcessed + 1);

    if (min > range.Max)
        continue;
    
    sum += (range with { Min = min }).GetSize();
    lastProcessed = range.Max;
}

Console.WriteLine($"Part one: {validIdCount}");
Console.WriteLine($"Part two: {sum}");

internal sealed record Range(long Min, long Max)
{
    public bool Contains(long value)
    {
        return value >= Min && value <= Max;
    }

    public static Range Parse(string line)
    {
        var parts = line.Split('-');
        var start = long.Parse(parts[0]);
        var end = long.Parse(parts[1]);
        return new Range(start, end);
    }

    public long GetSize()
    {
        return Max - Min + 1;
    }
}
