namespace AdventOfCode2025.CSharp;

public interface ICSharpSolver : ISolver;

public interface ICSharpSolver<T> : ICSharpSolver, ISolver<T> where T : ISolver<T>, new();