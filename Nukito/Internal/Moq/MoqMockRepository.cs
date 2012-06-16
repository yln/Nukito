using System;
using System.Reflection;
using Moq;

namespace Nukito.Internal.Moq
{
  public class MoqMockRepository : IMockRepository
  {
    private static readonly MethodInfo s_createMethod = typeof (MockRepository).GetMethod ("Create", new[] { typeof (MockBehavior) });

    private readonly MockRepository _repository = new MockRepository (MockBehavior.Default);
    private readonly IReflectionHelper _reflectionHelper;

    public MoqMockRepository(IReflectionHelper reflectionHelper)
    {
      _reflectionHelper = reflectionHelper;
    }

    public object WrappedRepository
    {
      get { return _repository; }
    }

    public bool IsMockRepositoryType(Type type)
    {
      return typeof (MockRepository).IsAssignableFrom (type);
    }

    public object CreateMock (Type serviceType, MockSettings settings)
    {
      var mock = (Mock) _reflectionHelper.InvokeGenericMethod (s_createMethod, new[] { serviceType }, _repository, new object[] { settings.Behavior });
      mock.DefaultValue = settings.DefaultValue;
      mock.CallBase = settings.CallBase;

      return mock.Object;
    }

    public void VerifyMocks (MockVerification mockVerification)
    {
      switch (mockVerification)
      {
        case MockVerification.All:
          _repository.VerifyAll();
          break;
        case MockVerification.Marked:
          _repository.Verify();
          break;
        case MockVerification.None:
          // Do Nothing
          break;
      }
    }
  }
}