using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Assertions;
using Moq;
using Nukito.Internal;
using Nukito.Internal.ConstructorChooser;
using Nukito.Test.Scenario;

namespace Nukito.Test.Unit.ConstructorChooser
{
  public class ConstructorChooserCompositeTest
  {
    [NukitoFact]
    public void GetConstructorWithNoDelegates()
    {
      // Arrange
      var cc = new ConstructorChooserComposite( /* no delegates */);

      // Act
      Action execution = () => cc.GetConstructor(typeof (A));

      // Assert
      execution.ShouldThrow<NukitoException>()
        .WithMessage("Could not find an applicable constructor for type Nukito.Test.Scenario.A",
                     ComparisonMode.Substring);
    }

    [NukitoFact]
    public void GetConstructorShouldReturnFirstNonNull()
    {
      // Arrange
      var cc0 = new Mock<IConstructorChooser>();
      var cc1 = new Mock<IConstructorChooser>();
      var cc = new ConstructorChooserComposite(cc0.Object, cc1.Object);
      ConstructorInfo constructorInfo = typeof (A).GetConstructors().Single();
      cc0.Setup(x => x.GetConstructor(typeof (A))).Returns(constructorInfo);

      // Act
      ConstructorInfo result = cc.GetConstructor(typeof (A));

      // Assert
      result.Should().BeSameAs(constructorInfo);
      cc1.Verify(x => x.GetConstructor(typeof (A)), Times.Never());
    }

    [NukitoFact]
    public void CompositeDescription()
    {
      // Arrange 
      var cc0 = new Mock<IConstructorChooser>();
      var cc1 = new Mock<IConstructorChooser>();
      var cc = new ConstructorChooserComposite(cc0.Object, cc1.Object);
      cc0.Setup(x => x.StrategyDescription).Returns("AAA");
      cc1.Setup(x => x.StrategyDescription).Returns("BBB");

      // Act
      string result = cc.StrategyDescription;

      // Asssert
      result.Should().Contain("1) AAA").And.Contain("2) BBB");
    }
  }
}