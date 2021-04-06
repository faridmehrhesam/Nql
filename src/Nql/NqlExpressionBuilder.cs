using System.Linq;
using System.Linq.Expressions;
using Nql.Abstractions;
using Nql.Abstractions.Enums;
using Nql.Abstractions.Nodes;
using Nql.Exceptions;
using Nql.Internals.ExpressionVisitors;

namespace Nql
{
    public class NqlExpressionBuilder : INqlExpressionBuilder
    {
        private readonly INqlExpressionParser nqlExpressionParser;

        private readonly INqlTypeBuilder typeBuilder;

        public NqlExpressionBuilder(INqlExpressionParser nqlExpressionParser, INqlTypeBuilder typeBuilder)
        {
            this.nqlExpressionParser = nqlExpressionParser;
            this.typeBuilder = typeBuilder;
        }

        public Expression Build(string nqlString, Expression source)
        {
            var diagnostics = new Diagnostics();
            var nqlNode = nqlExpressionParser.Parse(nqlString, diagnostics);

            if (diagnostics.Any(i => i.Level == DiagnosticsLevel.Error))
                throw new NqlParseException(diagnostics.ToArray());

            return Build(nqlNode, source);
        }

        public Expression Build(NqlNode nqlNode, Expression source)
        {
            return new NqlExpressionVisitor(source, typeBuilder).Visit(nqlNode);
        }
    }
}