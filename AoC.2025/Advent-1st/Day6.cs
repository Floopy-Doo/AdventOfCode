using System.Text.RegularExpressions;

namespace AcC._2025.Advent_1st;

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

        return operandLines
            .SelectMany(line => ParseOperationsRecursive(operations, line))
            .Concat(operations)
            .GroupBy(x => x.Index, (i, o) => o.ToArray())
            .Select(Calc)
            .Sum();
    }

    private static IEnumerable<Operation.Operator> ParseOperationsRecursive(Operation[] remainderOperations, string remainderOperatorLine)
    {
        if (remainderOperations.Length == 0 && remainderOperatorLine.Length == 0)
            return [];
        if (remainderOperations.Length == 0)
            throw new InvalidOperationException($"something did not get parsed, leftover {remainderOperatorLine}");
        if (remainderOperatorLine.Length == 0)
            throw new InvalidOperationException($"some operation do not have a value, leftover {remainderOperations.Length}");

        var op = remainderOperations[0];
        var operand = remainderOperatorLine.Substring(0, op.Length);
        return ParseOperationsRecursive(remainderOperations[1..], remainderOperatorLine[op.Length..])
            .Append(new Operation.Operator(op.Index, operand.Length, int.Parse(operand)));
    }

    private static decimal Calc(Operation[] operations)
    {
        var operands = operations.OfType<Operation.Operator>();
        var operation = operations.Single(x => x is not Operation.Operator);
        return operation switch
        {
            Operation.Add => operands.Aggregate(0, (acc, op) => acc + op.Value),
            Operation.Multiply => operands.Aggregate(1, (acc, op) => acc * op.Value),
            _ => throw new InvalidOperationException($"unexpected operation {operation}"),
        };
    }

    private abstract record Operation(int Index, int Length)
    {
        public sealed record Add(int Index, int Length) : Operation(Index, Length);

        public sealed record Multiply(int Index, int Length) : Operation(Index, Length);

        public sealed record Operator(int Index, int Length, int Value) : Operation(Index, Length);
    }


    [GeneratedRegex(@"[+*]\s+")]
    private static partial Regex OperatorRegex();
}