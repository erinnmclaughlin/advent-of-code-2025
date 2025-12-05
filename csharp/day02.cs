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
            var idString = id.ToString();

            // if the id is invalid for part one it'll also be invalid for part two, so return now
            if (!IsValid(idString, 2))
                return (current.PartOne + id, current.PartTwo + id);

            var mod = 3; // start at 3 because we already checked 2

            while (mod <= idString.Length)
            {
                if (!IsValid(idString, mod))
                {
                    return (current.PartOne, current.PartTwo + id);
                }

                mod++;
            }

            return current;
        }
    );

Console.WriteLine($"Part one: {partOneSum}");
Console.WriteLine($"Part two: {partTwoSum}");


static bool IsValid(string idString, int numParts)
{
    if (idString.Length == 0 || idString.Length % numParts != 0)
        return true;

    var size = idString.Length / numParts;
    var part = idString[..size];
    var invalid = string.Join("", Enumerable.Repeat(part, numParts));

    return !idString.Equals(invalid);
}