using System;
using System.Linq;

namespace Nukito.Internal
{
  public class Resolver : IResolver
  {
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
      if (!request.Instances.TryGetValue (request.Type, out obj))
      {
        obj = Create (request);
        request.Instances.Add (request.Type, obj);
      }

      return obj;
    }

    private object Create (Request request)
    {
      if (!request.ForceMockCreation)
      {
        if (request.Type.IsClass && !request.Type.IsAbstract)
          return CreateClassInstance (request);

        if (request.Type.IsValueType)
          return Activator.CreateInstance (request.Type);
      }

      return _mockRepository.CreateMock (request.Type, request.Settings);
    }

    private object CreateClassInstance (Request request)
    {
      var constructor = _constructorChooser.GetConstructor (request.Type);
      var arguments = constructor.GetParameters ().Select (p => Get (request.CreateSubRequest (p.ParameterType))).ToArray ();

      return _reflectionHelper.InvokeConstructor (constructor, arguments);
    }
  }
}