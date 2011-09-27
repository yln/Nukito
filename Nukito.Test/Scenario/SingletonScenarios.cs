using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  public class SingletonScenarios
  {
    [NukitoFact]
    public void SelfBindableTypesAreSingletons(A a1, A a2)
    {
      // Assert
      a1.Should().BeSameAs(a2);
    }

    [NukitoFact]
    public void SelfBindableTypesInDifferentContextAreSingletonsToo(A a, DepOnClass depOnClass)
    {
      // Assert
      a.Should().BeSameAs(depOnClass.A);
    }

    [NukitoFact]
    public void MockedObjectsAreSingletons(IA a1, IA a2)
    {
      // Assert
      a1.Should().BeSameAs(a2);
    }

    [NukitoFact]
    public void MockedObjectsInDifferentContextAreSingletonsToo(IA a, DepOnInterface depOnInterface)
    {
      // Assert
      a.Should().BeSameAs(depOnInterface.A);
    }

    [NukitoFact]
    public void MocksAreSingletons(Mock<IA> mock1, Mock<IA> mock2)
    {
      // Assert
      mock1.Should().BeSameAs(mock2);
    }
  }
}