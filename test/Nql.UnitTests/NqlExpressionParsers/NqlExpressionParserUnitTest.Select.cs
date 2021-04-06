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
        public void TestVisitSelect_WhenEverythingIsOk_SelectNodeMustBeReturned()
        {
            var nqlExpressionParser = new NqlExpressionParser();
            var diagnostics = new Diagnostics();
            var nqlMainNode =
                (NqlMainNode) nqlExpressionParser.Parse("| select 10 as number, name as name", diagnostics);
            var nqlSelectNode = (NqlSelectNode) nqlMainNode.ChildNodes.First();
            var nqlSelectPropertyNodes = nqlSelectNode.Fields.Cast<NqlSelectFieldNode>().ToArray();

            diagnostics.Should().HaveCount(0);
            nqlMainNode.ChildNodes.Should().HaveCount(1);
            nqlSelectPropertyNodes[0].Name.Should().Be("number");
            ((NqlConstantNode) nqlSelectPropertyNodes[0].Value).Value.Should().Be(10);
            nqlSelectPropertyNodes[1].Name.Should().Be("name");
            ((NqlFieldNode) nqlSelectPropertyNodes[1].Value).Name.Should().Be("name");
        }
    }
}