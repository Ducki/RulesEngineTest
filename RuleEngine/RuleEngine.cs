using System.Linq.Expressions;

namespace ruleEngine;

public class RuleEngine
{
    public Func<T, bool> CompileRule<T>(Rule rule)
    {
        var ruleParameter = Expression.Parameter(typeof(T));
        var expression = BuildExpression<T>(rule, ruleParameter);

        return Expression.Lambda<Func<T, bool>>(expression, ruleParameter).Compile();
    }

    private Expression BuildExpression<T>(Rule rule, ParameterExpression ruleParameter)
    {
        var left = Expression.Property(ruleParameter, rule.MemberName);
        var op = BuildOperator(rule.Operator);

        var targetValuePropertyType = typeof(T).GetProperty(rule.MemberName)!.PropertyType;
        var rightValue = Convert.ChangeType(rule.TargetValue, targetValuePropertyType);
        Expression right = Expression.Constant(rightValue);

        return Expression.MakeBinary(op, left, right);
    }

    public ExpressionType BuildOperator(string op)
    {
        var conversionSucceeded = Enum.TryParse(op,
            out ExpressionType tBinary);
        if (!conversionSucceeded)
            throw new ArgumentException("Operator conversion failed", nameof(op));

        return tBinary;
    }
}