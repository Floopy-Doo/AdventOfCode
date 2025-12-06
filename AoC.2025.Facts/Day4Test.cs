using Shouldly;
using static AcC._2025.Day4;

namespace AoC._2025.Facts;

public class Day4Test
{
    public static TheoryData<string, decimal> SampleDataPart1 = new()
    {
        { "...\n.@.\n...", 1 },
        { "@..\n.@.\n...", 2 },
        { "@@.\n.@.\n...", 3 },
        { "@@@\n.@.\n...", 4 },
        { "@@@\n@@.\n...", 3 },
        { "@@@\n@@@\n...", 4 },
        { "@@@\n@@@\n@..", 4 },
        { "@@@\n@@@\n@@.", 3 },
        { "@@@\n@@@\n@@@", 4 },
        { ".@@\n@@@\n@@@", 3 },
        { "..@\n@@@\n@@@", 4 },
        { "...\n@@@\n@@@", 4 },
        { "...\n.@@\n@@@", 3 },
        { "...\n.@.\n@@@", 4 },
        { "...\n.@.\n.@@", 3 },
        { "...\n.@.\n..@", 2 },
        { "@.@\n.@.\n@.@", 4 },
        { ".@.\n@@.\n.@.", 4 },
        { "@..\n@@@\n..@", 4 },
        { "..@\n.@.\n@..", 3 },
        { "@@@\n.@.\n@@@", 6 },
        { ".@.\n.@.\n.@.", 3 },
        { "@@.\n.@@\n...", 4 },
        { "...\n@@.\n.@@", 4 },
        { "@..\n.@@\n@@.", 4 },
        { ".@@\n.@.\n@@.", 4 },
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
        var input = "..@@.@@@@.\n@@@.@.@.@@\n@@@@@.@.@@\n@.@@@@..@.\n@@.@@@@.@@\n.@@@@@@@.@\n.@.@.@.@@@\n@.@@@.@@@@\n.@@@@@@@@.\n@.@.@@@.@.\n";
        var result = SolvePart1(input);
        result.ShouldBe(13);
    }

    [Fact]
    public void ShouldFIndPart1Solution()
    {
        var result = SolvePart1(AocInput.Day4Input);
        result.ShouldBe(1486m);
    }
}