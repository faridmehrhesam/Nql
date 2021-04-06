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
        public void TestVisitSkip_WhenEverythingIsOk_SkipMustBeAppliedOnSource()
        {
            var source = Enumerable.Range(1, 10).Select(i => i).AsQueryable();
            var nqlExpressionBuilder = new NqlExpressionBuilder(null, new NqlTypeBuilder());
            var nqlConstantNode = new NqlConstantNode(5, typeof(int), null);
            var nqlSkipNode = new NqlSkipNode(nqlConstantNode, null);
            var expression = nqlExpressionBuilder.Build(nqlSkipNode, source.Expression);
            var actual = (IQueryable) Expression.Lambda(expression).Compile().DynamicInvoke();

            actual.Should().BeEquivalentTo(source.Skip(5), options => options.WithStrictOrdering());
        }
    }
}