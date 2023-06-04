using System.Linq.Expressions;
using System.Reflection;

namespace Lazy.Util.EntityModelMapper.Internal;

public static class MapperExpressionToMemberBuilder
{
    public static MemberInfo SourceExpression<T, TP>(Expression<Func<T, TP>> expression)
    {
        var prop = TestSourceExpression(expression.Body, typeof(T));

        if (prop is null)
            throw new ArgumentException("No suitable field or property found.");

        return prop;
    }

    private static MemberInfo? TestSourceExpression(Expression ex, Type type)
    {
        switch (ex)
        {
            case UnaryExpression ue:
                return TestSourceExpression(ue.Operand, type);

            case BinaryExpression be:
                return TestSourceExpression(be.Left, type) ?? TestSourceExpression(be.Right, type);

            case MethodCallExpression mce:
                foreach (var exParameter in mce.Arguments)
                {
                    var pmc = TestSourceExpression(exParameter, type);
                    if (pmc is not null) return pmc;
                }

                return null;

            case MemberExpression me:
                var p = me.Member;
                if (p.ReflectedType == null ||
                    (type != p.ReflectedType && !type.IsSubclassOf(p.ReflectedType)))
                    return null;
                return p;

            default:
                return null;
        }
    }

    public static MemberInfo DestinationMember<T, TP>(Expression<Func<T, TP>> expression)
    {
        if (expression.Body is not MemberExpression memberExpression)
            throw new ArgumentException($"Expression '{expression.Name}' does not refer to a field or property.");

        var type = typeof(T);

        if (memberExpression.Member.ReflectedType == null ||
            (type != memberExpression.Member.ReflectedType &&
             !type.IsSubclassOf(memberExpression.Member.ReflectedType)))
            throw new ArgumentException(
                $"Expresion '{memberExpression.Member.Name}' refers to a property that is not from type {type}.");
        return memberExpression.Member;
    }
}