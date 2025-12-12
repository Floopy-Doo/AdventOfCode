using System.Text.RegularExpressions;

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

        // List<NodeJoltage[]> results = [];
        // Parallel.ForEach(problems, problem => results.Add(DFSJoltages2(problem.Toggles, problem.RequiredJoltages)));

        var results = problems.Select(problem => DFSJoltages2(problem.Toggles, problem.RequiredJoltages));


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

        if (joltages.Length != indicators.Length || toggles.Any(x => x.Length != joltages.Length)) throw new InvalidDataException($"{line}");

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
            if (IsResult(currentNode)) return branch;

            var newStates = possibleToggles
                .Select(toggles => new Node(toggles, ApplyToggle(currentNode.Indicators, toggles)));
            var childSearches = newStates
                .Where(nextNode => branch.All(n => !AreSameIndicators(n.Indicators,nextNode.Indicators)))
                .OrderBy(x => x.Indicators.Count(z => z))
                .Select(nextNode => (nextNode, branch.Append(nextNode).ToArray()));

            foreach (var childSearch in childSearches)
            {
                if (IsResult(childSearch.nextNode)) return childSearch.Item2;
                
                nextSearch.Enqueue(childSearch);
            }
        }

        return [];

        static bool[] ApplyToggle(bool[] indicators, bool[] toggles) => indicators.Zip(toggles).Select(x => x.Second ? !x.First : x.First).ToArray();
        static bool AreSameIndicators(bool[] indicatorA, bool[] indicatorB) => indicatorA.Zip(indicatorB).Aggregate(true, (b, tuple) => b && tuple.First == tuple.Second);

        static bool IsResult(Node node) => node.Indicators.All(x => !x);
    }

    public static NodeJoltage[] DFSJoltages(bool[][] possibleToggles, int[] requiredJoltages)
    {
        var elementPriority = Enumerable
            .Range(0, requiredJoltages.Length)
            .Select(i => (i, NoOfTogglesApplicable: possibleToggles.Select(t => t[i] ? 1 : 0).Sum()))
            .OrderBy(x => x.NoOfTogglesApplicable)
            .Select(x => x.i)
            .ToList();

        List<NodeJoltage[]> fullSolutions = [];

        Queue<NodeJoltage[]> remainingPartialSolutions = [];
        remainingPartialSolutions.Enqueue([new NodeJoltage([], requiredJoltages)]);

        foreach (var elementIndex in elementPriority)
        {
            var applicableToggles = possibleToggles
                .Where(x => x[elementIndex])
                .OrderBy(x => x.Count(z => z))
                .ToList();

            List<NodeJoltage[]> partialSolutionsForNextIteration = [];
            do
            {
                var partialSolution = remainingPartialSolutions.Dequeue();
                foreach (var toggle in applicableToggles)
                {
                    var solution = RecursiveApplyToggle(toggle, partialSolution[^1].Joltages, partialSolution);
                    if (solution.Any(IsResult)) fullSolutions.Add(solution);
                    else if (IsDeadEnd(solution[^1])) continue;
                    else partialSolutionsForNextIteration.Add(solution);
                }
            } while (remainingPartialSolutions.Count != 0);

            remainingPartialSolutions = new Queue<NodeJoltage[]>(partialSolutionsForNextIteration);
        }

        return fullSolutions.OrderBy(x => x.Length).First();

        static NodeJoltage[] RecursiveApplyToggle(bool[] toggles, int[] initialJoltages, NodeJoltage[] branch)
        {
            var result = new NodeJoltage(toggles,ApplyToggle(initialJoltages, toggles));
            NodeJoltage[] newBranch = [.. branch, result];
            if (IsResult(result) || IsDeadEnd(result)) return newBranch;
            return RecursiveApplyToggle(result.UsedToggles, result.Joltages, newBranch);
        }

        static int[] ApplyToggle(int[] joltages, bool[] toggles) => joltages.Zip(toggles).Select(x => x.Second ? (x.First - 1) : x.First).ToArray();
        static bool IsResult(NodeJoltage node) => node.Joltages.All(x => x == 0);
        static bool IsDeadEnd(NodeJoltage node) => node.Joltages.Any(x => x < 0);
    }


    public static NodeJoltage[] DFSJoltages2(bool[][] possibleToggles, int[] requiredJoltages)
    {
        // Calculate Index Priority

        // Recursive
        // - Give Index
        // - Give Remaining Buttons
        // - Give CurrentBranch
        // Filter Buttons by Priority
        // For each Button
        //      Apply the max amount of presses
        //      Abort if solution is found
        //      Recurse (Index++, Remaing Buttons without Button)
        //
        var orderedEnumerable = Enumerable
            .Range(0, requiredJoltages.Length)
            .Select(i => (i, NoOfTogglesApplicable: possibleToggles.Count(t => t[i])))
            .OrderBy(x => x.NoOfTogglesApplicable);
        var indexPriority = orderedEnumerable
            .Select(x => x.i)
            .ToArray();

        var branches = ButtonRecursion(indexPriority, possibleToggles, [new NodeJoltage([], requiredJoltages)]);
        return branches.OrderBy(x => x.Length).First()[1..];


        static NodeJoltage[][] ButtonRecursion(int[] indexPriorities, bool[][] unusedToggles, NodeJoltage[] branch)
        {
            var applicableToggles = unusedToggles
                .Where(x => x[indexPriorities[0]])
                .OrderBy(x => x.Count(z => z))
                .ToList();

            List<NodeJoltage[]> solutions = [];
            foreach (var toggle in applicableToggles)
            {
                var remainingToggles = unusedToggles.Except([toggle]).ToArray();
                var toggleBranch = remainingToggles.Length > 1
                    ? RecursiveApplyToggle(toggle, branch[^1].Joltages, branch, remainingToggles[0])
                    : RecursiveApplyToggle(toggle, branch[^1].Joltages, branch, null);
                if (IsResult(toggleBranch[^1]))
                {
                    solutions.Add(toggleBranch);
                    continue;
                }

                if (indexPriorities.Length == 1)
                {
                    continue;
                }

                solutions.AddRange(
                    ButtonRecursion(
                        indexPriorities[1..],
                        remainingToggles,
                        toggleBranch));
            }

            return solutions.ToArray();
        }

        static NodeJoltage[] RecursiveApplyToggle(bool[] toggles, int[] initialJoltages, NodeJoltage[] branch, bool[]? nextRemainingToggle)
        {
            var result = new NodeJoltage(toggles, ApplyToggle(initialJoltages, toggles));
            NodeJoltage[] newBranch = [.. branch, result];
            if (IsResult(result)) return newBranch;
            if (IsDeadEnd(result)) return branch;
            if (HaveAllPositionOfNextButtonEqualized(result, nextRemainingToggle)) return newBranch;
            return RecursiveApplyToggle(result.UsedToggles, result.Joltages, newBranch, nextRemainingToggle);
        }

        static int[] ApplyToggle(int[] joltages, bool[] toggles) =>
            joltages.Zip(toggles).Select(x => x.Second ? x.First - 1 : x.First).ToArray();

        static bool IsResult(NodeJoltage node) =>
            node.Joltages.All(x => x == 0);

        static bool IsDeadEnd(NodeJoltage node) =>
            node.Joltages.Any(x => x < 0);

        static bool HaveAllPositionOfNextButtonEqualized(NodeJoltage node, bool[]? nextRemainingToggle) =>
            nextRemainingToggle is not null && node.Joltages.Zip(nextRemainingToggle).Where(x => x.Second).GroupBy(x => x.First).Count() == 1;
    }

    public static NodeJoltage[] DFSJoltages3(bool[][] possibleToggles, int[] requiredJoltages)
    {
        return [];



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