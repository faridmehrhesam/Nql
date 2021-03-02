using Nql.Abstractions;
using Nql.Abstractions.Nodes;
using Nql.Internals.Grammers;

namespace Nql.Internals.ParserVisitors
{
    internal partial class NqlParserVisitor
    {
        public override NqlNode VisitInteger(NqlParser.IntegerContext context)
        {
            var text = context.GetText();

            if (string.IsNullOrWhiteSpace(text))
                return null;

            var location = new NqlNodeLocation(
                context.start.Line,
                context.start.Column,
                context.stop.Line,
                context.start.Column + text.Length);

            return new NqlConstantNode(int.Parse(text), typeof(int), location);
        }
    }
}