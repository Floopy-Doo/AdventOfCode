namespace AcC._2025;

public static class Day1
{
    public static int Solve(IReadOnlyCollection<string> input)
    {
        var operations = input.Select(InputParser.Parse).ToList();
        var rotations = operations.Aggregate(new Rotations(50, 0), Apply);
        return rotations.ReachingZero;
    }

    public static Rotations Apply(Rotations arg1, Operation arg2)
    {
        var newRotation = arg2 switch
        {
            Operation.Left x => arg1.Current - x.Value,
            Operation.Right x => arg1.Current + x.Value,
            _ => throw new InvalidOperationException("Unknown operation"),
        };
        var newRotationCorrectedForCircle = (newRotation % 100 + 100) % 100;
        var newZeroCount = arg1.ReachingZero + (newRotationCorrectedForCircle == 0 ? 1 : 0);

        return new Rotations(newRotationCorrectedForCircle, newZeroCount);
    }


    public record Rotations(int Current, int ReachingZero);
}