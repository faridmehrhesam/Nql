using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nql.Abstractions;
using Nql.Abstractions.Nodes;

namespace Nql.Internals.ExpressionVisitors
{
    internal partial class NqlExpressionVisitor
    {
        public override Expression VisitSelect(NqlSelectNode node)
        {
            var sourceElementType = source.ToElementType();
            var newTypeFields = new List<NqlField>();
            var nameValueMappings = new Dictionary<string, Expression>();

            sourceParameter = Expression.Parameter(sourceElementType);

            foreach (var field in node.Fields.Cast<NqlSelectFieldNode>())
            {
                var fieldValue = Visit(field.Value);

                nameValueMappings.Add(field.Name, fieldValue);
                newTypeFields.Add(new NqlField
                {
                    Name = field.Name,
                    FieldType = fieldValue.Type
                });
            }

            var newType = nqlTypeBuilder.Build(newTypeFields.ToArray());
            var memberAssignments = newType.GetProperties().Select(i => Expression.Bind(i, nameValueMappings[i.Name]));
            var memberInit = Expression.MemberInit(Expression.New(newType), memberAssignments);
            var expression = Expression.Call(
                typeof(Queryable),
                nameof(Queryable.Select),
                new[] {sourceParameter.Type, newType},
                source,
                Expression.Lambda(memberInit, sourceParameter));

            sourceParameter = null;

            return expression;
        }
    }
}