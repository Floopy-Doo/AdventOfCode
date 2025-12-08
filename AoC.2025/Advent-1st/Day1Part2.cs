namespace AoC._2025.Advent_1st;

public static class Day1Part2
{
    public static int Solve(IReadOnlyCollection<string> input)
    {
        var operations = input.Select(InputParser.Parse);
        var state = operations.Aggregate(new State(50, 0), Apply);
        return state.PassedZero;
    }

    public static State Apply(State state, Operation operation)
    {
        var increments = operation switch
        {
            Operation.Left x => Enumerable.Repeat(-1, x.Value),
            Operation.Right x => Enumerable.Repeat(1, x.Value),
            _ => throw new InvalidOperationException("Unknown operation"),
        };
        var positions = increments.Aggregate([state.CurrentPosition], int[] (acc, value) => [((acc[0] + value) % 100 + 100) % 100, .. acc]);
        var clickedOnZero = positions.SkipLast(1).Count(x => x == 0);
        
        return new State(positions[0], state.PassedZero + clickedOnZero);
    }


    public sealed record State(int CurrentPosition, int PassedZero);
}