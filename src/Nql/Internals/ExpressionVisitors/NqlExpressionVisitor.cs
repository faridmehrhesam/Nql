using System.Linq.Expressions;
using Nql.Abstractions;

namespace Nql.Internals.ExpressionVisitors
{
    internal partial class NqlExpressionVisitor : NqlNodeVisitor<Expression>
    {
        private readonly INqlTypeBuilder nqlTypeBuilder;

        private Expression source;

        private ParameterExpression sourceParameter;

        internal NqlExpressionVisitor(Expression source, INqlTypeBuilder nqlTypeBuilder)
        {
            this.source = source;
            this.nqlTypeBuilder = nqlTypeBuilder;
        }
    }
}