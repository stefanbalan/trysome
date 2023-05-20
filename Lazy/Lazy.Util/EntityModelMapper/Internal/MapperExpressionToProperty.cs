using System.Linq.Expressions;
using System.Reflection;

namespace Lazy.Util.EntityModelMapper.Internal;

public static class MapperExpressionToProperty
{
    public static PropertyInfo SourceExpression<T, TP>(Expression<Func<T, TP>> expression)
    {
        var prop = TestExpression(expression.Body, typeof(T));

        if (prop is null)
            throw new ArgumentException("No suitable field or property found.");

        return prop;

        PropertyInfo? TestExpression(Expression ex, Type type)
        {
            switch (ex)
            {
                case UnaryExpression ue:
                    return TestExpression(ue.Operand, type);

                case LambdaExpression le:
                    foreach (var exParameter in le.Parameters)
                    {
                        var pi = TestExpression(exParameter, type);
                        if (pi is not null) return pi;
                    }

                    break;

                case MethodCallExpression mce:
                    foreach (var exParameter in mce.Arguments)
                    {
                        var pi = TestExpression(exParameter, type);
                        if (pi is not null) return pi;
                    }

                    break;

                case MemberExpression me:
                    var p = me.Member as PropertyInfo;
                    if (p?.ReflectedType == null ||
                        (type != p.ReflectedType && !type.IsSubclassOf(p.ReflectedType)))
                        return null;
                    return p;

                default:
                    return null;
            }

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
