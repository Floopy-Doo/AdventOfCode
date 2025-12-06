using Shouldly;
using static AcC._2025.Advent_1st.Day2;

namespace AoC._2025.Facts.Advent_1st;

public class Day2Test
{
    public static TheoryData<string, decimal[]> LineDataPart1 = new()
    {
        { "11-22", [11m,22m] },
        { "95-115", [99m] },
        { "998-1012", [1010m] },
        { "1188511880-1188511890", [1188511885m] },
        { "222220-222224", [222222m] },
        { "1698522-1698528", [] },
        { "446443-446449", [446446] },
        { "38593856-38593862", [38593859] },
        { "565653-565659", [] },
        { "824824821-824824827", [] },
        { "2121212118-2121212124", [] },
    };

    public static TheoryData<string, decimal[]> LineDataPart2 = new()
    {
        { "11-22", [11m,22m] },
        { "95-115", [99m, 111m] },
        { "998-1012", [999m, 1010m] },
        { "1188511880-1188511890", [1188511885m] },
        { "222220-222224", [222222m] },
        { "1698522-1698528", [] },
        { "446443-446449", [446446] },
        { "38593856-38593862", [38593859] },
        { "565653-565659", [565656m] },
        { "824824821-824824827", [824824824m] },
        { "2121212118-2121212124", [2121212121m] },
    };

    public static TheoryData<decimal, decimal> RepetitionDataPart2 = new()
    {
        { 12341234, 1234 },
        { 11, 1 },
        { 22, 2 },
        { 123123123, 123 },
        { 1212121212, 12 },
        { 1111111, 1 },
    };

    [Theory]
    [MemberData(nameof(LineDataPart1))]
    public void ShouldSolvePart1(string input, decimal[] expected)
    {
        var result = SearchForDuplicatedNumberPart1(input);
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldSolvePart1FullSample()
    {
        var input = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
        var result = SolvePart1(input);
        result.ShouldBe(1227775554m);
    }

    [Fact]
    public void ShouldFIndPart1Solution()
    {
        var result = SolvePart1(AocInput.Day2Input);
        result.ShouldBe(18952700150m);
    }

    [Theory]
    [MemberData(nameof(RepetitionDataPart2))]
    public void ShouldContainRepetitionsPart2(decimal input, decimal expected)
    {
        var result = RepeatingNumberPart2(input);
        result.ShouldBeEquivalentTo(expected);
    }


    [Theory]
    [MemberData(nameof(LineDataPart2))]
    public void ShouldSolvePart2(string input, decimal[] expected)
    {
        var result = SearchForDuplicatedNumberPart2(input);
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldSolvePart2FullSample()
    {
        var input = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
        var result = SolvePart2(input);
        result.ShouldBe(4174379265m);
    }

    [Fact]
    public void ShouldFIndPart2Solution()
    {
        var result = SolvePart2(AocInput.Day2Input);
        result.ShouldBe(28858486244m);
    }
}