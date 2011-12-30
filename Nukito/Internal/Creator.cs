using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nukito.Internal
{
  internal class Creator : ICreator
  {
    private readonly IDictionary<Type, object> _instances = new Dictionary<Type, object>();
    private readonly IConstructorChooser _constructorChooser;
    private readonly IMockHandler _mockHandler;

    internal Creator(IConstructorChooser constructorChooser, IMockHandler mockHandler)
    {
      _constructorChooser = constructorChooser;
      _mockHandler = mockHandler;
    }

    public object Create(Type type)
    {
      object obj;
      if (!_instances.TryGetValue(type, out obj))
      {
        obj = CreateNew(type);
        _instances.Add(type, obj);
      }

      return obj;
    }

    private object CreateNew(Type type)
    {
      if (type.IsClass && !type.IsAbstract)
      {
        return CreateNewClass(type);
      }
      return _mockHandler.CreateMock(type);
    }

    private object CreateNewClass(Type type)
    {
      ConstructorInfo constructorInfo = _constructorChooser.GetConstructor(type);
      object[] parameters = constructorInfo.GetParameters().Select(p => Create(p.ParameterType)).ToArray();

      return constructorInfo.Invoke(parameters);
    }
  }
}