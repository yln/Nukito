using System.Diagnostics;
using System.Reflection;
using Xunit.Sdk;
using System.Linq;

namespace Nukito.Internal
{
  public class NukitoFactCommand : FactCommand
  {
    private readonly ConstructorInfo _constructor;
    private readonly IRequestProvider _requestProvider;
    private readonly IReflectionHelper _reflectionHelper;
    private readonly IResolver _resolver;
    private readonly IMockRepository _mockRepository;
    private readonly MockSettings _settings;
    private readonly MockSettings _constructorSettings;

    public NukitoFactCommand (
        IMethodInfo method,
        ConstructorInfo constructor,
        IRequestProvider requestProvider,
        IResolver resolver,
        IReflectionHelper reflectionHelper,
        IMockRepository mockRepository,
        MockSettings settings,
        MockSettings constructorSettings)
      : base(method)
    {
      _constructor = constructor;
      _requestProvider = requestProvider;
      _resolver = resolver;
      _reflectionHelper = reflectionHelper;
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

      if (base.ShouldCreateInstance)
      {
        var ctorArguments = CreateArguments (_constructor, _constructorSettings);
        testClass = _reflectionHelper.InvokeConstructor (_constructor, ctorArguments);
      }

      var arguments = CreateArguments (testMethod.MethodInfo, _settings);
      testMethod.Invoke (testClass, arguments);
      _mockRepository.VerifyMocks (_settings.Verification);

      return new PassedResult (testMethod, DisplayName);
    }

    private object[] CreateArguments (MethodBase methodBase, MockSettings settings)
    {
      return methodBase.GetParameters()
          .Select (p => _resolver.Get (_requestProvider.GetRequest (GetContextName (p), p.ParameterType, settings)))
          .ToArray(); 
    }

    private string GetContextName (ParameterInfo parameter)
    {
      var ctxAttribute = (CtxAttribute) parameter.GetCustomAttributes (typeof (CtxAttribute), false).SingleOrDefault();
      return ctxAttribute != null ? ctxAttribute.Name : "<default>";
    }
  }
}