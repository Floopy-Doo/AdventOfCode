using Shouldly;
using static AcC._2025.Day5;

namespace AoC._2025.Facts;

public class Day5Test
{
    public static TheoryData<string, int> SampleDataPart1 = new()
    {
        { "3-5\n\n1", 0 },
        { "3-5\n\n2", 0 },
        { "3-5\n\n3", 1 },
        { "3-5\n\n4", 1 },
        { "3-5\n\n5", 1 },
        { "3-5\n\n6", 0 },
        { "16-20\n\n16\n17\n18\n19\n20", 5 },
        { "16-20\n\n16\n17\n19\n20", 4 },
    };

    [Theory]
    [MemberData(nameof(SampleDataPart1))]
    public void ShouldSolvePart1(string input, int expected)
    {
        var result = SolvePart1(input);
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldSolvePart1FullSample()
    {
        var input = "3-5\n10-14\n16-20\n12-18\n\n1\n5\n8\n11\n17\n32";
        var result = SolvePart1(input);
        result.ShouldBe(3);
    }

    [Fact]
    public void ShouldFindPart1Solution()
    {
        var result = SolvePart1(AocInput.Day5Input);
        result.ShouldBe(525);
    }
}