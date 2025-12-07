using Shouldly;
using static AcC._2025.Advent_1st.Day7;

namespace AoC._2025.Facts.Advent_1st;

public class Day7Test
{
    public static TheoryData<string, decimal> SampleDataPart1 = new()
    {
        { ".S.\n", 0 },
        { ".S.\n..\n.", 0 },
        { ".S.\n...\n.^.\n", 1 },
        { ".S.\n.^.\n", 1 },
        { ".S.\n...\n.^.\n^.^\n", 3 },
        { ".S.\n...\n.^.\n.^.\n", 1 },
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
}