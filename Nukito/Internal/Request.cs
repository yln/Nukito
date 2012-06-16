using System;

namespace Nukito.Internal
{
  public class Request
  {
    public Request (Type type, bool forceMockCreation, Context context)
    {
      Type = type;
      ForceMockCreation = forceMockCreation;
      Context = context;
    }

    public Type Type { get; private set; }
    public bool ForceMockCreation { get; private set; }
    public Context Context { get; private set; }
  }
}