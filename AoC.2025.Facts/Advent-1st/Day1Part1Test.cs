using AcC._2025.Advent_1st;
using Shouldly;
using static AcC._2025.Advent_1st.Day1Part1;

namespace AoC._2025.Facts.Advent_1st;

public class Day1Part1Test
{
    public static TheoryData<string[], int> TestData = new()
    {
        { ["L68", "L30", "R48", "L5", "R60", "L55", "L1", "L99", "R14", "L82"], 3 },
    };

    public static TheoryData<Rotations, Operation, Rotations> ApplyOperationData = new()
        {
            { new Rotations(50, 0), new Operation.Left(68), new Rotations(82, 0) },
            { new Rotations(82, 0), new Operation.Left(30), new Rotations(52, 0) },
            { new Rotations(52, 0), new Operation.Right(48), new Rotations(0, 1) },
            { new Rotations(0, 1), new Operation.Left(5), new Rotations(95, 1) },
            { new Rotations(95, 1), new Operation.Right(60), new Rotations(55, 1) },
            { new Rotations(55, 1), new Operation.Left(55), new Rotations(0, 2) },
            { new Rotations(0, 2), new Operation.Left(1), new Rotations(99, 2) },
            { new Rotations(99, 2), new Operation.Left(99), new Rotations(0, 3) },
            { new Rotations(0, 3), new Operation.Right(14), new Rotations(14, 3) },
            { new Rotations(14, 3), new Operation.Left(82), new Rotations(32, 3) },
    };

    [Theory]
    [MemberData(nameof(TestData))]
    public void ShouldEvaluateTheCorrectAmountOfZeros(string[] input, int expected)
    {
        var result = Solve(input);
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(ApplyOperationData))]
    public void ShouldApplyTheOperationToTheCurrentRotation(Rotations initial, Operation operation, Rotations expected)
    {
        var result = Apply(initial, operation);
        result.ShouldBe(expected);
    }

    [Fact]
    public void ShouldRunDay1Input()
    {
        var lines = AocInput.Day1Input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var result = Solve(lines);
        result.ShouldBe(1034);
    }
}