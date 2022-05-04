using System;
using System.Linq.Expressions;
using FluentAssertions;
using Xunit;

namespace ruleEngine.Test;

public class RuleEngineTests
{
    [Fact]
    public void CompilesRule()
    {
        // Arrange
        var ruleEngine = new RuleEngine();
        var user = new User("Alex", 24);
        var rule = new Rule(
            "Age",
            "GreaterThan",
            "42");

        // Act
        var compiledRule = ruleEngine.CompileRule<User>(rule);

        // Assert
        compiledRule.Should().BeOfType<Func<User, bool>>();
    }

    [Fact]
    public void RuleShouldValidate()
    {
        // Arrange
        var ruleEngine = new RuleEngine();
        var user = new User("Alex", 2);
        var rule = new Rule(
            "Age",
            "Equal",
            "42");

        var compiledRule = ruleEngine.CompileRule<User>(rule);

        // Act
        var ruleResult = compiledRule(user);

        // Assert
        ruleResult.Should().Be(true);
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