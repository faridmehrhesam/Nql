using System.Linq;
using Nql.Abstractions.Nodes;
using Nql.Internals.Grammers;

namespace Nql.Internals.ParserVisitors
{
    internal partial class NqlParserVisitor
    {
        public override NqlNode VisitSelect(NqlParser.SelectContext context)
        {
            return new NqlSelectNode(
                context.selectExpression().selectField().Select(Visit).ToArray(),
                context.ToLocation());
        }

        public override NqlNode VisitSelectField(NqlParser.SelectFieldContext context)
        {
            return new NqlSelectFieldNode(context.Name.Text, Visit(context.value()), context.ToLocation());
        }
    }
}