using Shouldly;
using static AoC._2025.Advent_2nd.Day9;
using static AoC._2025.Facts.Advent_2nd.AocInput;

namespace AoC._2025.Facts.Advent_2nd;

public class Day9Test
{
    public static TheoryData<string, decimal> SampleDataPart1 = new()
    {
        { "2,5\n9,7\n", 24 },
        { "7,1\n11,7\n", 35 },
        { "7,3\n2,3\n", 6 },
    };

    public static TheoryData<string, decimal> SampleDataPart2 = new()
    {
        { "2,5\n9,7\n", 24 },
        { "7,1\n11,7\n", 35 },
        { "7,3\n2,3\n", 6 },
    };

    [Theory]
    [MemberData(nameof(SampleDataPart1))]
    public void ShouldSolvePart1(string input, decimal expected)
    {
        var result = SolvePart1(input);
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldSolvePart1FullSample()
    {
        var input = "7,1\n11,1\n11,7\n9,7\n9,5\n2,5\n2,3\n7,3\n";
        var result = SolvePart1(input);
        result.ShouldBe(50m);
    }

    [Fact]
    public void ShouldFindPart1Solution()
    {
        var result = SolvePart1(Day9Input);
        result.ShouldBe(4759930955m);
    }

    [Theory]
    [MemberData(nameof(SampleDataPart2))]
    public void ShouldSolvePart2(string input, decimal expected)
    {
        var result = SolvePart2(input);
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldSolvePart2FullSample()
    {
        var input = "7,1\n11,1\n11,7\n9,7\n9,5\n2,5\n2,3\n7,3\n";
        var result = SolvePart2(input);
        result.ShouldBe(50m);
    }

    [Fact]
    public void ShouldFindPart2Solution()
    {
        var result = SolvePart2(Day9Input);
        result.ShouldBe(1675);
    }
}