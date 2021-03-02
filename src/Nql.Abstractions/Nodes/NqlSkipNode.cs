namespace Nql.Abstractions.Nodes
{
    public class NqlSkipNode : NqlNode
    {
        public NqlSkipNode(NqlNode count, NqlNodeLocation location)
            : base(location)
        {
            Count = count;
        }

        public NqlNode Count { get; }

        public override T Accept<T>(INqlNodeVisitor<T> visitor)
        {
            return visitor.VisitSkip(this);
        }
    }
}