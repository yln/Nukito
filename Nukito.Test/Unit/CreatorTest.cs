using FluentAssertions;
using Nukito.Internal;
using Nukito.Test.Scenario;
using Nukito.Test.Utility;

namespace Nukito.Test.Unit
{
  public class CreatorTest
  {
    [NukitoFact]
    public void CreateShouldReturnSingletons(Creator creator)
    {
      // Act
      object a0 = creator.Create(typeof (IA));
      object a1 = creator.Create(typeof (A));
      object a2 = creator.Create(typeof (IA));
      object a3 = creator.Create(typeof (A));

      // Assert
      a0.Should().BeSameAs(a2);
      a1.Should().BeSameAs(a3);
    }

    [NukitoFact]
    public void CreateMockedInterface(Creator creator)
    {
      // Act
      object a = creator.Create(typeof (IA));

      // Assert
      a.Should().BeMock<IA>();
    }

    [NukitoFact]
    public void CreateConcreteClass(Creator creator)
    {
      // Act
      object a = creator.Create(typeof (A));

      // Assert
      a.Should().BeOfType<A>();
    }

    [NukitoFact]
    public void CreateClassWithTransitiveDependencies(Creator creator)
    {
      // Act
      object result = creator.Create(typeof (TransDep));

      // Assert
      var transDep = (TransDep) result;
      transDep.DepOnInterface.A.Should().BeMock<IA>();
      transDep.DepOnClass.A.Should().NotBeMock();
    }
  }
}