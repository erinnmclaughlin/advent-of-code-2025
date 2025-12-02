using System.Reflection;
using AdventOfCode2025;

var dayNumber = GetDayNumber(args) ?? DateTime.Today.Day;

if (GetSolver(dayNumber) is not { } solver)
{
    Console.WriteLine($"Could not find solver for day {dayNumber}");
    return;
}

if (GetInputFilePath(dayNumber, args) is not { } inputFilePath)
{
    Console.WriteLine("Could not find input file.");
    return;
}

var (one, two) = solver.Solve(new FileInfo(inputFilePath));
Console.WriteLine($"Part one: {one}");
Console.WriteLine($"Part two: {two}");

return;

static ISolver? GetSolver(int dayNumber)
{
    foreach (var type in typeof(Program).Assembly.GetTypes().Where(t => t is { IsAbstract: false, IsClass: true }))
    {
        if (!type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISolver<>)))
            continue;
        
        var dayNumberProperty = type.GetProperty(nameof(ISolver<>.DayNumber), BindingFlags.Public | BindingFlags.Static);
        
        if (dayNumberProperty?.GetValue(null)?.Equals(dayNumber) ?? false)
            return (ISolver?)Activator.CreateInstance(type);
    }

    return null;
}

static int? GetDayNumber(string[] args)
{
    var index = args.IndexOf("--day");
    return index != -1 && args.Length > index + 1 && int.TryParse(args[index + 1], out var day) ? day : null;
}

static string? GetInputFilePath(int dayNumber, string[] args)
{
    var index = args.IndexOf("--input");
    var fileName = index != -1 && args.Length > index + 1 ? args[index + 1] : "input.txt";

    var directoryPath = Path.Combine("inputs", dayNumber.ToString());
    var filePath = Path.Combine(directoryPath, fileName);

    return File.Exists(filePath) ? filePath : Directory.GetFiles(directoryPath).FirstOrDefault();
}