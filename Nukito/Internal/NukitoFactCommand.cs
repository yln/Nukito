using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Nukito.Internal.ConstructorChooser;
using Xunit.Sdk;

namespace Nukito.Internal
{
  internal class NukitoFactCommand : FactCommand
  {
    private readonly IResolver _resolver;
    private readonly IVerifier _verifier;

    public NukitoFactCommand(IMethodInfo method, IResolver resolver, IVerifier verifier)
      : base(method)
    {
      _resolver = resolver;
      _verifier = verifier;
    }

    public override bool ShouldCreateInstance
    {
      get { return false; }
    }

    public override MethodResult Execute(object testClass)
    {
      Debug.Assert(testClass == null);
      if (base.ShouldCreateInstance)
        testClass = CreateInstance(testMethod.Class.Type);

      var arguments = CreateArguments(testMethod.MethodInfo);
      testMethod.Invoke(testClass, arguments);
      _verifier.VerifyMocks();

      return new PassedResult(testMethod, DisplayName);
    }

    private object CreateInstance(Type type)
    {
      var constructors = type.GetConstructors();
      if (constructors.Length != 1)
        throw new NukitoException("Test class must have a single public constructor");

      var arguments = CreateArguments(constructors.Single());
      return Activator.CreateInstance(type, arguments);
    }

    private object[] CreateArguments (MethodBase methodBase)
    {
      var parameters = methodBase.GetParameters();
      var arguments = new object[parameters.Length];

      for (int i = 0; i < parameters.Length; i++)
        arguments[i] = _resolver.Get(parameters[i].ParameterType);

      return arguments;
    }
  }
}