using Nql.Abstractions.Nodes;

namespace Nql.Abstractions
{
    public interface INqlExpressionParser
    {
        NqlNode Parse(string nqlString, Diagnostics diagnostics);
    }
}