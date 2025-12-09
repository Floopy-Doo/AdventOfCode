using Shouldly;
using static AoC._2025.Advent_2nd.Day7;

namespace AoC._2025.Facts.Advent_2nd;

public class Day7Test
{
    public static TheoryData<string, decimal> SampleDataPart1 = new()
    {
        { ".S.\n", 0 },
        { ".S.\n...\n", 0 },
        { ".S.\n...\n.^.\n", 1 },
        { ".S.\n.^.\n", 1 },
        { ".S.\n...\n.^.\n^.^\n", 3 },
        { ".S.\n...\n.^.\n.^.\n", 1 },
        { "..S..\n..^..\n.^.^.\n^^^^^\n", 6 },
    };

    public static TheoryData<string, decimal> SampleDataPart2 = new()
    {
        { ".S.\n", 1 },
        { ".S.\n...\n", 1 },
        { ".S.\n...\n.^.\n", 2 },
        { ".S.\n.^.\n", 2 },
        { ".S.\n...\n.^.\n^.^\n", 2 },
        { ".S.\n...\n.^.\n.^.\n", 2 },
        { "..S..\n..^..\n.^.^.\n^^^^^\n", 6 },
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
        var input = ".......S.......\n...............\n.......^.......\n...............\n......^.^......\n...............\n.....^.^.^.....\n...............\n....^.^...^....\n...............\n...^.^...^.^...\n...............\n..^...^.....^..\n...............\n.^.^.^.^.^...^.\n...............";
        var result = SolvePart1(input);
        result.ShouldBe(21);
    }

    [Fact]
    public void ShouldFindPart1Solution()
    {
        var result = SolvePart1(AocInput.Day7Input);
        result.ShouldBe(1675);
    }

    [Theory]
    [MemberData(nameof(SampleDataPart2))]
    public void ShouldSolvePar2(string input, decimal expected)
    {
        var result = SolvePart2(input);
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldSolvePart2FullSample()
    {
        var input = ".......S.......\n...............\n.......^.......\n...............\n......^.^......\n...............\n.....^.^.^.....\n...............\n....^.^...^....\n...............\n...^.^...^.^...\n...............\n..^...^.....^..\n...............\n.^.^.^.^.^...^.\n...............";
        var result = SolvePart2(input);
        result.ShouldBe(40);
    }

    [Fact]
    public void ShouldFindPart2Solution()
    {
        var result = SolvePart2(AocInput.Day7Input);
        result.ShouldBe(187987920774390m);
    }
}