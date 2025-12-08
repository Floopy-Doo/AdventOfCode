using Shouldly;
using static AoC._2025.Advent_1st.Day3Part2;

namespace AoC._2025.Facts.Advent_1st;

public class Day3Part2Tests
{
    public static TheoryData<string, decimal> LineData = new()
    {
        { "987654321111111", 987654321111m },
        { "811111111111119", 811111111119m },
        { "234234234234278", 434234234278m },
        { "818181911112111", 888911112111m },
    };

    [Theory]
    [MemberData(nameof(LineData))]
    public void SingleLineSolveData(string input, decimal expected)
    {
        var result = Solve(input);
        result.ShouldBe(expected);
    }


    [Fact]
    public void ShouldSolveSample()
    {
        var input = "987654321111111\n811111111111119\n234234234234278\n818181911112111\n";
        var result = Solve(input);
        result.ShouldBe(3121910778619m);
    }

    [Fact]
    public void ShouldDay3Input()
    {
        var result = Solve(AocInput.Day3Input);
        result.ShouldBe(169077317650774m);
    }
}