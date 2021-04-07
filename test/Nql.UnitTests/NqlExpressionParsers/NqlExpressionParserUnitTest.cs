using FluentAssertions;
using Nql.Abstractions;
using Nql.Abstractions.Nodes;
using Xunit;

namespace Nql.UnitTests.NqlExpressionParsers
{
    public partial class NqlExpressionParserUnitTest
    {
        [Fact]
        public void TestVisit_WhenMultipleIdentifiers_AllNodesMustBeReturned()
        {
            var nqlExpressionParser = new NqlExpressionParser();
            var diagnostics = new Diagnostics();
            var nqlMainNode = (NqlMainNode) nqlExpressionParser.Parse("| take 10 | skip 10", diagnostics);

            diagnostics.Should().HaveCount(0);
            nqlMainNode.ChildNodes.Should().HaveCount(2);
        }
    }
}