using System.Numerics;
using System.Text;

namespace AoC._2025.Advent_2nd;

public static class Day9
{
    public static decimal SolvePart1(string input)
    {
        var points = ParsePoints(input);

        var allPossibleAreas = AllPossibleRectanglesRecursive(points);
        return allPossibleAreas
            .Select(area => area.Value)
            .OrderByDescending(area => area)
            .First();
    }

    public static decimal SolvePart2(string input)
    {
        var corners = ParsePoints(input);
        var allPossibleRectangles = AllPossibleRectanglesRecursive(corners);

        var edges = CompileEdgesRecursive(corners[0], corners[1..], corners[0]).ToList();

        var includedRectangle = allPossibleRectangles
            .Where(area => !edges.Any(point => IsInArea(point, area)));
        return includedRectangle
            .Select(area => area.Value)
            .OrderByDescending(area => area)
            .First();
    }

    private static IEnumerable<Point> CompileEdgesRecursive(Point current, Point[] remaining, Point initial)
    {
        if (remaining.Length == 0) return GetEdges(current, DistanceX(initial, current), DistanceY(initial, current));

        var closestCorner = remaining
            .Where(p => p.X == current.X || p.Y == current.Y)
            .Select(p => (Point: p, DistanceX: DistanceX(p, current), DistanceY: DistanceY(p, current)))
            .OrderBy(p => int.Abs(p.DistanceX + p.DistanceY))
            .FirstOrDefault();

        var edges = GetEdges(current, closestCorner.DistanceX, closestCorner.DistanceY);
        return edges
            .Concat(CompileEdgesRecursive(
                closestCorner.Point,
                remaining.Except([closestCorner.Point]).ToArray(),
                initial));

        static IEnumerable<Point> GetEdges(Point current, int distanceX, int distanceY) =>
            Enumerable
                .Range(1, int.Max(int.Abs(distanceX), int.Abs(distanceY)) - 1)
                .Select(i => new Point(
                    current.X + i * Math.Sign(distanceX), current.Y + i * Math.Sign(distanceY)));
        int DistanceX(Point a, Point b) => a.X - b.X;
        int DistanceY(Point a, Point b) => a.Y - b.Y;
    }

    public static decimal SolvePart2BruteForce(string input)
    {
        var cornerPoints = ParsePoints(input);
        var allPossibleRectangles = AllPossibleRectanglesRecursive(cornerPoints);

        var maxX = cornerPoints.Max(point => point.X);
        var minX = cornerPoints.Min(point => point.X);
        var maxY = cornerPoints.Max(point => point.Y);
        var minY = cornerPoints.Min(point => point.Y);

        var edgePoints = new List<Point>((maxY - minY) * (maxX - minY));

        var startCornerX = new List<Point>(maxX - minY);
        for (var iX = minX; iX <= maxX; iX++)
        {
            Point? startCornerY = null;
            for (var iY = minY; iY <= maxY; iY++)
            {
                var possibleEdge = new Point(iX, iY);
                var isCorner = cornerPoints.Contains(possibleEdge);
                var hasActiveYEdge = startCornerY is not null;
                var activeXEdgeStart = startCornerX.SingleOrDefault(p => p?.Y == possibleEdge.Y, null);

                startCornerY = (isCorner, hasActiveYEdge) switch
                {
                    (true, true) => null,
                    (true, false) => possibleEdge,
                    (false, _) => startCornerY,
                };

                if (isCorner && activeXEdgeStart is {} x) startCornerX.Remove(x);
                if (isCorner && activeXEdgeStart is null) startCornerX.Add(possibleEdge);
                if (!isCorner && (activeXEdgeStart is not null || hasActiveYEdge)) edgePoints.Add(possibleEdge);
            }
        }

        var includedRectangle = allPossibleRectangles
            .Where(area => !edgePoints.Any(point => IsInArea(point, area)));
        return includedRectangle
            .Select(area => area.Value)
            .OrderByDescending(area => area)
            .First();
    }

    private static bool IsInArea(Point point, Area area)
    {
        var withinX = decimal.Min(area.A.X, area.B.X) < point.X && point.X < decimal.Max(area.A.X, area.B.X);
        var withinY = decimal.Min(area.A.Y, area.B.Y) < point.Y && point.Y < decimal.Max(area.A.Y, area.B.Y);
        return withinX && withinY;
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

    private static Point[] ParsePoints(string input)
    {
        return input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(",", StringSplitOptions.RemoveEmptyEntries))
            .Select(line => new Point(int.Parse(line[1]), int.Parse(line[0])))
            .ToArray();
    }

    private sealed record Point(int X, int Y);

    private sealed record Area(Point A, Point B, decimal Value);
}