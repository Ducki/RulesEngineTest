using System;
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

        // Act
        var compiledRule = ruleEngine.CompileRule<int>(rule);

        // Assert
        compiledRule.Should().BeOfType<Func<int, string>>();
    }

    [Fact]
    public void BuildsOperator()
    {
        // Arrange
        var ruleEngine = new RuleEngine();

        // Act
        var act = Record.Exception(() =>
        {
            var _ = ruleEngine.BuildOperator("GreaterThan");
        });

        // Assert
        act.Should().BeNull();
    }
}