using System;
using Nukito.Test.Scenario;

namespace Nukito.Test.Unit.Internal.ConstructorChooser
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
    public MultipleCtorsSingleInjectAttribute(IA a)
    {
    }

    [Inject]
    public MultipleCtorsSingleInjectAttribute(IB b)
    {
    }
  }

  public class InjectAttribute : Attribute
  {
  }
}