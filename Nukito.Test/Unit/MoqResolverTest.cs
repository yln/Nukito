using FluentAssertions;
using Moq;
using Nukito.Internal;
using Nukito.Test.Scenario;

namespace Nukito.Test.Unit
{
  public class MoqResolverTest
  {
    private static readonly MoqResolver _Resolver = new MoqResolver();

    [NukitoFact]
    public void IsInvalidMockType()
    {
      // Act + Assert
      _Resolver.IsInvalidMockType(typeof (IA)).Should().BeFalse();
      _Resolver.IsInvalidMockType(typeof (Mock)).Should().BeTrue();
      _Resolver.IsInvalidMockType(typeof (Mock<IA>)).Should().BeFalse();
    }

    [NukitoFact]
    public void IsMockType()
    {
      // Act + Assert
      _Resolver.IsMockType(typeof (IA)).Should().BeFalse();
      _Resolver.IsMockType(typeof (Mock)).Should().BeFalse();
      _Resolver.IsMockType(typeof (Mock<IA>)).Should().BeTrue();
    }

    [NukitoFact]
    public void GetMock()
    {
      // Act
      Mock mock = _Resolver.GetMock(typeof (Mock<IA>));

      // Assert
      mock.Should().BeAssignableTo<Mock<IA>>();
    }

    [NukitoFact]
    public void GetMockedInterface()
    {
      // Act
      object iface = _Resolver.Get(typeof (IA));

      // Assert
      iface.Should().BeAssignableTo<IA>();
    }

    [NukitoFact]
    public void GetSelfBindableConcreteClass()
    {
      // Act
      object aClass = _Resolver.Get(typeof (A)); // self binding

      // Assert
      aClass.Should().BeOfType<A>();
    }

    [NukitoFact]
    public void GetMockType()
    {
      // Act
      object mock = _Resolver.Get(typeof (Mock<IA>));

      // Assert
      mock.Should().BeOfType<Mock<IA>>();
    }
  }
}