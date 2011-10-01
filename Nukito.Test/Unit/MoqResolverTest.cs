using FluentAssertions;
using Moq;
using Nukito.Internal;
using Nukito.Test.Scenario;

namespace Nukito.Test.Unit
{
  public class MoqResolverTest
  {
    [NukitoFact]
    public void IsInvalidMockType(MoqResolver resolver)
    {
      // Act + Assert
      resolver.IsInvalidMockType(typeof (IA)).Should().BeFalse();
      resolver.IsInvalidMockType(typeof (Mock)).Should().BeTrue();
      resolver.IsInvalidMockType(typeof (Mock<IA>)).Should().BeFalse();
    }

    [NukitoFact]
    public void IsMockType(MoqResolver resolver)
    {
      // Act + Assert
      resolver.IsMockType(typeof (IA)).Should().BeFalse();
      resolver.IsMockType(typeof (Mock)).Should().BeFalse();
      resolver.IsMockType(typeof (Mock<IA>)).Should().BeTrue();
    }

    [NukitoFact]
    public void CreateMock(Mock<ICreator> collaborator, MoqResolver resolver)
    {
      // Arrange
      var mock = new Mock<IA>();
      collaborator.Setup(c => c.Create(typeof (IA))).Returns(mock.Object);

      // Act
      object result = resolver.CreateMock(typeof (Mock<IA>));

      // Assert
      result.Should().BeSameAs(mock);
    }

    [NukitoFact]
    public void GetMockedInterface(Mock<ICreator> collaborator, MoqResolver resolver)
    {
      // Arrange
      var mock = new Mock<IA>();
      collaborator.Setup(c => c.Create(typeof (IA))).Returns(mock.Object);

      // Act
      object result = resolver.Get(typeof (IA));

      // Assert
      result.Should().BeSameAs(mock.Object);
    }

    [NukitoFact]
    public void GetSelfBindableConcreteClass(Mock<ICreator> collaborator, MoqResolver resolver)
    {
      // Arrange
      var a = new A();
      collaborator.Setup(c => c.Create(typeof (A))).Returns(a);

      // Act
      object result = resolver.Get(typeof (A));

      // Assert
      result.Should().BeSameAs(a);
    }

    [NukitoFact]
    public void GetMockType(Mock<ICreator> collaborator, MoqResolver resolver)
    {
      // Arrange
      var mock = new Mock<IA>();
      collaborator.Setup(c => c.Create(typeof (IA))).Returns(mock.Object);

      // Act
      object result = resolver.Get(typeof (Mock<IA>));

      // Assert
      result.Should().BeSameAs(mock);
    }
  }
}