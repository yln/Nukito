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
  }

  public class DepOnInterface
  {
    public DepOnInterface(IA a)
    {
      A = a;
    }

    public IA A { get; private set; }
  }

  public class DepOnClass
  {
    public DepOnClass(A a)
    {
      A = a;
    }

    public A A { get; private set; }
  }
}