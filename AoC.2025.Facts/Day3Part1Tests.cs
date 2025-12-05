using Shouldly;
using static AcC._2025.Day3Part1;

namespace AoC._2025.Facts;

public class Day3Part1Tests
{
    public static TheoryData<string, int> LineData = new()
    {
        { "987654321111111", 98 },
        { "811111111111119", 89 },
        { "234234234234278", 78 },
        { "818181911112111", 92 },
    };

    [Theory]
    [MemberData(nameof(LineData))]
    public void SingleLineSolveData(string input, int expected)
    {
        var result = Solve(input);
        result.ShouldBe(expected);
    }


    [Fact]
    public void ShouldSolveSample()
    {
        var input = "987654321111111\n811111111111119\n234234234234278\n818181911112111\n";
        var result = Solve(input);
        result.ShouldBe(357);
    }

    [Fact]
    public void ShouldDay3Input()
    {
        var result = Solve(AocInput.Day3Input);
        result.ShouldBe(17166);
    }
}