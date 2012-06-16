using FluentAssertions;
using Moq;
using Nukito.Internal;
using Nukito.Internal.Moq;
using Nukito.Test.Scenario;

namespace Nukito.Test.Unit.Moq
{
  public class MoqResolverTest
  {
    private readonly Context _context;

    public MoqResolverTest()
    {
      _context = new Context (new NukitoSettings());
    }



    [NukitoFact]
    public void CreateMock(Mock<ICreator> collaborator, MoqResolver mockRepository)
    {
      // Arrange
      var mock = new Mock<IA>();
      collaborator.Setup(c => c.GetOrCreate(typeof (IA), _context)).Returns(mock.Object);

      // Act
      object result = mockRepository.CreateMock(typeof (Mock<IA>), _context);

      // Assert
      result.Should().BeSameAs(mock);
    }

    [NukitoFact]
    public void GetMockedInterface(Mock<ICreator> collaborator, MoqResolver mockRepository)
    {
      // Arrange
      var mock = new Mock<IA>();
      collaborator.Setup(c => c.GetOrCreate(typeof (IA), _context)).Returns(mock.Object);

      // Act
      object result = mockRepository.Get(typeof (IA), _context);

      // Assert
      result.Should().BeSameAs(mock.Object);
    }

    [NukitoFact]
    public void GetSelfBindableConcreteClass(Mock<ICreator> collaborator, MoqResolver mockRepository)
    {
      // Arrange
      var a = new A();
      collaborator.Setup(c => c.GetOrCreate(typeof (A), _context)).Returns(a);

      // Act
      object result = mockRepository.Get(typeof (A), _context);

      // Assert
      result.Should().BeSameAs(a);
    }

    [NukitoFact]
    public void GetMockType(Mock<ICreator> collaborator, MoqResolver mockRepository)
    {
      // Arrange
      var mock = new Mock<IA>();
      collaborator.Setup(c => c.GetOrCreate(typeof (IA), _context)).Returns(mock.Object);

      // Act
      object result = mockRepository.Get(typeof (Mock<IA>), _context);

      // Assert
      result.Should().BeSameAs(mock);
    }
  }
}