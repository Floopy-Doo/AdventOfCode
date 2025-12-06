using System.Text.RegularExpressions;

namespace AcC._2025;

public static class Day2
{
    public static decimal SolvePart1(string input)
    {
        return input.Split(",")
            .SelectMany(SearchForDuplicatedNumber)
            .Sum();
    }

    public static decimal[] SearchForDuplicatedNumber(string input)
    {
        var inputData = Regex.Match(input, @"(\d+)-(\d+)");
        var lowerBound = decimal.Parse(inputData.Groups[1].Value);
        var upperBound = decimal.Parse(inputData.Groups[2].Value);

        if (lowerBound > upperBound) throw new InvalidDataException($"Strange Data in sample {input}");

        var duplicatedNumbers = Enumerable
            .Range(0, int.MaxValue)
            .TakeWhile(x => x < upperBound - lowerBound + 1)
            .Select(x => $"{lowerBound + x}")
            .Where(IsRepeatingString)
            .Select(decimal.Parse)
            .ToArray();

        return duplicatedNumbers;
    }

    private static bool IsRepeatingString(string value)
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