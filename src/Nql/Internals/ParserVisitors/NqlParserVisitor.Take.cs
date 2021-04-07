using Nql.Abstractions.Nodes;
using Nql.Internals.Grammers;

namespace Nql.Internals.ParserVisitors
{
    internal partial class NqlParserVisitor
    {
        public override NqlNode VisitTake(NqlParser.TakeContext context)
        {
            var takeExpressionContext = context.takeExpression();
            var count = Visit(takeExpressionContext.integer());

            return new NqlTakeNode(count, context.ToLocation());
        }
    }
}