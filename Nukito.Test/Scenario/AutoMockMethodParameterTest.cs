using FluentAssertions;
using Moq;
using Nukito.TestRealm;

namespace Nukito.Scenario
{
  public class AutoMockMethodParameterTest
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
    public void MockForInterface(IA a)
    {
      // Assert
      a.Should().BeAssignableTo<IMocked>();
    }

    [NukitoFact]
    public void MockForMultipleParameters(IA a, IB b)
    {
      // Assert
      a.Should().BeAssignableTo<IMocked>();
      b.Should().BeAssignableTo<IMocked>();
    }
  }
}