using System.Linq.Expressions;
using Nql.Abstractions;
using Nql.Abstractions.Nodes;

namespace Nql.Internals.ExpressionVisitors
{
    internal partial class NqlExpressionVisitor : INqlNodeVisitor<Expression>
    {
        private Expression source;

        internal NqlExpressionVisitor(Expression source)
        {
            this.source = source;
        }

        public Expression Visit(NqlNode node)
        {
            return node?.Accept(this);
        }
    }
}