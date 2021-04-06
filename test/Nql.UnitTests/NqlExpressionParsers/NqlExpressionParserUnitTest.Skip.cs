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
        public void TestVisitSkip_WhenEverythingIsOk_SkipNodeMustBeReturned()
        {
            var nqlExpressionParser = new NqlExpressionParser();
            var diagnostics = new Diagnostics();
            var nqlMainNode = (NqlMainNode) nqlExpressionParser.Parse("| skip 10", diagnostics);
            var nqlSkipNode = (NqlSkipNode) nqlMainNode.ChildNodes.First();
            var nqlConstantNode = (NqlConstantNode) nqlSkipNode.Count;

            diagnostics.Should().HaveCount(0);
            nqlMainNode.ChildNodes.Should().HaveCount(1);
            nqlConstantNode.Value.Should().Be(10);
            nqlConstantNode.Type.Should().Be(typeof(int));
        }

        [Fact]
        public void TestVisitSkip_WhenInvalidCount_ErrorMustBeAddedToDiagnostics()
        {
            var nqlExpressionParser = new NqlExpressionParser();
            var diagnostics = new Diagnostics();
            var nqlMainNode = (NqlMainNode) nqlExpressionParser.Parse("| skip A", diagnostics);
            var nqlSkipNode = (NqlSkipNode) nqlMainNode.ChildNodes.First();

            diagnostics.Should().HaveCount(1);
            nqlMainNode.ChildNodes.Should().HaveCount(1);
            nqlSkipNode.Count.Should().BeNull();
        }
    }
}