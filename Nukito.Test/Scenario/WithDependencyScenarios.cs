using FluentAssertions;

namespace Nukito.Test.Scenario
{
  public class WithDependencyScenarios
  {
    [NukitoFact]
    public void ClassWithDependencyOnInterface(DepOnInterface depOnInterface)
    {
      // Assert
      depOnInterface.A.Should().BeAssignableTo<IA>();
    }

    [NukitoFact]
    public void ClassWithDependencyOnOtherClass(DepOnClass depOnClass)
    {
      // Assert
      depOnClass.A.Should().BeOfType<A>();
    }
  }
}