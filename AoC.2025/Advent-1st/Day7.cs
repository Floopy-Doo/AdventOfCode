using static AcC._2025.Advent_1st.Day7.Propagation;

namespace AcC._2025.Advent_1st;

public static class Day7
{
    public static decimal SolvePart1(string input)
    {
        var lines = input.Split("\n");
        var initialBeams = lines[0].Select(IsBeam);

        var state = lines[1..].Aggregate(
            new State([.. initialBeams], 0),
            EvaluateBeamPropagation);

        return state.TotalSplits;

        static State EvaluateBeamPropagation(State state, string line)
        {
            var fullPropagation = line.Zip(state.ActiveBeams)
                .SelectMany(ApplyBeamCalculation)
                .ToList();

            var newBeams = fullPropagation.GroupBy(x => x.Index,
                    (i, group) => (Index: i,
                        WithBeam: group.Aggregate(false, (acc, data) => acc || data is not NoBeam)))
                .Where(x => x.Index >= 0 && x.Index < line.Length)
                .OrderBy(x => x.Index)
                .Select(x => x.WithBeam)
                .ToArray();

            var newSplits = fullPropagation.Count(x => x is SplitBeam) / 2;
            return new State(newBeams, state.TotalSplits + newSplits);
        }
    }

    private static IEnumerable<Propagation> ApplyBeamCalculation(
        (char input, bool hasActiveBeam) data,
        int index)
    {
        return (data.hasActiveBeam, data.input) switch
        {
            (true, '^') =>
                [new SplitBeam(index - 1), new NoBeam(index), new SplitBeam(index + 1)],
            (true, '.') =>
                [new Beam(index)],
            _ => [new NoBeam(index)],
        };
    }

    private static bool IsBeam(char input)
    {
        return input == 'S';
    }

    private sealed record State(bool[] ActiveBeams, decimal TotalSplits);

    public abstract record Propagation(int Index)
    {
        public sealed record Beam(int Index) : Propagation(Index);

        public sealed record SplitBeam(int Index) : Propagation(Index);

        public sealed record NoBeam(int Index) : Propagation(Index);
    }
}