namespace AdventOfCode2025;

public interface ISolver<TSelf> : ISolver where TSelf : ISolver<TSelf>, new()
{
    public static abstract int DayNumber { get; }
}

public interface ISolver
{
    public (object? PartOne, object? PartTwo) Solve(FileInfo inputFile);
}
