using AdventOfCode2025.CSharp;

namespace AdventOfCode2025.Runner;

[Trait("Category", "CSharp")]
public abstract class CSharpSolverTestBase<T> : SolverTestBase<T> where T : ISolver<T>, new();

[Trait("Category", "FSharp")]
public abstract class FSharpSolverTestBase<T> : SolverTestBase<T> where T : ISolver<T>, new();

public abstract class SolverTestBase<T> where T : ISolver<T>, new()
{
    private static DirectoryInfo BaseDirectory => new(Path.Combine($"Day{T.DayNumber:D2}", "inputs"));
    
    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void TestSolver(string inputDirectory, int part)
    {
        var inputFile = GetInputFile(inputDirectory);
        var solution = new T().Solve(inputFile);
        
        Assert.Equal(GetExpectedSolution(inputDirectory, part), part == 1? solution.PartOne : solution.PartTwo);
    }
    
    public static TheoryData<string, int> GetTestCases() => new(
        BaseDirectory
            .GetDirectories()
            .SelectMany(d => Enumerable.Range(1, 2).Select(i => new TheoryDataRow<string, int>(d.FullName, i)))
    );
    
    private string GetExpectedSolution(string directory, int part)
    {
        var filePath = Path.Combine(BaseDirectory.FullName, directory, $"solution_part{part}.txt");
        return File.ReadAllText(filePath);
    }

    private static FileInfo GetInputFile(string directory)
    {
        var filePath = Path.Combine(BaseDirectory.FullName, directory, "input.txt");
        
        if (!File.Exists(filePath))
            filePath = Path.Combine(BaseDirectory.FullName, directory, "sample.txt");
        
        return new FileInfo(filePath);
    }
}