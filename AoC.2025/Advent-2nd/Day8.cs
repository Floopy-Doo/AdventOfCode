using System.Numerics;
using System.Text.RegularExpressions;

namespace AoC._2025.Advent_2nd;

public static partial class Day8
{
    public static decimal SolvePart1(int relevantCount, string input)
    {
        var junctionBoxes = ParseIntoVectors(input);
        var directCircuits = ConnectJunctionBoxesRecursive(junctionBoxes);
        var relevantDirectCircuits = directCircuits
            .OrderBy(x => x.Distance)
            .Take(relevantCount);

        var circuits = relevantDirectCircuits.Aggregate(
            (Vector3[][])[],
            (lookup, tuple) => JoinCircuit(lookup, tuple.First, tuple.Second));
        var biggestCircuits = circuits
            .OrderByDescending(x => x.Length)
            .Take(3)
            .Select(x => Convert.ToDecimal(x.Length));


        return biggestCircuits.Aggregate((acc, lenght) => acc * lenght);
    }

    private static IEnumerable<DirectCircuit> ConnectJunctionBoxesRecursive(Vector3[] unconnectedJunctionBoxes)
    {
        if (unconnectedJunctionBoxes.Length == 0) return [];

        var first = unconnectedJunctionBoxes[0];
        var remainingUnconnectedJunctionBoxes = unconnectedJunctionBoxes[1..];
        var directCircuitsWithThisBox = remainingUnconnectedJunctionBoxes
            .Select(second => new DirectCircuit(first, second, Convert.ToDecimal(Vector3.DistanceSquared(first, second))));

        return directCircuitsWithThisBox
            .Concat(ConnectJunctionBoxesRecursive(remainingUnconnectedJunctionBoxes));
    }

    public static Vector3[][] JoinCircuit(Vector3[][] circuits, Vector3 first, Vector3 second)
    {
        var existingCircuitForFirst = circuits.SingleOrDefault(x => x.Contains(first), [first]);
        var existingCircuitForSecond = circuits.SingleOrDefault(x => x.Contains(second), [second]);

        if (existingCircuitForFirst == existingCircuitForSecond) return circuits;

        return circuits
            .Except([existingCircuitForFirst])
            .Except([existingCircuitForSecond])
            .Append([.. existingCircuitForFirst, .. existingCircuitForSecond])
            .ToArray();
    }

    private static Vector3[] ParseIntoVectors(string input)
    {
        return input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => SplitValues().Match(x))
            .Select(x => new Vector3(float.Parse(x.Groups[1].Value),  float.Parse(x.Groups[2].Value), float.Parse(x.Groups[3].Value)))
            .ToArray();
    }

    [GeneratedRegex(@"(\d+),(\d+),(\d+)")]
    private static partial Regex SplitValues();

    private sealed record DirectCircuit(Vector3 First, Vector3 Second, decimal Distance);
}