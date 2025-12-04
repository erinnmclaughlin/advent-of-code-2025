var filePath = args.Length > 0 ? args[0] : Path.Combine("..", "inputs", "day02.txt");

if (!File.Exists(filePath))
{
    Console.WriteLine($"File '{filePath}' was not found.");
    return;
}

var (partOneSum, partTwoSum) = File
    .ReadAllText(filePath)
    .Split(',')
    .SelectMany(line =>
    {
        var parts = line.Split('-');
        var min = int.Parse(parts[0]);
        var max = int.Parse(parts[1]);
        return Enumerable.Range(min, max - min + 1);
    })
    .Aggregate(
        seed: (PartOne: 0L, PartTwo: 0L),
        func: (current, id) =>
        {
            // any single digit number will always be valid
            if (id < 10) 
                return current;

            var idString = id.ToString();

            // if the id is invalid for part one it'll also be invalid for part two, so return now
            if (!IsValid(idString, maxMod: 2))
                return (current.PartOne + id, current.PartTwo + id);

            // set minMod = 3 because we've already checked mod = 2
            return (current.PartOne, current.PartTwo + (IsValid(idString, minMod: 3) ? 0 : id));
        }
    );

Console.WriteLine($"Part one: {partOneSum}");
Console.WriteLine($"Part two: {partTwoSum}");

static bool IsValid(string idString, int minMod = 2, int? maxMod = null)
{
    maxMod ??= idString.Length;
    var length = idString.Length;

    if (length == 1) return true;

    var mod = minMod;

    while (mod <= maxMod.Value)
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