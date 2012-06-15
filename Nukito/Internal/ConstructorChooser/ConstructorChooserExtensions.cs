using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nukito.Internal.ConstructorChooser
{
  public static class ConstructorChooserExtensions
  {
    public static TSource SingleOrDefaultForAny<TSource>(this IEnumerable<TSource> source)
    {
      using (IEnumerator<TSource> e = source.GetEnumerator())
      {
        if (!e.MoveNext()) return default(TSource);
        TSource result = e.Current;
        return !e.MoveNext() ? result : default(TSource);
      }
    }

    public static TSource SingleOrDefaultForAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      return source.Where(predicate).SingleOrDefaultForAny();
    }

    public static IEnumerable<ConstructorInfo> GetInternalConstructors(this Type type)
    {
      return type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(ci => ci.IsFamilyOrAssembly || ci.IsAssembly);
    }
  }
}