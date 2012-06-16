using System;
using System.Collections.Generic;
using System.Linq;

namespace Nukito.Internal
{
  public class Resolver : IResolver
  {
    private readonly IDictionary<Type, object> _instances = new Dictionary<Type, object>();
    private readonly IMockRepository _mockRepository;
    private readonly IConstructorChooser _constructorChooser;
    private readonly IReflectionHelper _reflectionHelper;

    public Resolver (IMockRepository mockRepository, IConstructorChooser constructorChooser, IReflectionHelper reflectionHelper)
    {
      _constructorChooser = constructorChooser;
      _reflectionHelper = reflectionHelper;
      _mockRepository = mockRepository;
    }

    public object Get (Type serviceType, Context context)
    {
      return Get (new Request(serviceType, false, context));
    }

    public object Get (Request request)
    {
      var serviceType = request.Type;

      if (_mockRepository.IsMockRepositoryType (serviceType))
        return _mockRepository.WrappedRepository;

      object obj;
      if (!_instances.TryGetValue (serviceType, out obj))
      {
        obj = Create (serviceType, request);
        _instances.Add (serviceType, obj);
      }

      return obj;
    }

    private object Create (Type serviceType, Request request)
    {
      if (!request.ForceMockCreation)
      {
        if (serviceType.IsClass && !serviceType.IsAbstract) // TODO only if a ctor is available
          return CreateInstance (serviceType, request.Context);

        if (serviceType.IsValueType)
          return Activator.CreateInstance (serviceType);
      }

      return _mockRepository.CreateMock (serviceType, request.Context.Settings);
    }

    private object CreateInstance (Type classType, Context context)
    {
      var constructor = _constructorChooser.GetConstructor (classType);
      var arguments = constructor.GetParameters ().Select (p => Get (p.ParameterType, context)).ToArray ();

      return _reflectionHelper.InvokeConstructor(constructor, arguments);
    }
  }
}