using FluentAssertions;

namespace Nukito.Test.Scenario
{
  public class WithDependencyScenarios
  {
    [NukitoFact]
    public void ClassWithDependencyOnInterface(DepOnInterface depOnInterface)
    {
      // Assert
      depOnInterface.A.Should().BeMock();
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
      transDep.DepOnInterface.A.Should().BeMock();
      transDep.DepOnClass.A.Should().NotBeMock();
    }
  }
}