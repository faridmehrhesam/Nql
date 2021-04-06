namespace Nql.Abstractions.Nodes
{
    public class NqlSelectFieldNode : NqlNode
    {
        public NqlSelectFieldNode(string name, NqlNode value, NqlNodeLocation location)
            : base(location)
        {
            Value = value;
            Name = name;
        }

        public NqlNode Value { get; }

        public string Name { get; }

        public override T Accept<T>(INqlNodeVisitor<T> visitor)
        {
            return visitor.VisitSelectField(this);
        }
    }
}