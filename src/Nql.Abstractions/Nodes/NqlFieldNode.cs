namespace Nql.Abstractions.Nodes
{
    public class NqlFieldNode : NqlNode
    {
        public NqlFieldNode(string name, NqlNodeLocation location)
            : base(location)
        {
            Name = name;
        }

        public string Name { get; }

        public override T Accept<T>(INqlNodeVisitor<T> visitor)
        {
            return visitor.VisitField(this);
        }
    }
}