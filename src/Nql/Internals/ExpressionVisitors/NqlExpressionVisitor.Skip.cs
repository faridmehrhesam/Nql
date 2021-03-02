using System.Linq;
using System.Linq.Expressions;
using Nql.Abstractions.Nodes;

namespace Nql.Internals.ExpressionVisitors
{
    internal partial class NqlExpressionVisitor
    {
        public Expression VisitSkip(NqlSkipNode node)
        {
            return Expression.Call(
                typeof(Queryable),
                nameof(Queryable.Skip),
                new[] {source.ToElementType()},
                source,
                Visit(node.Count));
        }
    }
}