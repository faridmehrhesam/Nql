using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Antlr4.Runtime;
using Nql.Abstractions;

[assembly: InternalsVisibleTo("Nql.UnitTests")]

namespace Nql.Internals
{
    internal static class Extensions
    {
        internal static NqlNodeLocation ToLocation(this ParserRuleContext context)
        {
            return new NqlNodeLocation(
                context.Start.Line,
                context.Start.Column,
                context.stop.Line,
                context.stop.Column
            );
        }

        internal static Type ToElementType(this Expression expression)
        {
            return expression.Type.HasElementType
                ? expression.Type.GetElementType()
                : expression.Type.GenericTypeArguments[0];
        }
    }
}