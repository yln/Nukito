using System;

namespace Nukito
{
  public class NukitoException : Exception
  {
    internal NukitoException(String message)
      : base(message)
    {
    }

    internal NukitoException(String message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}