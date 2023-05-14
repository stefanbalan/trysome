using System.Linq.Expressions;
using System.Reflection;

namespace Lazy.Util.EntityModelMapper.Internal;

public class MapperExpressionToProperty
{
    public static PropertyInfo SourcePropertyInfo<T, TP>(Expression<Func<T, TP>> expression)
    {
        var prop = TestExpression(expression.Body, typeof(T));

        if (prop is null)
            //throw new ArgumentException($"Expression '{memberExpression1.Member.Name}' refers to a field, not a property.");
            throw new ArgumentException("No property found.");

        return prop;

        PropertyInfo? TestExpression(Expression ex, Type type)
        {
            switch (ex)
            {
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

    public static PropertyInfo DestinationPropertyInfo<T, TP>(Expression<Func<T, TP>> expression)
    {
        var memberExpression = expression.Body as MemberExpression ??
                               ((UnaryExpression)expression.Body).Operand as MemberExpression;

        var type = typeof(T);
        if (memberExpression == null)
            throw new ArgumentException($"Expression '{expression.Name}' refers to a method, not a property.");

        var propInfo = memberExpression.Member as PropertyInfo;
        if (propInfo == null)
            throw new ArgumentException(
                $"Expression '{memberExpression.Member.Name}' refers to a field, not a property.");

        if (propInfo.ReflectedType == null ||
            (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType)))
            throw new ArgumentException(
                $"Expresion '{memberExpression.Member.Name}' refers to a property that is not from type {type}.");
        return propInfo;
    }
}