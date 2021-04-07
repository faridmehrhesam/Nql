using System;

namespace Nql.Abstractions.Nodes
{
    public class NqlConstantNode : NqlNode
    {
        public NqlConstantNode(object value, Type type, NqlNodeLocation location)
            : base(location)
        {
            Value = value;
            Type = type;
        }

        public Type Type { get; }

        public object Value { get; }

        public override T Accept<T>(INqlNodeVisitor<T> visitor)
        {
            return visitor.VisitConstant(this);
        }
    }
}