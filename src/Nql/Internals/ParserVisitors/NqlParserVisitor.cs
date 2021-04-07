using System.Linq;
using Antlr4.Runtime.Tree;
using Nql.Abstractions.Nodes;
using Nql.Internals.Grammers;

namespace Nql.Internals.ParserVisitors
{
    internal partial class NqlParserVisitor : NqlParserBaseVisitor<NqlNode>
    {
        public override NqlNode VisitNql(NqlParser.NqlContext context)
        {
            return new NqlMainNode(context.nqlExpression().Select(Visit).ToArray(), context.ToLocation());
        }

        protected override bool ShouldVisitNextChild(IRuleNode node, NqlNode currentResult)
        {
            return currentResult == null;
        }
    }
}