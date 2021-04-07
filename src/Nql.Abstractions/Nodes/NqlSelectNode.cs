namespace Nql.Abstractions.Nodes
{
    public class NqlSelectNode : NqlNode
    {
        public NqlSelectNode(NqlNode[] fields, NqlNodeLocation location)
            : base(location)
        {
            Fields = fields;
        }

        public NqlNode[] Fields { get; }

        public override T Accept<T>(INqlNodeVisitor<T> visitor)
        {
            return visitor.VisitSelect(this);
        }
    }
}