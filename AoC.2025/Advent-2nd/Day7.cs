using static AoC._2025.Advent_2nd.Day7.Propagation;

namespace AoC._2025.Advent_2nd;

public static class Day7
{
    public static decimal SolvePart1(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var initialBeams = lines[0].Select(IsBeam);

        var state = lines[1..].Aggregate(
            new State([.. initialBeams], 0),
            EvaluateBeamPropagation);

        return state.TotalSplits;
    }

    public static decimal SolvePart2(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var initialBeams = lines[0].Select(IsBeam);

        var state = lines[1..].Aggregate(
            new State([.. initialBeams], 0),
            EvaluateBeamPropagation);

        return state.ParticlesInBeams.Sum();
    }


    private static State EvaluateBeamPropagation(State state, string line)
    {
        var fullPropagation = line.Zip(state.ParticlesInBeams)
            .SelectMany(ApplyBeamCalculation)
            .ToList();

        var newBeams = fullPropagation
            .GroupBy(x => x.Index,
                (i, group) =>
                (
                    Index: i,
                    ActiveParticles: group.Aggregate(0m, (acc, data) => acc + data.ActiveParticles))
            )
            .Where(x => x.Index >= 0 && x.Index < line.Length)
            .OrderBy(x => x.Index)
            .Select(x => x.ActiveParticles)
            .ToArray();

        var newSplits = fullPropagation.Count(x => x is SplitBeam) / 2;
        return new State(newBeams, state.TotalSplits + newSplits);
    }

    private static IEnumerable<Propagation> ApplyBeamCalculation(
        (char Input, decimal ActiveParticles) data,
        int index)
    {
        return (data.ActiveParticles, data.Input) switch
        {
            (> 0, '^') =>
            [
                new SplitBeam(index - 1, data.ActiveParticles),
                new NoBeam(index),
                new SplitBeam(index + 1, data.ActiveParticles),
            ],
            (> 0, '.') =>
                [new Beam(index, data.ActiveParticles)],
            _ => [new NoBeam(index)],
        };
    }

    private static decimal IsBeam(char input)
    {
        return input == 'S' ? 1 : 0;
    }

    private sealed record State(decimal[] ParticlesInBeams, decimal TotalSplits);

    public abstract record Propagation(int Index, decimal ActiveParticles)
    {
        public sealed record Beam(int Index, decimal ActiveParticles) : Propagation(Index, ActiveParticles);

        public sealed record SplitBeam(int Index, decimal ActiveParticles) : Propagation(Index, ActiveParticles);

        public sealed record NoBeam(int Index) : Propagation(Index, 0m);
    }
}