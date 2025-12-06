using Shouldly;
using static AcC._2025.Advent_1st.Day6;

namespace AoC._2025.Facts.Advent_1st;

public class Day6Test
{
    public static TheoryData<string, decimal> SampleDataPart1 = new()
    {
        { "123 \n 45 \n  6 \n*   \n", 33210 },
        { "328 \n64  \n98  \n+   \n", 490 },
        { " 51 \n387 \n215 \n*   \n", 4243455 },
        { "64  \n23  \n314 \n+   \n", 401 },
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
        var input = "123 328  51 64 \n 45 64  387 23 \n  6 98  215 314\n*   +   *   +  \n";
        var result = SolvePart1(input);
        result.ShouldBe(4277556);
    }

    [Fact]
    public void ShouldFindPart1Solution()
    {
        var result = SolvePart1(AocInput.Day6Input);
        result.ShouldBe(59562874502m);
    }
}