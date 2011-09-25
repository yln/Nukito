using System;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Nukito.Internal
{
  internal class NukitoFactCommand : FactCommand
  {
    private readonly IResolver _resolver;

    public NukitoFactCommand(IMethodInfo method, IResolver resolver)
      : base(method)
    {
      _resolver = resolver;
    }

    public override MethodResult Execute(object testClass)
    {
      ParameterInfo[] parameterInfos = testMethod.MethodInfo.GetParameters();
      var parameters = new object[parameterInfos.Length];

      int i = 0;
      foreach (Type parameterType in parameterInfos.Select(p => p.ParameterType))
      {
        parameters[i++] = _resolver.Get(parameterType);
      }
      testMethod.Invoke(testClass, parameters);

      return new PassedResult(testMethod, DisplayName);
    }
  }
}