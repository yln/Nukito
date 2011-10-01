using FluentAssertions;
using Nukito.Test.Utility;

namespace Nukito.Test.Scenario
{
  public class WithDependencyScenarios
  {
    [NukitoFact]
    public void ClassWithDependencyOnInterface(DepOnInterface depOnInterface)
    {
      // Assert
      depOnInterface.A.Should().BeMock<IA>();
    }

    [NukitoFact]
    public void ClassWithDependencyOnOtherClass(DepOnClass depOnClass)
    {
      // Assert
      depOnClass.A.Should().NotBeMock();
    }

    [NukitoFact]
    public void ClassWithTransitiveDependencyOnOtherClasses(TransDep transDep)
    {
      // Assert
      transDep.DepOnInterface.A.Should().BeMock<IA>();
      transDep.DepOnClass.A.Should().NotBeMock();
    }
  }
}