using System.Text.RegularExpressions;

namespace AcC._2025.Advent_1st;

public static class Day2
{
    public static decimal SolvePart1(string input)
    {
        return input.Split(",")
            .SelectMany(SearchForDuplicatedNumberPart1)
            .Sum();
    }

    public static decimal[] SearchForDuplicatedNumberPart1(string input)
    {
        var duplicatedNumbers = ExpandNumbers(input)
            .Where(x => IsRepeatingStringPart1($"{x}"))
            .ToArray();

        return duplicatedNumbers;
    }

    public static decimal SolvePart2(string input)
    {
        return input.Split(",")
            .SelectMany(SearchForDuplicatedNumberPart2)
            .Sum();
    }

    public static decimal[] SearchForDuplicatedNumberPart2(string input)
    {
        var duplicatedNumbers = ExpandNumbers(input)
            .Where(x => RepeatingNumberPart2(x) is not null)
            .ToArray();

        return duplicatedNumbers;
    }

    public static decimal? RepeatingNumberPart2(decimal value)
    {
        var valueAsString = $"{value}";
        var viableSplits = Enumerable
            .Range(1, valueAsString.Length / 2)
            .Where(range => valueAsString.Length % range == 0)
            .Select(range => valueAsString[..range])
            .Where(sample => valueAsString.Replace(sample, string.Empty).Length == 0);

        return viableSplits
            .OrderBy(sample => sample.Length)
            .Select(decimal.Parse)
            .OfType<decimal?>()
            .FirstOrDefault();
    }

    private static IEnumerable<decimal> ExpandNumbers(string input)
    {
        var inputData = Regex.Match(input, @"(\d+)-(\d+)");
        var lowerBound = decimal.Parse(inputData.Groups[1].Value);
        var upperBound = decimal.Parse(inputData.Groups[2].Value);

        var expandedNumbers = Enumerable
            .Range(0, int.MaxValue)
            .TakeWhile(x => x < upperBound - lowerBound + 1)
            .Select(x => lowerBound + x);
        return expandedNumbers;
    }


    private static bool IsRepeatingStringPart1(string value)
    {
        return value.Length % 2 == 0
               && value
                   .Select((charValue, i) => (charValue, i))
                   .GroupBy(g => g.i / (value.Length / 2), c => c.charValue)
                   .Select(g => new string(g.ToArray()))
                   .GroupBy(part => part)
                   .Count() == 1;
    }
}