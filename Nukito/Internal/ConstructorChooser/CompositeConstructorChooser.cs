using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nukito.Internal.ConstructorChooser
{
  internal class CompositeConstructorChooser : IConstructorChooser
  {
    private readonly IConstructorChooser[] _constructorChoosers;

    internal CompositeConstructorChooser(params IConstructorChooser[] constructorChoosers)
    {
      _constructorChoosers = constructorChoosers;
    }

    public ConstructorInfo GetConstructor(Type type)
    {
      ConstructorInfo constructorInfo = _constructorChoosers
        .Select(cc => cc.GetConstructor(type)).FirstOrDefault(ci => ci != null);
      if (constructorInfo != null)
      {
        return constructorInfo;
      }

      throw new NukitoException(BuildExceptionMessage(type));
    }

    private string BuildExceptionMessage(Type type)
    {
      return string.Format("Could not find an applicable constructor for type {0}{1}The following was tried:{1}{2}",
                           type.FullName, Environment.NewLine, StrategyDescription);
    }

    public string StrategyDescription
    {
      get
      {
        var sb = new StringBuilder();
        int i = 0;

        foreach (IConstructorChooser cc in _constructorChoosers)
        {
          sb.AppendFormat("  {0}) {1}{2}", ++i, cc.StrategyDescription, Environment.NewLine);
        }

        return sb.ToString();
      }
    }
  }
}