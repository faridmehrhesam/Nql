using System.Linq;
using System.Linq.Expressions;
using Nql.Abstractions.Nodes;

namespace Nql.Internals.ExpressionVisitors
{
    internal partial class NqlExpressionVisitor
    {
        public override Expression VisitConstant(NqlConstantNode node)
        {
            return Expression.Constant(node.Value, node.Type);
        }

        public override Expression VisitField(NqlFieldNode node)
        {
            return node.Name
                .Split('.')
                .Aggregate<string, Expression>(null,
                    (field, fieldName) => Expression.Property(field ?? sourceParameter, fieldName));
        }
    }
}