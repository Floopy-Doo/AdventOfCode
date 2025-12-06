namespace AcC._2025.Advent_1st;

public static class Day4
{
    public static decimal SolvePart1(string input)
    {
        var rawData = input
            .Split("\n")
            .SelectMany(ParseLine)
            .SelectMany(ExtendWithAdjacentIndicators);
        var positionWeights = rawData
            .Aggregate(new Dictionary<(X, Y), int>(), Reduce);
        return positionWeights
            .Count(x => x.Value > 4);
    }

    public static decimal SolvePart2(string input)
    {
        var rawData = input
            .Split("\n")
            .SelectMany(ParseLine);

        return Solve2Recursive([.. rawData]);
    }

    private static IEnumerable<Data.Barrel> ParseLine(string line, int x)
    {
        return line.SelectMany<char, Data.Barrel>((value, y) => value switch
        {
            '.' => [],
            '@' => [BarrelIndicator(x, y)],
            _ => throw new InvalidDataException($"Unknown char '{value}'"),
        });

        // static Data EmptyIndicator(int x, int y) => new Data.Empty(new X(x), new Y(y));
        static Data.Barrel BarrelIndicator(int x, int y) => new Data.Barrel(new X(x), new Y(y));
    }

    private static IEnumerable<Data> ExtendWithAdjacentIndicators(Data data)
    {
        return data switch
        {
            // Data.Empty => [data],
            Data.Barrel x => Enumerable
                .Range(0, 9)
                .Select(offsetSource => AdjacentIndicator(x.X, x.Y, offsetSource))
                .Append(x),
            Data.AdjacentIndicator => throw new InvalidOperationException("Should not contain adjacent inidicators"),
            _ => throw new InvalidOperationException($"exhaustivness of {data}"),
        };

        static Y CalcY(Y y,  int offsetSource) => new(offsetSource / 3 % 3 - 1 + y.Value);
        static X CalcX(X x, int  offsetSource) => new(offsetSource % 3 - 1 + x.Value);
        static Data AdjacentIndicator(X x, Y y, int value) => new Data.AdjacentIndicator(CalcX(x, value), CalcY(y, value));
    }

    private static decimal Solve2Recursive(IReadOnlyCollection<Data.Barrel> rawData)
    {
        var removablePositions = rawData
            .SelectMany(ExtendWithAdjacentIndicators)
            .Aggregate(new Dictionary<(X, Y), int>(), Reduce)
            .Where(x => x.Value > 4)
            .ToDictionary();
        var reducedBy = removablePositions.Count;
        if (reducedBy == 0) return 0;

        var reducedData = rawData
            .ExceptBy(removablePositions.Keys, barrel => (barrel.X, barrel.Y))
            .ToList();
        var innerReduction = Solve2Recursive(reducedData);
        return innerReduction + reducedBy;
    }

    private static Dictionary<(X X, Y Y), int> Reduce(Dictionary<(X X, Y Y), int> state, Data data)
    {
        state[(data.X, data.Y)] = state.GetValueOrDefault((data.X, data.Y)) + data.Value;
        return state;
    }

    private record X(int Value);

    private record Y(int Value);

    private abstract record Data(X X, Y Y, int Value)
    {
        // public sealed record Empty(X X, Y Y) : Data(X, Y, 0);

        public sealed record Barrel(X X, Y Y) : Data(X, Y, 9);

        public sealed record AdjacentIndicator(X X, Y Y) : Data(X, Y, -1);
    }
}