namespace AcC._2025.Advent_1st;

public static class Day3Part1
{
    public static int Solve(string input) =>
        input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(Parse)
            .Select(SolveLine)
            .Sum();

    private static int SolveLine(int[] line)
    {
        return AllCombinations(line).Max();
    }

    private static IEnumerable<int> AllCombinations(int[] data)
    {
        for (int firstIndex = 0; firstIndex < data.Length; firstIndex++)
        {
            for (int secondIndex = firstIndex + 1; secondIndex < data.Length; secondIndex++)
            {
                yield return data[firstIndex] * 10 + data[secondIndex];
            }
        }
    }

    private static int[] Parse(string line) =>
        line.Select(z => int.Parse($"{z}")).ToArray();
}