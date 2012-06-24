using System;
using System.Linq;
using Moq;

namespace Nukito.Internal.Moq
{
  public class MoqResolver : IResolver
  {
    private readonly IResolver _resolver;

    public MoqResolver (IResolver resolver)
    {
      _resolver = resolver;
    }

    public object Get (Request request)
    {
      var serviceType = GetServiceType (request.Type);
      var isConfigRequested = serviceType != request.Type;
      if (isConfigRequested)
        request = new Request (serviceType, true, request.Settings);

      var obj = _resolver.Get (request);
      return isConfigRequested ? ((IMocked) obj).Mock : obj;
    }

    private Type GetServiceType (Type type)
    {
      if (type == typeof (Mock))
        throw new NukitoException ("The generic version Mock<T> must be used in place of Mock.");

      if (type.IsGenericType && type.GetGenericTypeDefinition () == typeof (Mock<>))
        return type.GetGenericArguments().Single();

      return type;
    }
  }
}