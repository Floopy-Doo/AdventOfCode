using System.Text.RegularExpressions;

namespace AcC._2025;

public static partial class Day1
{
    public static int Solve(IReadOnlyCollection<string> input)
    {
        var operations = input.Select(Parse).ToList();
        var rotations = operations.Aggregate(new Rotations(50, 0), Apply);
        return rotations.ReachingZero;
    }

    public static Rotations Apply(Rotations arg1, Operations arg2)
    {
        var newRotation = arg2 switch
        {
            Operations.Left x => (arg1.Current - x.Value),
            Operations.Right x => (arg1.Current + x.Value),
            _ => throw new InvalidOperationException("Unknown operation")
        };
        var newRotationCorrectedForCircle = (newRotation % 100 + 100) % 100;
        var newZeroCount = arg1.ReachingZero + (newRotationCorrectedForCircle == 0 ? 1 : 0);

        return new Rotations(newRotationCorrectedForCircle, newZeroCount);
    }

    public static Operations Parse(string action)
    {
        var result = ActionRegex().Match(action);
        if (!result.Success)
        {
            throw new InvalidOperationException($"Unable to parse {action}");
        }

        var direction = result.Groups[1].Value;
        var distance = result.Groups[2].Value;

        return direction switch
        {
            "R" => new Operations.Right(int.Parse(distance)),
            "L" => new Operations.Left(int.Parse(distance)),
            _ => throw new InvalidOperationException($"Unknown action {direction} - {distance}")
        };
    }

    public abstract record Operations
    {
        public sealed record Left(int Value) : Operations;
        public sealed record Right(int Value) : Operations;
    }


    public record Rotations(int Current, int ReachingZero);

    [GeneratedRegex(@"^([RL])(\d+)$")]
    private static partial Regex ActionRegex();
}

