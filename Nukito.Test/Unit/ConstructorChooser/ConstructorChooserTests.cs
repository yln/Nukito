using FluentAssertions;
using Nukito.Internal.ConstructorChooser;

namespace Nukito.Test.Unit.ConstructorChooser
{
  public class ConstructorChooserTests
  {
    [NukitoFact]
    internal void Test0(SinglePublicConstructorChooser cc)
    {
      // Act + Assert
      cc.GetConstructor(typeof (DefaultCtor)).Should().NotBeNull();
      cc.GetConstructor(typeof (SingleCtor)).Should().NotBeNull();
      cc.GetConstructor(typeof (MultipleCtorsDifferentArgumentCount)).Should().BeNull();
      cc.GetConstructor(typeof (MultipleCtorsSameArgumentCount)).Should().BeNull();
      cc.GetConstructor(typeof (MultipleCtorsSingleInjectAttribute)).Should().BeNull();
    }

    [NukitoFact]
    internal void Test1(SingleCtorWithInjectAttributeConstructorChooser cc)
    {
      // Act + Assert
      cc.GetConstructor(typeof (DefaultCtor)).Should().BeNull();
      cc.GetConstructor(typeof (SingleCtor)).Should().BeNull();
      cc.GetConstructor(typeof (MultipleCtorsDifferentArgumentCount)).Should().BeNull();
      cc.GetConstructor(typeof (MultipleCtorsSameArgumentCount)).Should().BeNull();
      cc.GetConstructor(typeof (MultipleCtorsSingleInjectAttribute)).Should().NotBeNull();
    }

    [NukitoFact]
    internal void Test2(MaxArgumentsPublicConstructorChooser cc)
    {
      // Act + Assert
      cc.GetConstructor(typeof (DefaultCtor)).Should().NotBeNull();
      cc.GetConstructor(typeof (SingleCtor)).Should().NotBeNull();
      cc.GetConstructor(typeof (MultipleCtorsDifferentArgumentCount)).Should().NotBeNull();
      cc.GetConstructor(typeof (MultipleCtorsSameArgumentCount)).Should().BeNull();
      cc.GetConstructor(typeof (MultipleCtorsSingleInjectAttribute)).Should().BeNull();
    }
  }
}