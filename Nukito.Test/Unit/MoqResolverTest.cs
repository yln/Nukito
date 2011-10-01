using FluentAssertions;
using Moq;
using Nukito.Internal;
using Nukito.Test.Scenario;
using Nukito.Test.Utility;

namespace Nukito.Test.Unit
{
  public class MoqResolverTest
  {
    private readonly MoqResolver _resolver = (MoqResolver) new NukitoFactory().NewResolver();

    [NukitoFact]
    public void IsInvalidMockType()
    {
      // Act + Assert
      _resolver.IsInvalidMockType(typeof (IA)).Should().BeFalse();
      _resolver.IsInvalidMockType(typeof (Mock)).Should().BeTrue();
      _resolver.IsInvalidMockType(typeof (Mock<IA>)).Should().BeFalse();
    }

    [NukitoFact]
    public void IsMockType()
    {
      // Act + Assert
      _resolver.IsMockType(typeof (IA)).Should().BeFalse();
      _resolver.IsMockType(typeof (Mock)).Should().BeFalse();
      _resolver.IsMockType(typeof (Mock<IA>)).Should().BeTrue();
    }

    [NukitoFact]
    public void CreateMock()
    {
      // Act
      Mock mock = _resolver.CreateMock(typeof (Mock<IA>));

      // Assert
      mock.Should().BeOfType<Mock<IA>>();
    }

    [NukitoFact]
    public void GetMockedInterface()
    {
      // Act
      object iface = _resolver.Get(typeof (IA));

      // Assert
      iface.Should().BeMock().And.BeAssignableTo<IA>();
    }

    [NukitoFact]
    public void GetSelfBindableConcreteClass()
    {
      // Act
      object aClass = _resolver.Get(typeof (A));

      // Assert
      aClass.Should().BeOfType<A>();
    }

    [NukitoFact]
    public void GetMockType()
    {
      // Act
      object mock = _resolver.Get(typeof (Mock<IA>));

      // Assert
      mock.Should().BeOfType<Mock<IA>>();
    }
  }
}