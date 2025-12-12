using Shouldly;
using static AoC._2025.Advent_2nd.Day11;
using static AoC._2025.Facts.Advent_2nd.AocInput;

namespace AoC._2025.Facts.Advent_2nd;

public class Day11Test
{
    public static TheoryData<string, decimal> SampleDataPart1 = new()
    {
        { "you: bbb ccc\nbbb: out\nccc: out\n", 2 },
        { "you: bbb ccc ddd\nbbb: out\nccc: out\nddd: eee\neee: out\n", 3 },
        { "you: bbb ccc ddd\nbbb: eee ccc\nccc: out\nddd: eee\neee: out\n", 4 },
    };

    public static TheoryData<string, decimal> SampleDataPart2 = new()
    {
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
        var input = "aaa: you hhh\nyou: bbb ccc\nbbb: ddd eee\nccc: ddd eee fff\nddd: ggg\neee: out\nfff: out\nggg: out\nhhh: ccc fff iii\niii: out\n";
        var result = SolvePart1(input);
        result.ShouldBe(5m);
    }

    [Fact]
    public void ShouldFindPart1Solution()
    {
        var result = SolvePart1(Day10Input);
        result.ShouldBe(-1m);
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
        var input = "aaa: you hhh\nyou: bbb ccc\nbbb: ddd eee\nccc: ddd eee fff\nddd: ggg\neee: out\nfff: out\nggg: out\nhhh: ccc fff iii\niii: out\n";
        var result = SolvePart2(input);
        result.ShouldBe(-1m);
    }

    [Fact]
    public void ShouldFindPart2Solution()
    {
        var result = SolvePart2(Day10Input);
        result.ShouldBe(-1m);
    }
}