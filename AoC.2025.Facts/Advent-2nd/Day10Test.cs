using Shouldly;
using static AoC._2025.Advent_2nd.Day10;
using static AoC._2025.Facts.Advent_2nd.AocInput;

namespace AoC._2025.Facts.Advent_2nd;

public class Day10Test
{
    public static TheoryData<string, decimal> SampleDataPart1 = new()
    {
        { "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}\n", 2 },
        { "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}\n", 3 },
        { "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}\n", 2 },
    };

    public static TheoryData<bool[], bool[][], Node[]> TreeSearchData = new()
    {
        {
            [false, true, true, false],
            [
                [false, false, true, false],
                [false, true, false, true],
                [false, false, true, false],
                [false, false, true, true],
                [true, false, true, false],
                [true, true, false, false],
            ],
            [
                new Node([false, true, false, true], [false, false, true, true]),
                new Node([false, false, true, true], [false, false, false, false]),
            ]
        },
    };


    public static TheoryData<string, bool[], bool[][]> ParseData = new()
    {
        {
            "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}",
            [false, true, true, false],
            [
                [false, false, false, true],
                [false, true, false, true],
                [false, false, true, false],
                [false, false, true, true],
                [true, false, true, false],
                [true, true, false, false],
            ]
        },        {
            "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}",
            [false, false, false, true, false],
            [
                [true, false, true, true, true],
                [false, false, true, true, false],
                [true, false, false, false, true],
                [true, true, true, false, false],
                [false, true, true, true, true],
            ]
        },
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

    [Theory]
    [MemberData(nameof(TreeSearchData))]
    public void ShouldSolveTree(bool[] input, bool[][] toggles, Node[] expected)
    {
        var result = BFS(toggles, input);
        result.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(ParseData))]
    public void ShouldSolveParse(string input, bool[] expectedIndicators, bool[][] expectedToggles)
    {
        var result = ParseProblem(input);
        result.ShouldSatisfyAllConditions(
            x => x.IndicatorToggles.ShouldBeEquivalentTo(expectedToggles),
            x => x.RequiredIndicators.ShouldBeEquivalentTo(expectedIndicators));
    }


    [Fact]
    public void ShouldSolvePart1FullSample()
    {
        var input = "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}\n[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}\n[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}\n";
        var result = SolvePart1(input);
        result.ShouldBe(7m);
    }

    [Fact]
    public void ShouldFindPart1Solution()
    {
        var result = SolvePart1(Day10Input);
        result.ShouldBe(542m);
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
        var input = "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}\n[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}\n[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}\n";
        var result = SolvePart2(input);
        result.ShouldBe(7m);
    }

    [Fact]
    public void ShouldFindPart2Solution()
    {
        var result = SolvePart2(Day9Input);
        result.ShouldBe(1525241870m);
    }
}