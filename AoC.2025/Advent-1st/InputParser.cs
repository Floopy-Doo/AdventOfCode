using System.Text.RegularExpressions;

namespace AoC._2025.Advent_1st;

public static partial class InputParser
{
    public static Operation Parse(string action)
    {
        var result = ActionRegex().Match(action);
        if (!result.Success) throw new InvalidOperationException($"Unable to parse {action}");

        var direction = result.Groups[1].Value;
        var distance = result.Groups[2].Value;

        return direction switch
        {
            "R" => new Operation.Right(int.Parse(distance)),
            "L" => new Operation.Left(int.Parse(distance)),
            _ => throw new InvalidOperationException($"Unknown action {direction} - {distance}"),
        };
    }


    [GeneratedRegex(@"^([RL])(\d+)$")]
    private static partial Regex ActionRegex();
}

public abstract record Operation
{
    public sealed record Left(int Value) : Operation;

    public sealed record Right(int Value) : Operation;
}