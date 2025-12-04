namespace AdventOfCode2025.Runner.Day01;

public sealed class SolverTests
{
    private static DirectoryInfo BaseDirectory { get; } = new(Path.Combine("Day01", "inputs"));

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void CSharpTests(string inputDirectory, int part)
    {
        TestSolver(new CSharp.Day01(), inputDirectory, part);
    }
    
    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void FSharpTests(string inputDirectory, int part)
    {
        TestSolver(new FSharp.Day01(), inputDirectory, part);
    }

    private static void TestSolver(ISolver solver, string inputDirectory, int part)
    {
        var inputFile = GetInputFile(inputDirectory);
        var solution = solver.Solve(inputFile);
        
        Assert.Equal(GetExpectedSolution(inputDirectory, part), part == 1? solution.PartOne : solution.PartTwo);
    }
    
    public static TheoryData<string, int> GetTestCases() => new(
        BaseDirectory.GetDirectories().SelectMany(d => Enumerable.Range(1, 2).Select(i => new TheoryDataRow<string, int>(d.FullName, i)))
    );
    
    private static string GetExpectedSolution(string directory, int part)
    {
        var filePath = Path.Combine(BaseDirectory.FullName, directory, $"solution_part{part}.txt");
        return File.ReadAllText(filePath);
    }

    private static FileInfo GetInputFile(string directory)
    {
        var filePath = Path.Combine(BaseDirectory.FullName, directory, "sample.txt");
        return new FileInfo(filePath);
    }
}