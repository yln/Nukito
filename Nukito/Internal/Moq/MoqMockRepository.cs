using System;
using System.Reflection;
using Moq;

namespace Nukito.Internal.Moq
{
  public class MoqMockRepository : IMockRepository
  {
    private static readonly MethodInfo s_createMethod = typeof (MockRepository).GetMethod ("Create", new[] { typeof (MockBehavior) });

    private readonly MockRepository _repository = new MockRepository (MockBehavior.Default);

    public object WrappedRepository
    {
      get { return _repository; }
    }

    public bool IsMockRepositoryType(Type type)
    {
      return typeof (MockRepository).IsAssignableFrom (type);
    }

    // TODO: Consider creating delegate to speedup reflection.
    public object CreateMock (Type serviceType, MockSettings settings)
    {
      var method = s_createMethod.MakeGenericMethod (serviceType);
      try
      {
        var mock = (Mock) method.Invoke (_repository, new object[] { settings.Behavior });
        mock.DefaultValue = settings.DefaultValue;
        mock.CallBase = settings.CallBase;

        return mock.Object;
      }
      catch (TargetInvocationException ex)
      {
        throw new NukitoException(ex.InnerException.Message, ex.InnerException);
      }
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