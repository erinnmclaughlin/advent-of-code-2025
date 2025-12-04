var filePath = args.Length > 0 ? args[0] : Path.Combine("..", "inputs", "day01.txt");

if (!File.Exists(filePath))
{
    Console.WriteLine($"File '{filePath}' was not found.");
    return;
}

var (current, exactlyZeroCount, passThroughZeroCount) = (50, 0, 0);

foreach (var line in File.ReadLines(filePath))
{
    var increment = line[0] is 'L' ? -1 : 1;
    var ticks = int.Parse(line[1..]);

    for (var i = 0; i < ticks; i++)
    {
        current = (current + increment + 100) % 100;
        if (current == 0) passThroughZeroCount++;
    }

    if (current == 0) exactlyZeroCount++;
}

Console.WriteLine($"Part one: {exactlyZeroCount}");
Console.WriteLine($"Part two: {passThroughZeroCount}");
