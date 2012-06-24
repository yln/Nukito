using System;

namespace Nukito
{
  public class CtxAttribute : Attribute
  {
    public CtxAttribute (string name)
    {
      Name = name;
    }

    public string Name { get; private set; }
  }
}