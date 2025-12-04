var filePath = args.Length > 0 ? args[0] : Path.Combine("..", "inputs", "day04.txt");

if (!File.Exists(filePath))
{
    Console.WriteLine($"File '{filePath}' was not found.");
    return;
}

var lines = File.ReadAllLines(filePath);
var (removedCount, updatedMap) = RemoveRolls(lines);
var (initialRemovedCount, totalRemovedCount) = (removedCount, removedCount);

while (removedCount > 0)
{
    (removedCount, updatedMap) = RemoveRolls(updatedMap);
    totalRemovedCount += removedCount;
}

Console.WriteLine($"Part one: {initialRemovedCount}");
Console.WriteLine($"Part one: {totalRemovedCount}");

static (int RemovedCount, string[] NewMap) RemoveRolls(string[] lines)
{
    var total = 0;

    var sb = new System.Text.StringBuilder();
    
    for (var row = 0; row < lines.Length; row++)
    {
        for (var col = 0; col < lines[0].Length; col++)
        {
            if (lines[row][col] != '@')
            {
                sb.Append(lines[row][col]);
                continue;
            }
            
            var count = 0;

            if (row > 0)
            {
                if (col > 0 && lines[row - 1][col - 1] == '@') count++;
                if (lines[row - 1][col] == '@') count++;
                if (col < lines[0].Length - 1 && lines[row - 1][col + 1] == '@') count++;
            }

            if (col > 0 && lines[row][col - 1] == '@') count++;
            if (col < lines[0].Length - 1 && lines[row][col + 1] == '@') count++;

            if (row < lines.Length - 1)
            {
                if (col > 0 && lines[row + 1][col - 1] == '@') count++;
                if (lines[row + 1][col] == '@') count++;
                if (col < lines[0].Length - 1 && lines[row + 1][col + 1] == '@') count++;
            }

            if (count < 4)
            {
                total++;
                sb.Append('x');
            }
            else
            {
                sb.Append(lines[row][col]);
            }
        }

        sb.AppendLine();
    }

    return (total, sb.ToString().Trim().Split(Environment.NewLine));
}