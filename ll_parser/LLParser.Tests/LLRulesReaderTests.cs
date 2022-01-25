using System;
using System.IO;
using NUnit.Framework;

namespace LLParser.Tests;
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LLRulesReader_Read_CorrectRules()
    {
        //Arrange
        Stream input = File.OpenRead( Path.Combine( Environment.CurrentDirectory, "rules.txt" ) );
        LLRulesReader rulesReader = new( input );

        //Act
        var rules = rulesReader.Read();

        //ASSert
        Assert.That( rules.Rules.Count, Is.EqualTo( 3 ) );

        LLRule firstRule = rules.Rules[ 0 ];
        Assert.That( firstRule.Id, Is.EqualTo( 1 ) );
        Assert.That( firstRule.GuideSymbols.Count, Is.EqualTo( 4 ) );
        Assert.That( firstRule.Shift, Is.False );
        Assert.That( firstRule.ThrowIfNotMatch, Is.True );
        Assert.That( firstRule.NextRuleId, Is.EqualTo( 1 ) );
        Assert.That( firstRule.StackNextRule, Is.False );
        Assert.That( firstRule.IsEnd, Is.False );

        LLRule secondRule = rules.Rules[ 1 ];
        Assert.That( secondRule.Id, Is.EqualTo( 3 ) );
        Assert.That( secondRule.GuideSymbols.Count, Is.EqualTo( 1 ) );
        Assert.That( secondRule.Shift, Is.True );
        Assert.That( secondRule.ThrowIfNotMatch, Is.False );
        Assert.That( secondRule.NextRuleId, Is.EqualTo( null ) );
        Assert.That( secondRule.StackNextRule, Is.True );
        Assert.That( secondRule.IsEnd, Is.True );

        LLRule thirdRule = rules.Rules[ 2 ];
        Assert.That( thirdRule.Id, Is.EqualTo( 16 ) );
        Assert.That( thirdRule.GuideSymbols.Count, Is.EqualTo( 0 ) );
        Assert.That( thirdRule.Shift, Is.False );
        Assert.That( thirdRule.ThrowIfNotMatch, Is.True );
        Assert.That( thirdRule.NextRuleId, Is.EqualTo( 27 ) );
        Assert.That( thirdRule.StackNextRule, Is.True );
        Assert.That( thirdRule.IsEnd, Is.False );
    }
}