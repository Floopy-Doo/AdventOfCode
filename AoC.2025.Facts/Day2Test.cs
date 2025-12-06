using Shouldly;
using static AcC._2025.Day2;

namespace AoC._2025.Facts;

public class Day2Test
{
    public static TheoryData<string, decimal[]> LineData = new()
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

    [Theory]
    [MemberData(nameof(LineData))]
    public void ShouldSolvePart1(string input, decimal[] expected)
    {
        var result = SearchForDuplicatedNumber(input);
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
}