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
    private readonly Mock<IRequestProvider> _requestProvider;
    private readonly ConstructorInfo _fakeConstructor;
    private readonly Type _ctorArgType;
    private readonly Mock<IResolver> _resolver;
    private readonly Mock<IReflectionHelper> _reflectionHelper;
    private readonly Mock<IMockRepository> _repository;
    private readonly MockSettings _settings;
    private readonly MockSettings _ctorSettings;

    private readonly NukitoFactCommand _command;

    public NukitoFactCommandTest (Mock<IMethodInfo> method, Mock<IRequestProvider> requestProvider, Mock<IResolver> resolver, Mock<IReflectionHelper> reflectionHelper, Mock<IMockRepository> repository)
    {
      _method = method;
      _requestProvider = requestProvider;
      _fakeConstructor = typeof (string).GetConstructor (new[] { typeof (char[]) });
      _ctorArgType = _fakeConstructor.GetParameters().Single().ParameterType;
      _resolver = resolver;
      _reflectionHelper = reflectionHelper;
      _repository = repository;
      _settings = new MockSettings();
      _ctorSettings = new MockSettings();

      _command = new NukitoFactCommand (method.Object, _fakeConstructor, requestProvider.Object, resolver.Object, reflectionHelper.Object, repository.Object, _settings, _ctorSettings);
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
      var fakeCtorArgRequest = CreateRequest();
      _requestProvider.Setup (x => x.GetRequest("<default>", _ctorArgType, _ctorSettings)).Returns (fakeCtorArgRequest);
      var fakeCtorArg = new object();
      _resolver.Setup (x => x.Get (fakeCtorArgRequest)).Returns (fakeCtorArg);
      var fakeTestClass = new object();
      _reflectionHelper.Setup (x => x.InvokeConstructor (_fakeConstructor, new[] { fakeCtorArg })).Returns (fakeTestClass);

      var fakeTestMethod = typeof (TestClass).GetMethod("TestMethod");
      _method.Setup(x => x.MethodInfo).Returns (fakeTestMethod);
      var fakeArgRequests = new[] { CreateRequest(), CreateRequest() };
      _requestProvider.Setup (x => x.GetRequest ("<default>", typeof (int), _settings)).Returns (fakeArgRequests[0]);
      _requestProvider.Setup (x => x.GetRequest ("<default>", typeof (string), _settings)).Returns (fakeArgRequests[1]);
      var fakeArgs = new[] { new object (), new object () };
      _resolver.Setup (x => x.Get (fakeArgRequests[0])).Returns (fakeArgs[0]);
      _resolver.Setup (x => x.Get (fakeArgRequests[1])).Returns (fakeArgs[1]);

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

    private Request CreateRequest()
    {
      return new Request (typeof (object), false, new MockSettings (), null);
    }
  }
}