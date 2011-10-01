namespace Nukito.Test.Scenario
{
  public interface IA
  {
  }

  public class A : IA
  {
  }

  public interface IB
  {
    void DoSomething();
    void DoSomethingElse();
  }

  public class DepOnInterface
  {
    public IA A { get; private set; }

    public DepOnInterface(IA a)
    {
      A = a;
    }
  }

  public class DepOnClass
  {
    public A A { get; private set; }

    public DepOnClass(A a)
    {
      A = a;
    }
  }

  public class TransDep
  {
    public DepOnInterface DepOnInterface { get; set; }
    public DepOnClass DepOnClass { get; set; }

    public TransDep(DepOnInterface depOnInterface, DepOnClass depOnClass)
    {
      DepOnInterface = depOnInterface;
      DepOnClass = depOnClass;
    }
  }
}