using System.Linq;
using System.Reflection;
using FluentAssertions;
using Moq;
using Nukito.Internal;
using Nukito.Test.Scenario;
using Nukito.Test.Utility;

namespace Nukito.Test.Unit
{
  public class CreatorTest
  {
    private readonly Context _context;

    public CreatorTest()
    {
      _context = new Context(new NukitoSettings());
    }

    [NukitoFact]
    public void CreateShouldReturnSingletons(Resolver resolver, Mock<IRepository> handler, Mock<IConstructorChooser> chooser)
    {
      // Arrange
      handler.Setup(h => h.CreateMock(typeof (IA), _context)).Returns(new Mock<IA>().Object);
      chooser.Setup(c => c.GetConstructor(typeof (A))).Returns(GetLoneConstructor<A>());

      // Act
      object a0 = resolver.GetOrCreate(typeof (IA), _context);
      object a1 = resolver.GetOrCreate(typeof (A), _context);
      object a2 = resolver.GetOrCreate(typeof (IA), _context);
      object a3 = resolver.GetOrCreate(typeof (A), _context);

      // Assert
      a0.Should().BeSameAs(a2);
      a1.Should().BeSameAs(a3);
      handler.Verify(h => h.CreateMock(typeof (IA), _context), Times.Once());
      chooser.Verify(c => c.GetConstructor(typeof (A)), Times.Once());
    }

    [NukitoFact]
    public void CreateMockedInterface(Resolver resolver, Mock<IRepository> handler)
    {
      // Arrange
      var a = new Mock<IA>().Object;
      handler.Setup(h => h.CreateMock(typeof (IA), _context)).Returns(a);

      // Act
      object result = resolver.GetOrCreate(typeof (IA), _context);

      // Assert
      result.Should().BeSameAs(a);
    }

    [NukitoFact]
    public void CreateConcreteClass(Resolver resolver, Mock<IConstructorChooser> chooser)
    {
      // Arrange
      chooser.Setup(c => c.GetConstructor(typeof (A))).Returns(GetLoneConstructor<A>());

      // Act
      object result = resolver.GetOrCreate(typeof (A), _context);

      // Assert
      result.Should().BeOfType<A>();
    }

    [NukitoFact]
    public void CreateValueType(Resolver resolver)
    {
      // Act
      var valueType = resolver.GetOrCreate(typeof (double), _context);

      // Assert
      valueType.Should().Be(0.0);
    }

    [NukitoFact]
    public void CreateClassWithTransitiveDependencies(Resolver resolver, Mock<IRepository> handler, Mock<IConstructorChooser> chooser)
    {
      // Arrange
      chooser.Setup(c => c.GetConstructor(typeof (TransDep))).Returns(GetLoneConstructor<TransDep>());
      chooser.Setup(c => c.GetConstructor(typeof (DepOnClass))).Returns(GetLoneConstructor<DepOnClass>());
      chooser.Setup(c => c.GetConstructor(typeof (A))).Returns(GetLoneConstructor<A>());
      chooser.Setup(c => c.GetConstructor(typeof (DepOnInterface))).Returns(GetLoneConstructor<DepOnInterface>());
      handler.Setup(h => h.CreateMock(typeof (IA), _context)).Returns(new Mock<IA>().Object);

      // Act
      object result = resolver.GetOrCreate(typeof (TransDep), _context);

      // Assert
      var transDep = (TransDep) result;
      transDep.DepOnInterface.A.Should().BeMock<IA>();
      transDep.DepOnClass.A.Should().NotBeMock();
    }

    private ConstructorInfo GetLoneConstructor<T> ()
    {
      return typeof (T).GetConstructors ().Single ();
    }
  }
}