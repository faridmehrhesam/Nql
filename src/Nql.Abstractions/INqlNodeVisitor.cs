using Nql.Abstractions.Nodes;

namespace Nql.Abstractions
{
    public interface INqlNodeVisitor<out T>
    {
        T Visit(NqlNode node);

        T VisitMain(NqlMainNode node);

        T VisitSelect(NqlSelectNode node);

        T VisitSkip(NqlSkipNode node);

        T VisitTake(NqlTakeNode node);

        T VisitConstant(NqlConstantNode node);

        T VisitField(NqlFieldNode node);

        T VisitSelectField(NqlSelectFieldNode node);
    }
}