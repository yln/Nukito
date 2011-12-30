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
    private ConstructorInfo GetLoneConstructor<T>()
    {
      return typeof (T).GetConstructors().Single();
    }

    [NukitoFact]
    internal void CreateShouldReturnSingletons(
      Creator creator, Mock<IMockHandler> handler, Mock<IConstructorChooser> chooser)
    {
      // Arrange
      handler.Setup(h => h.CreateMock(typeof (IA))).Returns(new Mock<IA>().Object);
      chooser.Setup(c => c.GetConstructor(typeof (A))).Returns(GetLoneConstructor<A>());

      // Act
      object a0 = creator.Create(typeof (IA));
      object a1 = creator.Create(typeof (A));
      object a2 = creator.Create(typeof (IA));
      object a3 = creator.Create(typeof (A));

      // Assert
      a0.Should().BeSameAs(a2);
      a1.Should().BeSameAs(a3);
      handler.Verify(h => h.CreateMock(typeof (IA)), Times.Once());
      chooser.Verify(c => c.GetConstructor(typeof (A)), Times.Once());
    }

    [NukitoFact]
    internal void CreateMockedInterface(Creator creator, Mock<IMockHandler> handler)
    {
      // Arrange
      var a = new Mock<IA>().Object;
      handler.Setup(h => h.CreateMock(typeof (IA))).Returns(a);

      // Act
      object result = creator.Create(typeof (IA));

      // Assert
      result.Should().BeSameAs(a);
    }

    [NukitoFact]
    internal void CreateConcreteClass(Creator creator, Mock<IConstructorChooser> chooser)
    {
      // Arrange
      chooser.Setup(c => c.GetConstructor(typeof (A))).Returns(GetLoneConstructor<A>());

      // Act
      object result = creator.Create(typeof (A));

      // Assert
      result.Should().BeOfType<A>();
    }

    [NukitoFact]
    internal void CreateClassWithTransitiveDependencies(
      Creator creator, Mock<IMockHandler> handler, Mock<IConstructorChooser> chooser)
    {
      // Arrange
      chooser.Setup(c => c.GetConstructor(typeof (TransDep))).Returns(GetLoneConstructor<TransDep>());
      chooser.Setup(c => c.GetConstructor(typeof (DepOnClass))).Returns(GetLoneConstructor<DepOnClass>());
      chooser.Setup(c => c.GetConstructor(typeof (A))).Returns(GetLoneConstructor<A>());
      chooser.Setup(c => c.GetConstructor(typeof (DepOnInterface))).Returns(GetLoneConstructor<DepOnInterface>());
      handler.Setup(h => h.CreateMock(typeof (IA))).Returns(new Mock<IA>().Object);

      // Act
      object result = creator.Create(typeof (TransDep));

      // Assert
      var transDep = (TransDep) result;
      transDep.DepOnInterface.A.Should().BeMock<IA>();
      transDep.DepOnClass.A.Should().NotBeMock();
    }
  }
}