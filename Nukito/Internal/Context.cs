namespace Nukito.Internal
{
  public class Context
  {
    public Context(MockSettings settings)
    {
      Settings = settings;
    }

    public MockSettings Settings { get; private set; } 
  }
}