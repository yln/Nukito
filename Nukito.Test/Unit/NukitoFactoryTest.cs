using FluentAssertions;
using Nukito.Internal;
using Nukito.Test.Scenario;
using Xunit.Sdk;

namespace Nukito.Test.Unit
{
  public class NukitoFactoryTest
  {
    public void Foo()
    {
    }

    public void Bar(IA a)
    {
    }

    [NukitoFact]
    public void ShouldReturnNukiotFactCommandForMethodsWithoutParameters()
    {
      // Arrange
      IMethodInfo method = Reflector.Wrap(GetType().GetMethod("Foo"));

      // Act
      ITestCommand command = NukitoFactory.CreateCommand(method, new NukitoSettings());

      // Assert
      command.Should().BeOfType<NukitoFactCommand>();
    }

    [NukitoFact]
    public void ShouldReturnNukitoFactCommandForMethodsWithParameters()
    {
      // Arrange
      IMethodInfo method = Reflector.Wrap(GetType().GetMethod("Bar"));

      // Act
      ITestCommand command = NukitoFactory.CreateCommand(method, new NukitoSettings());

      // Assert
      command.Should().BeOfType<NukitoFactCommand>();
    }
  }
}