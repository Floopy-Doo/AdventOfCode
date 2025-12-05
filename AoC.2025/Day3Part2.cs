namespace AcC._2025;

public static class Day3Part2
{
    public static decimal Solve(string input)
    {
        return input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(Parse)
            .Select(SolveLine)
            .Sum();
    }

    private static decimal SolveLine(Battery[] line)
    {
        var batteries = SolveRecursive(12, line);

        var finalNumber = batteries
            .OrderBy(x => x.Position)
            .Aggregate(string.Empty, (acc, battery) => $"{acc}{battery.Jolt}");

        return decimal.Parse(finalNumber);
    }

    private static IEnumerable<Battery> SolveRecursive(int sampleSize, Battery[] remaining)
    {
        if (remaining.Length == 0 || sampleSize == 0) return [];

        var selectionSize = remaining.Length - sampleSize + 1;
        var selection = remaining
            .Select((x, i) => (Data: x, Index: i))
            .Take(selectionSize);
        var highestBatteryInSample = selection
            .OrderByDescending(x => x.Data.Jolt)
            .First();
        var otherHighest = SolveRecursive(sampleSize - 1, remaining.Skip(highestBatteryInSample.Index + 1).ToArray());

        return otherHighest.Append(highestBatteryInSample.Data);
    }

    private static Battery[] Parse(string line)
    {
        return line.Select((jolt, index) => new Battery(decimal.Parse([jolt]), index)).ToArray();
    }

    private record Battery(decimal Jolt, int Position);
}