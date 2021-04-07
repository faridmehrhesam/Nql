using Nql.Abstractions.Nodes;

namespace Nql.Abstractions
{
    public class NqlNodeVisitor<T> : INqlNodeVisitor<T>
    {
        public virtual T Visit(NqlNode node)
        {
            return node.Accept(this);
        }

        public virtual T VisitMain(NqlMainNode node)
        {
            return default;
        }

        public virtual T VisitSelect(NqlSelectNode node)
        {
            return default;
        }

        public virtual T VisitSkip(NqlSkipNode node)
        {
            return default;
        }

        public virtual T VisitTake(NqlTakeNode node)
        {
            return default;
        }

        public virtual T VisitConstant(NqlConstantNode node)
        {
            return default;
        }

        public virtual T VisitField(NqlFieldNode node)
        {
            return default;
        }

        public virtual T VisitSelectField(NqlSelectFieldNode node)
        {
            return default;
        }
    }
}