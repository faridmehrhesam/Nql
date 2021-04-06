using System;

namespace Nql.Abstractions
{
    public interface INqlTypeBuilder
    {
        Type Build(NqlField[] fields, Type parent = null);
    }
}