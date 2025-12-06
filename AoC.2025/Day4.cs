using System.Collections;

namespace AcC._2025;

public static class Day4
{
    public static decimal SolvePart1(string input)
    {
        var rawData = input
            .Split("\n")
            .SelectMany(Parse);
        var positionWeights = rawData
            .Aggregate(new Dictionary<(X, Y), int>(), Reduce);
        return positionWeights
            .Count(x => x.Value > 4);
    }

    private static IEnumerable<Data> Parse(string line, int x)
    {
        return line.SelectMany((value, y) => value switch
        {
            '.' => [EmptyIndicator(x, y)],
            '@' => Enumerable.Range(0, 9).Select(offsetSource => AdjacentIndicator(x, y, offsetSource)).Append(BarrelIndicator(x, y)),
            _ => throw new InvalidDataException($"Unknown char '{value}'"),
        });

        static Y CalcY(int y,  int offsetSource) => new(offsetSource / 3 % 3 - 1 + y);
        static X CalcX(int x, int  offsetSource) => new(offsetSource % 3 - 1 + x);
        static Data AdjacentIndicator(int x, int y, int value) => new(CalcX(x, value), CalcY(y, value), -1);
        static Data EmptyIndicator(int x, int y) => new(new X(x), new Y(y), 0);
        static Data BarrelIndicator(int x, int y) => new(new X(x), new Y(y), 9);
    }

    private static Dictionary<(X X, Y Y), int> Reduce(Dictionary<(X X, Y Y), int> state, Data data)
    {
        state[(data.X, data.Y)] = state.GetValueOrDefault((data.X, data.Y)) + data.Value;
        return state;
    }

    private record X(int Value);

    private record Y(int Value);

    private record Data(X X, Y Y, int Value);

}