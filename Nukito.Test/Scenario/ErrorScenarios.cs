using System;
using System.Reflection;
using FluentAssertions;
using Moq;
using Nukito.Test.Utility;
using Xunit.Sdk;

namespace Nukito.Test.Scenario
{
  public class ErrorScenarios
  {
    [NukitoFact]
    public void RequestInvalidMockType()
    {
      // Act + Assert
      GetTest<Mock>()
        .ShouldThrow<NukitoException>()
        .WithMessage("The generic version Mock<T> must be used in place of Mock.");
    }

    [NukitoFact]
    public void RequestMockForSealedClass()
    {
      // Act + Assert
      GetTest<Mock<SealedClass>> ()
        .ShouldThrow<NukitoException>()
        .WithMessage ("Type to mock must be an interface or an abstract or non-sealed class. ");
    }

    [NukitoFact]
    public void ShouldThrowIfNoCtorIsFound ()
    {
      // Arrange
      var nukitoAttribute = new NukitoFactAttribute();
      var testMethod = Reflector.Wrap (typeof (ClassWithoutSinglePublicCtor).GetMethod ("TestMethod"));

      // Act + Assert
      this.Invoking(x => CallEnumerateTestCommands(nukitoAttribute, testMethod))
        .ShouldThrow<NukitoException> ()
        .WithMessage ("Test class must have a single public constructor");
    }

    private Action GetTest<T> ()
    {
      return TestUtility.GetTest<T> (RequestInvalidParameter);
    }

    private void RequestInvalidParameter<T> (T invalidParameter)
    {
      // Do nothing
    }

    private object CallEnumerateTestCommands(NukitoFactAttribute factAttribute, IMethodInfo method)
    {
      var protectedMethod = typeof (NukitoFactAttribute).GetMethod ("EnumerateTestCommands", BindingFlags.NonPublic | BindingFlags.Instance);
      try
      {
        return protectedMethod.Invoke(factAttribute, new object[] {method});
      }
      catch(TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }

    public class ClassWithoutSinglePublicCtor
    {
      internal ClassWithoutSinglePublicCtor () { }

      public void TestMethod() {}
    }
  }
}