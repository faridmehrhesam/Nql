using System.Linq;
using FluentAssertions;
using Nql.Abstractions;
using Nql.Abstractions.Nodes;
using Xunit;

namespace Nql.UnitTests.NqlExpressionParsers
{
    public partial class NqlExpressionParserUnitTest
    {
        [Fact]
        public void TestVisitTake_WhenEverythingIsOk_TakeNodeMustBeReturned()
        {
            var nqlExpressionParser = new NqlExpressionParser();
            var diagnostics = new Diagnostics();
            var nqlMainNode = (NqlMainNode) nqlExpressionParser.Parse("| take 10", diagnostics);
            var nqlTakeNode = (NqlTakeNode) nqlMainNode.ChildNodes.First();
            var nqlConstantNode = (NqlConstantNode) nqlTakeNode.Count;

            diagnostics.Should().HaveCount(0);
            nqlMainNode.ChildNodes.Should().HaveCount(1);
            nqlConstantNode.Value.Should().Be(10);
            nqlConstantNode.Type.Should().Be(typeof(int));
        }

        [Fact]
        public void TestVisitTake_WhenInvalidCount_ErrorMustBeAddedToDiagnostics()
        {
            var nqlExpressionParser = new NqlExpressionParser();
            var diagnostics = new Diagnostics();
            var nqlMainNode = (NqlMainNode) nqlExpressionParser.Parse("| take A", diagnostics);
            var nqlTakeNode = (NqlTakeNode) nqlMainNode.ChildNodes.First();

            diagnostics.Should().HaveCount(2);
            nqlMainNode.ChildNodes.Should().HaveCount(1);
            nqlTakeNode.Count.Should().BeNull();
        }
    }
}