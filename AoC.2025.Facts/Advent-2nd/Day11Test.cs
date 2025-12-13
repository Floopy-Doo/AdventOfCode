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
        { "you: dac fft\nsvr: you\ndac: out\nfft: eee\neee: out\n", 0 },
        { "svr: you dac\nyou: fft\ndac: eee\nfft: dac\neee: out\n", 1 },
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
        var result = SolvePart1(Day11Input);
        result.ShouldBe(523m);
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
        var input = "svr: aaa bbb\naaa: fft\nfft: ccc\nbbb: tty\ntty: ccc\nccc: ddd eee\nddd: hub\nhub: fff\neee: dac\ndac: fff\nfff: ggg hhh\nggg: out\nhhh: out\n";
        var result = SolvePart2(input);
        result.ShouldBe(2m);
    }

    [Fact]
    public void ShouldFindPart2Solution()
    {
        var result = SolvePart2(Day11Input);
        result.ShouldBe(-1m);
    }
}