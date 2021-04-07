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

            if (!int.TryParse(text, out var value))
                return null;

            var location = new NqlNodeLocation(
                context.start.Line,
                context.start.Column,
                context.stop.Line,
                context.start.Column + text.Length);

            return new NqlConstantNode(value, typeof(int), location);
        }

        public override NqlNode VisitField(NqlParser.FieldContext context)
        {
            return new NqlFieldNode(context.GetText(), context.ToLocation());
        }
    }
}