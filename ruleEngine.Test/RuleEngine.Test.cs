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
            "Age",
            "GreaterThan",
            "42");

        var user = new User("Alex", 24);

        // Act
        var compiledRule = ruleEngine.CompileRule<User>(rule);

        // Assert
        compiledRule.Should().BeOfType<Func<User, bool>>();
    }

    [Fact]
    public void BuildsGreaterThanOperator()
    {
        // Arrange
        var ruleEngine = new RuleEngine();

        // Act
        var operatorUnderTest = ruleEngine.BuildOperator("GreaterThan");

        // Assert
        operatorUnderTest.Should().Be(ExpressionType.GreaterThan);
    }
}