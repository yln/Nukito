﻿using System;
using System.Linq;
using System.Reflection;
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
      _verifier.VerifyMocks();

      return new PassedResult(testMethod, DisplayName);
    }
  }
}