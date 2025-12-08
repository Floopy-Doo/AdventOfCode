using System.Text.RegularExpressions;

namespace AoC._2025.Advent_1st;

public static partial class Day6
{
    public static decimal SolvePart1(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        var operatorLine = lines[^1];
        var operations = OperatorRegex()
            .Matches(operatorLine)
            .Select(Operation (x, i) =>
                x.Value.StartsWith('+')
                    ? new Operation.Add(i, x.Length)
                    : new Operation.Multiply(i, x.Length))
            .ToArray();

        var operandLines = lines[..^1];

        var inputOne = operandLines
            .SelectMany(line => ParseOperationsRecursive(operations, line))
            .Concat(operations);

        var valueTuples = inputOne
            .GroupBy(x => x.Index, (i, o) => o.ToArray())
            .Select((operations1, i) => (Index: i, Value: Calc(operations1, operandLines.Length)));

        return valueTuples
            .Sum(x => x.Value);
    }

    public static decimal SolvePart2(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Reverse().ToArray())
            .ToArray();
        var transposedArray = lines[1..]
            .Select(x => x.ToArray())
            .Aggregate(
                lines[0].Select(c => new[] { c }),
                (acc,  line) => acc.Zip(line).Select(x => (char[])[.. x.First, x.Second]))
            .ToArray();

        var problems = SplitByProblem([], transposedArray)
            .Where(x => x.SelectMany(z => z).Any(z => z != ' ')).ToArray();


        var results = problems.Select(problem =>
                problem
                    .Select(chars => decimal.Parse(new string(chars[..^1].Where(x => x != ' ').ToArray())))
                    .Aggregate((acc, value) => problem[^1][^1] == '+' ? acc + value : acc * value))
            .ToArray();


        return results.Sum();

        static IEnumerable<char[][]> SplitByProblem(char[][] current, char[][] lines)
        {
            if (lines.Length == 0) return [current];
            if (lines[0].All(x => x == ' ')) return [current, .. SplitByProblem([], lines[1..])];
            return SplitByProblem([.. current, lines[0]], lines[1..]);
        }
    }

    public static decimal SolvePart1Approach2(string input)
    {
        var stringsEnumerable = input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .ToArray();

        var transposedArray = stringsEnumerable
            .Skip(1)
            .Aggregate(
                stringsEnumerable
                    .First()
                    .Select(x => new[]{x}),
            (acc, line) => acc
                .Zip(line)
                .Select(x => (string[])[x.Second, .. x.First]))
            .ToArray();

        var result = transposedArray
            .Select(x => x[1..].Select(decimal.Parse).Aggregate((acc, z) => x[0]=="+" ? (acc + z) : (acc * z)));
        return result.Sum();
    }

    private static IEnumerable<Operation.Operator> ParseOperationsRecursive(Operation[] remainderOperations, string remainderOperatorLine)
    {
        if (remainderOperations.Length == 0 && remainderOperatorLine.Length == 0)
            return [];
        if (remainderOperations.Length == 0)
            throw new InvalidOperationException($"something did not get parsed, leftover '{remainderOperatorLine}'");
        if (remainderOperatorLine.Length == 0)
            throw new InvalidOperationException($"some operation do not have a value, leftover '{remainderOperations.Length}'");

        var operation = remainderOperations[0];
        var operandRaw = remainderOperatorLine.Substring(0, operation.Length);

        if (operandRaw.Trim().Contains(' '))
            throw new InvalidOperationException($"invalid operand '{operandRaw}'");

        return ParseOperationsRecursive(remainderOperations[1..], remainderOperatorLine[operation.Length..])
            .Append(new Operation.Operator(operation.Index, operandRaw.Length, decimal.Parse(operandRaw)));
    }

    private static decimal Calc(Operation[] operations, int operandLinesLength)
    {
        var operands = operations.OfType<Operation.Operator>();
        var operation = operations.Single(x => x is not Operation.Operator);
        return operation switch
        {
            Operation.Add => operands.Select(x => x.Value).Aggregate((acc, op) => acc + op),
            Operation.Multiply => operands.Select(x => x.Value).Aggregate((acc, op) => acc * op),
            _ => throw new InvalidOperationException($"unexpected operation {operation}"),
        };
    }

    private abstract record Operation(int Index, int Length)
    {
        public sealed record Add(int Index, int Length) : Operation(Index, Length);

        public sealed record Multiply(int Index, int Length) : Operation(Index, Length);

        public sealed record Operator(int Index, int Length, decimal Value) : Operation(Index, Length);
    }

    private abstract record Ops
    {
        public sealed record Add : Ops;

        public sealed record Multiply : Ops;

        public sealed record Operand(decimal Value) : Ops;
    }


    [GeneratedRegex(@"[+*]\s*")]
    private static partial Regex OperatorRegex();
    [GeneratedRegex(@"\s+")]
    private static partial Regex SplitBySpacesRegex();
}