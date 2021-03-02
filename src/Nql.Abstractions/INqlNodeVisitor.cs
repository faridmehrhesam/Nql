using Nql.Abstractions.Nodes;

namespace Nql.Abstractions
{
    public interface INqlNodeVisitor<out T>
    {
        T Visit(NqlNode node);

        T VisitMain(NqlMainNode node);

        T VisitTake(NqlTakeNode node);

        T VisitSkip(NqlSkipNode node);

        T VisitConstant(NqlConstantNode node);
    }
}