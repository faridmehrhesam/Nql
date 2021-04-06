using System.Linq;
using System.Linq.Expressions;
using Nql.Abstractions.Nodes;

namespace Nql.Internals.ExpressionVisitors
{
    internal partial class NqlExpressionVisitor
    {
        public override Expression VisitTake(NqlTakeNode node)
        {
            return Expression.Call(
                typeof(Queryable),
                nameof(Queryable.Take),
                new[] {source.ToElementType()},
                source,
                Visit(node.Count));
        }
    }
}