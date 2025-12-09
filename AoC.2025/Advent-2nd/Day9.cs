using System.Numerics;

namespace AoC._2025.Advent_2nd;

public static class Day9
{
    public static decimal SolvePart1(string input)
    {
        var points = input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(",", StringSplitOptions.RemoveEmptyEntries))
            .Select(line => new Point(decimal.Parse(line[1]), decimal.Parse(line[0])))
            .ToArray();

        var allPossibleAreas = AllPossibleRectanglesRecursive(points);
        return allPossibleAreas
            .Select(area => area.Value)
            .OrderByDescending(area => area)
            .First();
    }

    public static decimal SolvePart2(string input)
    {
        return 0;
    }

    private static IEnumerable<Area> AllPossibleRectanglesRecursive(Point[] corners)
    {
        if (corners.Length == 0) return [];

        var first = corners[0];
        var remainingCorners = corners[1..];
        var areas = remainingCorners
            .Select(second => new Area(
                first,
                second,
                (decimal.Abs(first.X - second.X) + 1) * (decimal.Abs(first.Y - second.Y) + 1)));

        return areas
            .Concat(AllPossibleRectanglesRecursive(remainingCorners));
    }

    private sealed record Point(decimal X, decimal Y);

    private sealed record Area(Point A, Point B, decimal Value);
}