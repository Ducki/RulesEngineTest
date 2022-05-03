using System.Linq.Expressions;

namespace ruleEngine;

public class RuleEngine
{
    public Func<T, string> CompileRule<T>(Rule rule)
    {
        var ruleParameter = Expression.Parameter(typeof(T));
        var expression = BuildExpression<T>(rule, ruleParameter);

        return Expression.Lambda<Func<T, string>>(expression, ruleParameter).Compile();
    }

    private Expression BuildExpression<T>(Rule rule, ParameterExpression ruleParameter)
    {
        Expression e = Expression.Constant("hi");
        ExpressionType op = BuildOperator(rule.Operator);
        // var lambda = Expression.Lambda<Func<T, bool>>(e, ruleParameter);
        return e;
    }

    public ExpressionType BuildOperator(string op)
    {
        var conversionSucceeded = Enum.TryParse(op, out ExpressionType tBinary);
        if (!conversionSucceeded) throw new ArgumentException("Operator conversion failed", nameof(op));

        return tBinary;
    }
}