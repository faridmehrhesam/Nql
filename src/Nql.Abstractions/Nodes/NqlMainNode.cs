namespace Nql.Abstractions.Nodes
{
    public class NqlMainNode : NqlNode
    {
        public NqlMainNode(NqlNode[] childNodes, NqlNodeLocation location)
            : base(location)
        {
            ChildNodes = childNodes;
        }

        public NqlNode[] ChildNodes { get; }

        public override T Accept<T>(INqlNodeVisitor<T> visitor)
        {
            return visitor.VisitMain(this);
        }
    }
}