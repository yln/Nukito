using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Xunit.Sdk;

namespace Nukito.Internal
{
  public class NukitoFactCommand : FactCommand
  {
    private readonly ConstructorInfo _constructor;
    private readonly IReflectionHelper _reflectionHelper;
    private readonly IResolver _resolver;
    private readonly IMockRepository _mockRepository;
    private readonly MockSettings _settings;
    private readonly MockSettings _constructorSettings;

    public NukitoFactCommand (
        IMethodInfo method,
        ConstructorInfo constructor,
        IReflectionHelper reflectionHelper,
        IResolver resolver,
        IMockRepository mockRepository,
        MockSettings settings,
        MockSettings constructorSettings)
      : base(method)
    {
      _constructor = constructor;
      _reflectionHelper = reflectionHelper;
      _resolver = resolver;
      _mockRepository = mockRepository;
      _settings = settings;
      _constructorSettings = constructorSettings;
    }

    public override bool ShouldCreateInstance
    {
      get { return false; }
    }

    public override MethodResult Execute (object testClass)
    {
      Debug.Assert (testClass == null);

      var instances = new Dictionary<Type, object>();

      if (base.ShouldCreateInstance)
      {
        var ctorArguments = CreateArguments (_constructor, _constructorSettings, instances);
        testClass = _reflectionHelper.InvokeConstructor (_constructor, ctorArguments);
      }

      var arguments = CreateArguments (testMethod.MethodInfo, _settings, instances);
      testMethod.Invoke (testClass, arguments);
      _mockRepository.VerifyMocks (_settings.Verification);

      return new PassedResult (testMethod, DisplayName);
    }

    private object[] CreateArguments (MethodBase methodBase, MockSettings settings, Dictionary<Type, object> instances)
    {
      var parameters = methodBase.GetParameters();
      var arguments = new object[parameters.Length];

      for (int i = 0; i < parameters.Length; i++)
      {
        var request = new Request (parameters[i].ParameterType, false, settings, instances);
        arguments[i] = _resolver.Get (request);
      }

      return arguments;
    }
  }
}