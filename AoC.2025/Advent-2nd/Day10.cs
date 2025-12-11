namespace AoC._2025.Advent_2nd;

public static class Day10
{
    public static decimal SolvePart1(string input)
    {
        var allProblems = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var problems = allProblems.Select(ParseProblem);

        List<Node[]> results = [];
        Parallel.ForEach(problems, problem => results.Add(BFSIndicators(problem.Toggles, problem.RequiredIndicators)));

        return results.Sum(x => x.Length);
    }

    public static decimal SolvePart2(string input)
    {
        var allProblems = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var problems = allProblems.Select(ParseProblem);

        List<NodeJoltage[]> results = [];
        Parallel.ForEach(problems, problem => results.Add(BFSJoltages(problem.Toggles, problem.RequiredJoltages)));

        return results.Sum(x => x.Length);
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

        var joltages = inputs.OfType<Input.JoltageRequirements>().Single().Requirements;

        return new Problem(indicators, joltages, toggles);

        static Input.Indicators ParseIndicator(string indicatorRaw) =>
            new(indicatorRaw.Trim('[', ']').Select(x => x is '#').ToArray());

        static Input.Toggles ParseToggles(string togglesRaw) =>
            new(togglesRaw.Trim('(', ')').Split(',').Select(int.Parse).ToArray());

        static Input.JoltageRequirements ParseJoltage(string joltageRaw) =>
            new(joltageRaw.Trim('{', '}').Split(',').Select(int.Parse).ToArray());
    }

    public static Node[] BFSIndicators(
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

    public static NodeJoltage[] BFSJoltages(bool[][] possibleToggles, int[] requiredJoltages)
    {
        Queue<(NodeJoltage CurrentNode, NodeJoltage[] Branch)> nextSearch = [];
        nextSearch.Enqueue((new NodeJoltage([], requiredJoltages), []));

        while (nextSearch.Count != 0)
        {
            var (currentNode, branch) = nextSearch.Dequeue();
            if (currentNode.Joltages.All(x => x == 0)) return branch;

            var newStates = possibleToggles
                .Select(toggles => new NodeJoltage(toggles, ApplyToggle(currentNode.Joltages, toggles)));
            var childSearches = newStates
                .Where(nextNode => nextNode.Joltages.All(x => x >= 0))
                .OrderBy(x => x.Joltages.Sum())
                .Select(nextNode => (nextNode, branch.Append(nextNode).ToArray()));

            foreach (var childSearch in childSearches) nextSearch.Enqueue(childSearch);
        }

        return [];

        static int[] ApplyToggle(int[] joltages, bool[] toggles) => joltages.Zip(toggles).Select(x => x.Second ? (x.First - 1) : x.First).ToArray();
    }

    public sealed record Problem(bool[] RequiredIndicators, int[] RequiredJoltages, bool[][] Toggles);

    public sealed record Node(bool[] UsedToggles, bool[] Indicators);

    public sealed record NodeJoltage(bool[] UsedToggles, int[] Joltages);

    private abstract record Input
    {
        public sealed record Indicators(bool[] States) : Input;

        public sealed record Toggles(int[] TogglePositions) : Input;

        public sealed record JoltageRequirements(int[] Requirements) : Input;
    }
}