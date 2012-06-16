using System;
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
    private readonly ConstructorInfo _fakeConstructor;
    private readonly Mock<IResolver> _resolver;
    private readonly Mock<IMockRepository> _repository;
    private readonly MockSettings _settings;
    private readonly MockSettings _ctorSettings;

    private readonly NukitoFactCommand _command;

    public NukitoFactCommandTest (Mock<IMethodInfo> method, ConstructorInfo fakeConstructor, Mock<IResolver> resolver, Mock<IMockRepository> repository)
    {
      _method = method;
      _fakeConstructor = fakeConstructor;
      _resolver = resolver;
      _repository = repository;
      _settings = new MockSettings();
      _ctorSettings = new MockSettings();

      _command = new NukitoFactCommand (method.Object, fakeConstructor, resolver.Object, repository.Object, _settings, _ctorSettings);
    }

    [NukitoFact]
    public void ShouldCreateInstance ()
    {
      // Assert
      _command.ShouldCreateInstance.Should().Be(false);
    }

    // TODO: update BasicExample in Readme
    // TODO: update SettingsExample (show that settings can be aplied to ctor aswell)
    [NukitoFact(Skip = "TODO")]
    public void Execute (Type fakeType)
    {
      // Arrange
      _method.Setup (x => x.Name).Returns ("TestMethodName");
      _method.Setup (x => x.Class.Type).Returns (fakeType);
      _resolver.Setup (x => x.Get (It.Is((Request r) => CheckRequest(r, fakeType, false, _ctorSettings))));

      _method.Setup(x => x.MethodInfo).Returns(typeof (TestClass).GetMethod("TestMethod"));
      var fakeArgs = new[] { new object(), new object() };
      //_resolver.Setup(x => x.Get(typeof (int), It.Is((Context c) => c.Settings == _settings))).Returns(fakeArgs[0]); // TODO default Settings
      //_resolver.Setup(x => x.Get (typeof (string), It.Is ((Context c) => c.Settings == _settings))).Returns (fakeArgs[1]); // TODO default settings
      
      // Act
      var result = _command.Execute(null);

      // Assert
      _repository.Verify (x => x.VerifyMocks(MockVerification.All));
      Func<object[], bool> argChecker = args => { args.Should().Equal (fakeArgs); return true; };
      _method.Verify (x => x.Invoke (It.IsAny<TestClass> (), It.Is ((object[] args) => argChecker(args))));

      result.Should().BeOfType<PassedResult>();
      result.MethodName.Should().Be ("TestMethodName");
    }

    private static bool CheckRequest(Request request, Type expectedType, bool expectedForceMockCreation, MockSettings expectedSettings)
    {
      request.Type.Should ().Be (expectedType);
      request.ForceMockCreation.Should ().Be (expectedForceMockCreation);
      request.Context.Settings.Should ().Be (expectedSettings);

      return true;
    }

    public class TestClass
    {
      public void TestMethod (int i, string s) { }
    }
  }
}