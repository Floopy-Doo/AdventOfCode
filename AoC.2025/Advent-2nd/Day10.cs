namespace AoC._2025.Advent_2nd;

public static class Day10
{
    public static decimal SolvePart1(string input)
    {
        var allProblems = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var problems = allProblems.Select(ParseProblem);

        var smallestSolveSequencePerProblem = problems.Select(x => BFS(x.IndicatorToggles, x.RequiredIndicators));

        return smallestSolveSequencePerProblem.Sum(x => x.Length);
    }

    public static Problem ParseProblem(string line)
    {
        var inputs = line.Split(" ")
            .Select(Input (x) => x[0] switch
            {
                '[' => ParseIndicator(x),
                '(' => ParseToggles(x),
                '{' => ParseJoltage(x),
                _ => throw new InvalidDataException($"Unknown Data {x}"),
            })
            .ToList();

        var indicators = inputs.OfType<Input.Indicators>().Single().States;
        var toggles = inputs
            .OfType<Input.Toggles>()
            .Select(x => Enumerable
                .Range(0, indicators.Length)
                .Select(i => x.TogglePositions.Contains(i))
                .ToArray())
            .ToArray();

        return new Problem(indicators, toggles);

        static Input.Indicators ParseIndicator(string indicatorRaw) =>
            new(indicatorRaw.Trim('[', ']').Select(x => x is '#').ToArray());

        static Input.Toggles ParseToggles(string togglesRaw) =>
            new(togglesRaw.Trim('(', ')').Split(',').Select(int.Parse).ToArray());

        static Input.JoltageRequirements ParseJoltage(string joltageRaw) =>
            new(joltageRaw.Trim('{', '}').Split(',').Select(int.Parse).ToArray());
    }


    public static decimal SolvePart2(string input)
    {
        return 0;
    }

    public static Node[] BFS(
        bool[][] possibleToggles,
        bool[] currentIndicators)
    {
        Queue<(Node CurrentNode, Node[] Branch)> nextSearch = [];
        nextSearch.Enqueue((new Node([], currentIndicators), []));

        while (nextSearch.Count != 0)
        {
            var (currentNode, branch) = nextSearch.Dequeue();
            if (currentNode.Indicators.All(x => !x)) return branch;

            var newStates = possibleToggles
                .Select(toggles => new Node(toggles, ApplyToggle(currentNode.Indicators, toggles)));
            var childSearches = newStates
                .Where(nextNode => branch.All(n => !AreSameIndicators(n.Indicators,nextNode.Indicators)))
                .OrderBy(x => x.Indicators.Count(x => x))
                .Select(nextNode => (nextNode, branch.Append(nextNode).ToArray()));

            foreach (var childSearch in childSearches) nextSearch.Enqueue(childSearch);
        }

        return [];

        static bool[] ApplyToggle(bool[] indicators, bool[] toggles) => indicators.Zip(toggles).Select(x => x.Second ? !x.First : x.First).ToArray();
        static bool AreSameIndicators(bool[] indicatorA, bool[] indicatorB) => indicatorA.Zip(indicatorB).Aggregate(true, (b, tuple) => b && tuple.First == tuple.Second);
    }

    public sealed record Problem(bool[] RequiredIndicators, bool[][] IndicatorToggles);

    public sealed record Node(bool[] UsedToggles, bool[] Indicators)
    {
        public override string ToString() => $"I:{Indicators.Aggregate("", (s, b) => $"{s}{(b ? "#" : ".")}")} | T:{UsedToggles.Aggregate("", (s, b) => $"{s}{(b ? 1 : 0)}")}";
    }

    private abstract record Input
    {
        public sealed record Indicators(bool[] States) : Input;

        public sealed record Toggles(int[] TogglePositions) : Input;

        public sealed record JoltageRequirements(int[] Requirements) : Input;
    }
}