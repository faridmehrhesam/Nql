namespace Nql.Abstractions.Nodes
{
    public abstract class NqlNode
    {
        protected NqlNode(NqlNodeLocation location)
        {
            Location = location;
        }

        public NqlNodeLocation Location { get; }

        public abstract T Accept<T>(INqlNodeVisitor<T> visitor);
    }
}