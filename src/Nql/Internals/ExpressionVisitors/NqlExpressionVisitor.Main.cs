using System.Linq.Expressions;
using Nql.Abstractions.Nodes;

namespace Nql.Internals.ExpressionVisitors
{
    internal partial class NqlExpressionVisitor
    {
        public Expression VisitMain(NqlMainNode node)
        {
            foreach (var childNode in node.ChildNodes)
                source = Visit(childNode);

            return source;
        }
    }
}