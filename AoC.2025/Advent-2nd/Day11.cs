namespace AoC._2025.Advent_2nd;

public static class Day11
{
    private static readonly Id Start = new("you");
    private static readonly Id End = new("out");

    public static decimal SolvePart1(string input)
    {
        var connections = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(Parse).ToArray();
        var you = connections.Single(x => x.Id == Start);

        var paths = Recursion(you, [], connections.Except([you]).ToDictionary(x => x.Id, x => x));
        return paths.Length;

        static Node[][] Recursion(Node current, Id[] visited, IReadOnlyDictionary<Id, Node> allNodes)
        {
            if (current.Connections.Contains(End)) return [[current]];

            Id[] extendedVisited = [.. visited, current.Id];
            Node[][] allPaths = current
                .Connections
                .Except(visited)
                .Select(x => allNodes[x])
                .SelectMany(x => Recursion(x, extendedVisited, allNodes))
                .Where(x => x.Length != 0)
                .Select(Node[] (x) => [current, .. x])
                .ToArray();
            return allPaths;
        }
    }

    public static decimal SolvePart2(string input)
    {
        return 0;
    }

    private static Node Parse(string line)
    {
        var data = line.Split(":", StringSplitOptions.TrimEntries);
        var nodeId = new Id(data[0]);
        var connections = data[1].Split(" ").Select(x => new Id(x)).ToArray();
        return new Node(nodeId, connections);
    }

    private sealed record Node(Id Id, Id[] Connections);

    private sealed record Id(string Value);
}