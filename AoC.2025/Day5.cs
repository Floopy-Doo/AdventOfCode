using System.Text.RegularExpressions;

namespace AcC._2025;

public partial class Day5
{
    public static int SolvePart1(string input)
    {
        var data = input.Split("\n\n");
        var databaseRaw = data[0].Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var productIdsRaw = data[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);

        var database = databaseRaw.Select(ParseDatabaseEntry);
        var products = productIdsRaw.Select(ParseProductIds);

        return products.Count(x => database.Any(r => r.Contains(x)));
    }

    private static ProductRange ParseDatabaseEntry(string raw)
    {
        var inputData = MyRegex().Match(raw);
        var lowerBound = decimal.Parse(inputData.Groups[1].Value);
        var upperBound = decimal.Parse(inputData.Groups[2].Value);

        return new ProductRange(new ProductId(lowerBound), new ProductId(upperBound));
    }

    private static ProductId ParseProductIds(string raw)
    {
        return new ProductId(decimal.Parse(raw));
    }

    private sealed record ProductId(decimal Value);

    private sealed record ProductRange(ProductId Lower, ProductId Upper)
    {
        public bool Contains(ProductId id) =>
            this.Lower.Value <= id.Value
            && id.Value <= this.Upper.Value;
    }

    [GeneratedRegex(@"(\d+)-(\d+)")]
    private static partial Regex MyRegex();
}