using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Assertions;
using Moq;
using Nukito.Internal;
using Nukito.Internal.ConstructorChooser;
using Nukito.Test.Scenario;

namespace Nukito.Test.Unit.Internal.ConstructorChooser
{
  public class ConstructorChooserCompositeTest
  {
    [NukitoFact]
    public void GetConstructorWithNoDelegates()
    {
      // Arrange
      var cc = new CompositeConstructorChooser(/* no delegates */);

      // Act
      Action execution = () => cc.GetConstructor(typeof (A));

      // Assert
      execution.ShouldThrow<NukitoException>()
        .WithMessage("Could not find an applicable constructor for type Nukito.Test.Scenario.A",
                     ComparisonMode.Substring);
    }

    [NukitoFact]
    public void GetConstructorShouldReturnFirstNonNull (Mock<IConstructorChooser> cc1, [Ctx ("other")] Mock<IConstructorChooser> cc2)
    {
      // Arrange
      var cc = new CompositeConstructorChooser (cc1.Object, cc2.Object);
      ConstructorInfo constructorInfo = typeof (A).GetConstructors().Single();
      cc1.Setup(x => x.GetConstructor(typeof (A))).Returns(constructorInfo);

      // Act
      ConstructorInfo result = cc.GetConstructor(typeof (A));

      // Assert
      result.Should().BeSameAs(constructorInfo);
      cc2.Verify(x => x.GetConstructor(typeof (A)), Times.Never());
    }

    [NukitoFact]
    public void CompositeDescription (Mock<IConstructorChooser> cc1, [Ctx ("other")] Mock<IConstructorChooser> cc2)
    {
      // Arrange
      var cc = new CompositeConstructorChooser (cc1.Object, cc2.Object);
      cc1.Setup(x => x.StrategyDescription).Returns("AAA");
      cc2.Setup(x => x.StrategyDescription).Returns("BBB");

      // Act
      string result = cc.StrategyDescription;

      // Asssert
      result.Should().Contain("1) AAA").And.Contain("2) BBB");
    }
  }
}