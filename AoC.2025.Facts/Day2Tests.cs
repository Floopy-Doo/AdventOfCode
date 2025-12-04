using AcC._2025;
using Shouldly;
using static AcC._2025.Day2;

namespace AoC._2025.Facts;

public class Day2Tests
{
    public static TheoryData<string[], int> TestData = new()
    {
        { ["L68", "L30", "R48", "L5", "R60", "L55", "L1", "L99", "R14", "L82"], 6 },
    };
    
    public static TheoryData<State, Operation, State> ApplyOperationData = new()
    {
        { new State(50, 0), new Operation.Left(68), new State(82, 1) },
        { new State(82, 1), new Operation.Left(30), new State(52, 1) },
        { new State(52, 1), new Operation.Right(48), new State(0, 2) },
        { new State(0, 2), new Operation.Left(5), new State(95, 2) },
        { new State(95, 2), new Operation.Right(60), new State(55, 3) },
        { new State(55, 3), new Operation.Left(55), new State(0, 4) },
        { new State(0, 4), new Operation.Left(1), new State(99, 4) },
        { new State(99, 4), new Operation.Left(99), new State(0, 5) },
        { new State(0, 5), new Operation.Right(14), new State(14, 5) },
        { new State(14, 5), new Operation.Left(82), new State(32, 6) },
    };

    public static TheoryData<State, Operation, State> EdgeCases = new()
    {
        { new State(0, 0), new Operation.Left(150), new State(50, 1) },
        { new State(0, 0), new Operation.Left(1000), new State(0, 10) },
        { new State(50, 0), new Operation.Right(150), new State(0, 2) },
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
    [MemberData(nameof(EdgeCases))]
    public void ShouldApplyTheOperationToTheCurrentRotation(State initial, Operation operation, State expected)
    {
        var result = Apply(initial, operation);
        result.ShouldBe(expected);
    }

    [Fact]
    public void ShouldRunDay1Input()
    {
        var lines = AocInput.Day1Input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var result = Solve(lines);
        result.ShouldBe(6166);
    }
}