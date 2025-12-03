using AcC._2025;
using Shouldly;

namespace AoC._2025.Facts;

public class InputParserTest
{
    public static TheoryData<string, Operation> OperationParseData = new()
    {
        { "L68", new Operation.Left(68) },
        { "L30", new Operation.Left(30) },
        { "R60", new Operation.Right(60) },
    };

    [Theory]
    [MemberData(nameof(OperationParseData))]
    public void ShouldExtractCorrectOperation(string input, Operation expected)
    {
        var result = InputParser.Parse(input);
        result.ShouldBe(expected);
    }
}