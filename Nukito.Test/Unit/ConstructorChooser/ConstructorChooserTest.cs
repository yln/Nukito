using Nukito.Test.Scenario;

namespace Nukito.Test.Unit
{
  public class ConstructorChooserTest
  {
    public class DefaultCtor
    {
    }

    public class SingleCtor
    {
      public SingleCtor(IA a)
      {
      }
    }

    public class MultipleCtorsDifferentArgumentCount
    {
      public MultipleCtorsDifferentArgumentCount(IA a)
      {
      }

      public MultipleCtorsDifferentArgumentCount(IA a, IB b)
      {
      }
    }

    public class MultipleCtorsSameArgumentCount
    {
      public MultipleCtorsSameArgumentCount(IA a)
      {
      }

      public MultipleCtorsSameArgumentCount(IB b)
      {
      }
    }

    public class MultipleCtorsSingleInjectAttribute
    {
      public MultipleCtorsSingleInjectAttribute()
      {
      }

      public MultipleCtorsSingleInjectAttribute(IA a)
      {
      }
    }
  }
}