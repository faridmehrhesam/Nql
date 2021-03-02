using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Nql.Abstractions.Nodes;
using Xunit;

namespace Nql.UnitTests.NqlExpressionBuilders
{
    public partial class NqlExpressionBuilderUnitTest
    {
        [Fact]
        public void TestVisit_WhenMultipleIdentifiers_AllNodesMustBeApplied()
        {
            var source = Enumerable.Range(1, 10).Select(i => i).AsQueryable();
            var nqlExpressionBuilder = new NqlExpressionBuilder(null);
            var nqlConstantNode = new NqlConstantNode(5, typeof(int), null);
            var nqlTakeNode = new NqlTakeNode(nqlConstantNode, null);
            var nqlSkipNode = new NqlSkipNode(nqlConstantNode, null);
            var nqlMainNode = new NqlMainNode(new NqlNode[] {nqlTakeNode, nqlSkipNode}, null);
            var expression = nqlExpressionBuilder.Build(nqlMainNode, source.Expression);
            var actual = (IQueryable) Expression.Lambda(expression).Compile().DynamicInvoke();

            actual.Should().BeEquivalentTo(source.Take(5).Skip(5), options => options.WithStrictOrdering());
        }
    }
}