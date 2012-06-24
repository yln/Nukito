using System;

namespace Nukito
{
  [AttributeUsage (AttributeTargets.Parameter, AllowMultiple = false)]
  public class CtxAttribute : Attribute
  {
    public CtxAttribute (string name)
    {
      Name = name;
    }

    public string Name { get; private set; }
  }
}