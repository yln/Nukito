using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Moq;
using Nukito.Internal;
using Xunit.Sdk;

namespace Nukito.Test.Unit.Internal
{
  public class NukitoFactCommandTest
  {
    private readonly Mock<IMethodInfo> _method;
    private readonly Mock<IReflectionHelper> _reflectionHelper;
    private readonly ConstructorInfo _fakeConstructor;
    private readonly Type _ctorArgType;
    private readonly Mock<IResolver> _resolver;
    private readonly Mock<IMockRepository> _repository;
    private readonly MockSettings _settings;
    private readonly MockSettings _ctorSettings;

    private readonly NukitoFactCommand _command;

    public NukitoFactCommandTest (Mock<IMethodInfo> method, Mock<IReflectionHelper> reflectionHelper, Mock<IResolver> resolver, Mock<IMockRepository> repository)
    {
      _method = method;
      _fakeConstructor = typeof (string).GetConstructor (new[] { typeof (char[]) });
      _ctorArgType = _fakeConstructor.GetParameters().Single().ParameterType;
      _reflectionHelper = reflectionHelper;
      _resolver = resolver;
      _repository = repository;
      _settings = new MockSettings();
      _ctorSettings = new MockSettings();

      _command = new NukitoFactCommand (method.Object, _fakeConstructor, reflectionHelper.Object, resolver.Object, repository.Object, _settings, _ctorSettings);
    }

    [NukitoFact]
    public void ShouldCreateInstance ()
    {
      // Assert
      _command.ShouldCreateInstance.Should().Be(false);
    }

    [NukitoFact, MockSettings (Behavior = MockBehavior.Strict)]
    public void Execute (Type fakeType)
    {
      // Arrange 
      var fakeCtorArgument = new object();
      _resolver
          .Setup (x => x.Get (It.Is ((Request r) => r.Type == _ctorArgType && !r.ForceMockCreation && r.Settings == _ctorSettings)))
          .Returns(fakeCtorArgument);
      var fakeTestClass = new object();
      _reflectionHelper.Setup (x => x.InvokeConstructor (_fakeConstructor, new[] { fakeCtorArgument })).Returns (fakeTestClass);

      var fakeTestMethod = typeof (TestClass).GetMethod("TestMethod");
      _method.Setup(x => x.MethodInfo).Returns (fakeTestMethod);
      var fakeArgs = new[] { new object (), new object () };
      _resolver
          .Setup (x => x.Get (It.Is ((Request r) => r.Type == typeof (int) && !r.ForceMockCreation && r.Settings == _settings)))
          .Returns(fakeArgs[0]);
      _resolver
          .Setup (x => x.Get (It.Is ((Request r) => r.Type == typeof (string) && !r.ForceMockCreation && r.Settings == _settings)))
          .Returns (fakeArgs[1]);

      _method.Setup(x => x.Invoke(fakeTestClass, fakeArgs));
      _repository.Setup(x => x.VerifyMocks(_settings.Verification));

      _method.Setup (x => x.Name).Returns ("TestMethodName");
      
      // Act
      var result = _command.Execute(null);

      // Assert
      result.Should().BeOfType<PassedResult>();
      result.MethodName.Should().Be ("TestMethodName");
    }

    public class TestClass
    {
      public void TestMethod (int i, string s) { }
    }
  }
}