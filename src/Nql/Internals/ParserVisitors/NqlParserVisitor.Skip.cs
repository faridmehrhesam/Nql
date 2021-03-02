using Nql.Abstractions.Nodes;
using Nql.Internals.Grammers;

namespace Nql.Internals.ParserVisitors
{
    internal partial class NqlParserVisitor
    {
        public override NqlNode VisitSkip(NqlParser.SkipContext context)
        {
            var skipExpressionContext = context.skipExpression();
            var count = Visit(skipExpressionContext.integer());

            return new NqlSkipNode(count, context.ToLocation());
        }
    }
}