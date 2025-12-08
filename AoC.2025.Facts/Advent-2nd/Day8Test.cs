using System.Numerics;
using Shouldly;
using static AoC._2025.Facts.Advent_2nd.AocInput;
using static AoC._2025.Advent_2nd.Day8;

namespace AoC._2025.Facts.Advent_2nd;

public class Day8Test
{
    public static TheoryData<Vector3[][], Vector3, Vector3, Vector3[][]> JoinCircuitData = new()
    {
        {
            [],
            new Vector3(1, 1, 1),
            new Vector3(2, 2, 2),
            [[new Vector3(1, 1, 1), new Vector3(2, 2, 2)]]
        },
        {
            [[new Vector3(1, 1, 1), new Vector3(3, 3, 3)]],
            new Vector3(1, 1, 1),
            new Vector3(2, 2, 2),
            [[new Vector3(1, 1, 1), new Vector3(3, 3, 3), new Vector3(2, 2, 2)]]
        },
        {
            [[new Vector3(2, 2, 2), new Vector3(3, 3, 3)]],
            new Vector3(1, 1, 1),
            new Vector3(2, 2, 2),
            [[new Vector3(1, 1, 1), new Vector3(2, 2, 2), new Vector3(3, 3, 3)]]
        },
        {
            [[new Vector3(2, 2, 2), new Vector3(3, 3, 3)], [new Vector3(0, 0, 0), new Vector3(1, 1, 1)]],
            new Vector3(1, 1, 1),
            new Vector3(2, 2, 2),
            [[new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(2, 2, 2), new Vector3(3, 3, 3)]]
        },
        {
            [[new Vector3(2, 2, 2), new Vector3(3, 3, 3), new Vector3(1, 1, 1)]],
            new Vector3(1, 1, 1),
            new Vector3(2, 2, 2),
            [[new Vector3(2, 2, 2), new Vector3(3, 3, 3), new Vector3(1, 1, 1)]]
        },
    };

    [Theory]
    [MemberData(nameof(JoinCircuitData))]
    public void ShouldCorrectlyJoinCircuits(Vector3[][] existingCircuits, Vector3 a, Vector3 b, Vector3[][] expected)
    {
        var result = JoinCircuit(existingCircuits, a, b);
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldSolvePart1FullSample()
    {
        var input = "162,817,812\n57,618,57\n906,360,560\n592,479,940\n352,342,300\n466,668,158\n542,29,236\n431,825,988\n739,650,466\n52,470,668\n216,146,977\n819,987,18\n117,168,530\n805,96,715\n346,949,466\n970,615,88\n941,993,340\n862,61,35\n984,92,344\n425,690,689\n";
        var result = SolvePart1(10, input);
        result.ShouldBe(40m);
    }

    [Fact]
    public void ShouldFindPart1Solution()
    {
        var result = SolvePart1(1000, Day8Input);
        result.ShouldBe(29406m);
    }
}