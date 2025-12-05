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

    private bool IsRunning { get; set; }

    public string AudioFile { get; set; } = "";
    public int CurrentDialNumber { get; set; } = 50;
    public double CurrentX { get; set; }
    public double CurrentY { get; set; }
    public string CurrentStep { get; set; } = "";


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Reset();
            StateHasChanged();
        }
    }

    public async Task ProcessInput()
    {
        IsRunning = true;

        foreach (var line in Input.Split('\n').Select(l => l.Trim()))
        {
            if (!IsRunning) return;

            CurrentStep = line;
            var increment = line[0] is 'L' ? -1 : 1;
            var ticks = int.Parse(line[1..]);

            for (var i = 0; i < ticks; i++)
            {
                if (!IsRunning) return;

                AudioFile = string.IsNullOrEmpty(AudioFile) ? "sfx/ui-soundpack/Abstract1.wav" : "";
                CurrentDialNumber = (CurrentDialNumber + increment + 100) % 100;
                (CurrentX, CurrentY) = GetPosition(CurrentDialNumber);
                StateHasChanged();

                if (i == ticks - 1)
                    break;

                if (CurrentDialNumber == 0)
                {
                    AudioFile = "sfx/ui-soundpack/Retro9.wav";
                    StateHasChanged();
                    await Task.Delay(100);
                }
                else
                {
                    StateHasChanged();
                    await Task.Delay(50);
                }
            }

            if (CurrentDialNumber == 0)
            {
                AudioFile = "sfx/ui-soundpack/Retro10.wav";
            }

            StateHasChanged();
            await Task.Delay(1000);

            AudioFile = "";

        }
    }

    private void Reset()
    {
        AudioFile = string.Empty;
        CurrentDialNumber = 50;
        (CurrentX, CurrentY) = GetPosition(CurrentDialNumber);
        CurrentStep = string.Empty;
        IsRunning = false;
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