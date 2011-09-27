using System;
using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  public class MockBehaviorScenarios
  {
    [NukitoFact]
    public void DefaultMockBehavior(Mock<IA> mock)
    {
      // Assert (Default -> Loose)
      mock.Behavior.Should().Be(MockBehavior.Default).And.Be(MockBehavior.Loose);
    }

    [NukitoFact]
    public void MockBehaviorCanBeConfigured(Mock<IA> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Strict);
      // TODO: find nice way to allow this
    }

    public class Bla
    {
      [NukitoFact]
      public void Blub(Mock<IA> mock)
      {
        throw new Exception("fail");
        // TODO: settings on class level
      }
    }
  }
}