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

    public object Get (Request request)
    {
      if (_mockRepository.IsMockRepositoryType (request.Type))
        return _mockRepository.WrappedRepository;

      object obj;
      if (!_instances.TryGetValue (request.Type, out obj))
      {
        obj = Create (request);
        _instances.Add (request.Type, obj);
      }

      return obj;
    }

    private object Create (Request request)
    {
      if (!request.ForceMockCreation)
      {
        if (request.Type.IsClass && !request.Type.IsAbstract) // TODO only if a ctor is available
          return CreateClassInstance (request);

        if (request.Type.IsValueType)
          return Activator.CreateInstance (request.Type);
      }

      return _mockRepository.CreateMock (request.Type, request.Settings);
    }

    private object CreateClassInstance (Request request)
    {
      var constructor = _constructorChooser.GetConstructor (request.Type);
      var arguments = constructor.GetParameters ().Select (p => Get (new Request (p.ParameterType, false, request.Settings))).ToArray ();

      return _reflectionHelper.InvokeConstructor (constructor, arguments);
    }
  }
}