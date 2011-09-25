using System;

namespace Nukito
{
  public class NukitoException : Exception
  {
    public NukitoException(String message)
      : base(message)
    {
    }

    public NukitoException(String message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}