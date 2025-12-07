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
        { "1241 \n   1 \n314  \n+    \n", 1556 },
        { "97451 \n 12   \n64    \n*     \n", 74842368 },
    };

    public static TheoryData<string, decimal> SampleDataPart2 = new()
    {
        { "123 \n 45 \n  6 \n*   \n", 8544 },
        { "328 \n64  \n98  \n+   \n", 625 },
        { " 51 \n387 \n215 \n*   \n", 3253600 },
        { "64  \n23  \n314 \n+   \n", 1058 },
        { "1241 \n   1 \n314  \n+    \n", 11m+44m+21m+13m },
        { "97451 \n 12   \n64    \n*     \n", 1m*5m*42m*714m*96m },
    };

    [Theory]
    [MemberData(nameof(SampleDataPart1))]
    public void ShouldSolvePart1(string input, decimal expected)
    {
        var result = SolvePart1(input);
        var result2 = SolvePart1Approach2(input);
        result.ShouldSatisfyAllConditions(
            x => x.ShouldBeEquivalentTo(expected),
            x => x.ShouldBeEquivalentTo(result2));
    }

    [Fact]
    public void ShouldSolvePart1FullSample()
    {
        var input = "123 328  51 64 \n 45 64  387 23 \n  6 98  215 314\n*   +   *   +  \n";
        var result = SolvePart1(input);
        var result2 = SolvePart1Approach2(input);
        result.ShouldSatisfyAllConditions(
            x => x.ShouldBe(4277556),
            x => x.ShouldBe(result2));
    }

    [Fact]
    public void ShouldFindPart1Solution()
    {
        var result = SolvePart1(AocInput.Day6Input);
        var result2 = SolvePart1Approach2(AocInput.Day6Input);
        result.ShouldSatisfyAllConditions(
            x => x.ShouldBe(6209956042374m),
            x => x.ShouldBe(result2));
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
        var input = "123 328  51 64 \n 45 64  387 23 \n  6 98  215 314\n*   +   *   +  \n";
        var result = SolvePart2(input);
        result.ShouldBe(3263827m);
    }

    [Fact]
    public void ShouldFindPart2Solution()
    {
        var result = SolvePart2(AocInput.Day6Input);
        result.ShouldBe(12608160008022m);
    }
}