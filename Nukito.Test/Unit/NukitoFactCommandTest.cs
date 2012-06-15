using System;
using FluentAssertions;
using Moq;
using Nukito.Internal;
using Xunit.Sdk;

namespace Nukito.Test.Unit
{
  public class NukitoFactCommandTest
  {
    private readonly NukitoFactCommand _command;
    private readonly Mock<IMethodInfo> _method;
    private readonly Mock<IResolver> _resolver;
    private readonly Mock<IVerifier> _verifier;

    public NukitoFactCommandTest (NukitoFactCommand command, Mock<IMethodInfo> method, Mock<IResolver> resolver, Mock<IVerifier> verifier)
    {
      _command = command;
      _method = method;
      _resolver = resolver;
      _verifier = verifier;
    }

    [NukitoFact]
    public void ShouldCreateInstance ()
    {
      // Assert
      _command.ShouldCreateInstance.Should().Be(false);
    }

    [NukitoFact]
    public void Execute ()
    {
      // Arrange
      _method.Setup(x => x.Name).Returns("TestMethodName");
      _method.Setup(x => x.Class.Type).Returns(typeof(TestClass));
      _method.Setup(x => x.MethodInfo).Returns(typeof (TestClass).GetMethod("TestMethod"));
      var fakeArgs = new[] { new object(), new object() };
      _resolver.Setup(x => x.Get(typeof (int))).Returns(fakeArgs[0]);
      _resolver.Setup(x => x.Get(typeof (string))).Returns(fakeArgs[1]);
      
      // Act
      var result = _command.Execute(null);

      // Assert
      _verifier.Verify(x => x.VerifyMocks());
      Func<object[], bool> argChecker = args => { args.Should().Equal(fakeArgs); return true; };
      _method.Verify (x => x.Invoke (It.IsAny<TestClass> (), It.Is((object[] args) => argChecker(args))));

      result.Should().BeOfType<PassedResult>();
      result.MethodName.Should().Be ("TestMethodName");
    }

    [NukitoFact]
    public void ExecuteShouldThrowIfNoCtorIsFound()
    {
      // Arrange
      _method.Setup(x => x.Class.Type).Returns(typeof (ClassWithoutSinglePublicCtor));
      
      // Act + Assert
      _command.Invoking(x => x.Execute(null))
        .ShouldThrow<NukitoException>()
        .WithMessage("Test class must have a single public constructor");
    }

    public class TestClass
    {
      public void TestMethod (int i, string s) { }
    }

    public class ClassWithoutSinglePublicCtor
    {
      internal ClassWithoutSinglePublicCtor() {}
    }
  }
}