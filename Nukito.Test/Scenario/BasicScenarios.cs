using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  public class BasicScenarios
  {
    [NukitoFact]
    public void SelfBindingForBuiltInType(object o)
    {
      // Assert
      o.Should().BeOfType<object>();
    }

    [NukitoFact]
    public void SelfBindingForConcreteType(A a)
    {
      // Assert
      a.Should().BeOfType<A>();
    }

    [NukitoFact]
    public void MockedInterface(IA a)
    {
      // Assert
      a.Should().BeAssignableTo<IMocked<IA>>();
    }

    [NukitoFact]
    public void MockedInterfaceMultipleParameters(IA a, IB b)
    {
      // Assert
      a.Should().BeAssignableTo<IMocked<IA>>();
      b.Should().BeAssignableTo<IMocked<IB>>();
    }

    [NukitoFact]
    public void MockType(Mock<IA> mock)
    {
      // Assert
      mock.Should().BeOfType<Mock<IA>>();
    }
  }
}