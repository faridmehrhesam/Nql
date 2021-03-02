namespace Nql.Abstractions.Nodes
{
    public class NqlTakeNode : NqlNode
    {
        public NqlTakeNode(NqlNode count, NqlNodeLocation location)
            : base(location)
        {
            Count = count;
        }

        public NqlNode Count { get; }

        public override T Accept<T>(INqlNodeVisitor<T> visitor)
        {
            return visitor.VisitTake(this);
        }
    }
}