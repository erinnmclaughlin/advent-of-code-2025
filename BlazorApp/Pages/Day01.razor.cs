namespace BlazorApp.Pages;

public partial class Day01
{
    private const string Input = """
        L68
        L30
        R48
        L5
        R60
        L55
        L1
        L99
        R14
        L82
        """;

    public int CurrentDialNumber { get; set; } = 50;
    public double CurrentX { get; set; }
    public double CurrentY { get; set; }
    public string CurrentStep { get; set; } = "";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _ = ProcessInput();
        }
    }

    public async Task ProcessInput()
    {
        foreach (var line in Input.Split('\n').Select(l => l.Trim()))
        {
            CurrentStep = line;
            var increment = line[0] is 'L' ? -1 : 1;
            var ticks = int.Parse(line[1..]);

            for (var i = 0; i < ticks; i++)
            {
                CurrentDialNumber = (CurrentDialNumber + increment + 100) % 100;
                (CurrentX, CurrentY) = GetPosition(CurrentDialNumber);
                StateHasChanged();
                await Task.Delay(50);
                //if (current == 0) passThroughZeroCount++;
            }

            await Task.Delay(500);

            //if (current == 0) exactlyZeroCount++;
        }
    }

    private static (double X, double Y) GetPosition(int dialNumber)
    {
        var angle = (dialNumber / 100.0) * 2 * Math.PI - Math.PI / 2;

        const int center = 50;
        const int radius = 45; // adding some padding

        var x = center + radius * Math.Cos(angle);
        var y = center + radius * Math.Sin(angle);

        return (x, y);
    }
}