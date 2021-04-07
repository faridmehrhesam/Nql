using System.Linq.Expressions;
using Nql.Abstractions.Nodes;

namespace Nql.Abstractions
{
    public interface INqlExpressionBuilder
    {
        Expression Build(string nqlString, Expression source);

        Expression Build(NqlNode nqlNode, Expression source);
    }
}