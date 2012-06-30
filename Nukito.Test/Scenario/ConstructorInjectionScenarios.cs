using FluentAssertions;

namespace Nukito.Test.Scenario
{
  public class ConstructorInjectionScenarios
  {
    private readonly IA _a;

    public ConstructorInjectionScenarios (IA a)
    {
      _a = a;
    }

    [NukitoFact]
    public void TestWithoutParameters ()
    {
      // Assert
      _a.Should().NotBeNull();
    }

    [NukitoFact]
    public void TestWithParameters (IA a)
    {
      // Assert
      _a.Should().BeSameAs(a);
    }
  }
}