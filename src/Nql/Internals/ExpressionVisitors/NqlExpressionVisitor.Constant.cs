using System.Linq.Expressions;
using Nql.Abstractions.Nodes;

namespace Nql.Internals.ExpressionVisitors
{
    internal partial class NqlExpressionVisitor
    {
        public Expression VisitConstant(NqlConstantNode node)
        {
            return Expression.Constant(node.Value, node.Type);
        }
    }
}