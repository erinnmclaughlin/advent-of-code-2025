namespace AdventOfCode2025.CSharp;

public sealed class Day01 : ISolver<Day01>
{
    public static int DayNumber => 1;
    
    public (object? PartOne, object? PartTwo) Solve(FileInfo inputFile)
    {
        var dial = new Dial();
        var exactlyZeroCount = 0;
        var passThroughZeroCount = 0;
        
        foreach (var line in File.ReadLines(inputFile.FullName))
        {
            Action turnDial = line[0] is 'L' ? dial.TurnLeft : dial.TurnRight;
            var ticks = int.Parse(line[1..]);

            for (var i = 0; i < ticks; i++)
            {
                turnDial();
                if (dial.CurrentValue == 0) passThroughZeroCount++;
            }

            if (dial.CurrentValue == 0) exactlyZeroCount++;
        }
        
        return (exactlyZeroCount, passThroughZeroCount);
    }

    public sealed class Dial
    {
        public int CurrentValue { get; private set; } = 50;

        public void TurnLeft() => CurrentValue = (CurrentValue + 99) % 100;
        public void TurnRight() => CurrentValue = (CurrentValue + 101) % 100;
    }
}