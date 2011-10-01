using System.Diagnostics;
using FluentAssertions;
using FluentAssertions.Assertions;
using Moq;

namespace Nukito.Test.Utility
{
  [DebuggerNonUserCode]
  public static class MockAssertionsExtensions
  {
    public static AndConstraint<ObjectAssertions> BeMock<T>(this ObjectAssertions objectAssertions)
      where T : class
    {
      return BeMock<T>(objectAssertions, string.Empty, new object[0]);
    }

    public static AndConstraint<ObjectAssertions> BeMock<T>(
      this ObjectAssertions objectAssertions, string reason, params object[] reasonArgs)
      where T : class
    {
      return CheckMock<IMocked<T>>(objectAssertions, true, "mock", reason, reasonArgs);
    }

    public static AndConstraint<ObjectAssertions> NotBeMock(this ObjectAssertions objectAssertions)
    {
      return NotBeMock(objectAssertions, string.Empty, new object[0]);
    }

    public static AndConstraint<ObjectAssertions> NotBeMock(
      this ObjectAssertions objectAssertions, string reason, params object[] reasonArgs)
    {
      return CheckMock<IMocked>(objectAssertions, false, "real", reason, reasonArgs);
    }

    private static AndConstraint<ObjectAssertions> CheckMock<T>(
      ObjectAssertions objectAssertions, bool shouldBeMock, string objectType, string reason, params object[] reasonArgs)
      where T : IMocked
    {
      object subject = objectAssertions.Subject;

      Execute.Verification
        .ForCondition(subject is T == shouldBeMock)
        .BecauseOf(reason, reasonArgs)
        .FailWith("Expected a " + objectType + " object{reason}, but found {0}", subject.GetType());

      return new AndConstraint<ObjectAssertions>(objectAssertions);
    }
  }
}