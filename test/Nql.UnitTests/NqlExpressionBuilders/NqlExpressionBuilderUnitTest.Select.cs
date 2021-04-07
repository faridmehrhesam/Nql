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
        public void TestVisitSelect_WhenEverythingIsOk_TakeMustBeAppliedOnSource()
        {
            var source = Enumerable.Range(1, 10).Select(i => new {Name = i}).AsQueryable();
            var nqlExpressionBuilder = new NqlExpressionBuilder(null, new NqlTypeBuilder());
            var selectPropertyNode = new NqlSelectFieldNode("NewName", new NqlFieldNode("Name", null), null);
            var nqlSelectNode = new NqlSelectNode(new NqlNode[] {selectPropertyNode}, null);
            var expression = nqlExpressionBuilder.Build(nqlSelectNode, source.Expression);
            var actual = (IQueryable) Expression.Lambda(expression).Compile().DynamicInvoke();
            var expected = source.Select(i => new {NewName = i.Name});

            actual.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }
    }
}