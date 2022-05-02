using System;
using System.Linq.Expressions;
using FluentAssertions;
using Xunit;

namespace ruleEngine.Test;

public class UnitTest1
{
    [Fact]
    public void CompilesRule()
    {
        // Arrange
        var ruleEngine = new RuleEngine();
        var rule = new Rule(
            MemberName: "Age",
            Operator: "GreaterThan",
            TargetValue: "42");
        
        // Act
        var compiledRule = ruleEngine.CompileRule<int>(rule);
        
        // Assert
        compiledRule.Should().BeOfType<Expression<Func<int, bool>>>();
    }
}