namespace AoC._2025.Advent_2nd;

public static class Day11
{
    private static readonly Id You = new("you");
    private static readonly Id Server = new("svr");
    private static readonly Id Out = new("out");
    private static readonly Id DigitalToAnalogConverter = new("dac");
    private static readonly Id FastFourierTransformation = new("fft");

    public static decimal SolvePart1(string input)
    {
        var connections = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(Parse).ToArray();
        var you = connections.Single(x => x.Id == You);

        var paths = Recursion(you, [], connections.Except([you]).ToDictionary(x => x.Id, x => x));
        return paths.Length;

        static Node[][] Recursion(Node current, Id[] visited, IReadOnlyDictionary<Id, Node> allNodes)
        {
            if (current.Connections.Contains(Out)) return [[current]];

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
        var connections = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(Parse).ToArray();
        var start = connections.Single(x => x.Id == Server);

        var nodeDict = connections
            .Except([start])
            .Append(new Node(Out, []))
            .ToDictionary(x => x.Id, x => x);

        var paths = Recursion(start, [], nodeDict, []);
        return paths.Count;


        static List<List<Id>> Recursion(
            Node current,
            Id[] path,
            IReadOnlyDictionary<Id, Node> nodeDict,
            Dictionary<Id, List<List<Id>>> analyzedNodes)
        {
            if (current.Id == Out && path.Intersect([DigitalToAnalogConverter, FastFourierTransformation]).Count() == 2)
                return [[.. path, current.Id]];

            if (current.Id == Out)
                return [];

            var foundNodes = current
                .Connections
                .Except(analyzedNodes.Keys)
                .Select(nextNodeId => Recursion(nodeDict[nextNodeId], [.. path, current.Id], nodeDict, analyzedNodes))
                .Where(x => x.Count > 0)
                .SelectMany(x => x)
                .ToList();

            return foundNodes;
        }
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