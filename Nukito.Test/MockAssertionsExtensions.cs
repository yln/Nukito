using System.Diagnostics;
using FluentAssertions;
using FluentAssertions.Assertions;
using Moq;

namespace Nukito.Test
{
  [DebuggerNonUserCode]
  public static class MockAssertionsExtensions
  {
    public static AndConstraint<ObjectAssertions> BeMock(this ObjectAssertions objectAssertions)
    {
      return BeMock(objectAssertions, string.Empty, new object[0]);
    }

    public static AndConstraint<ObjectAssertions> BeMock(
      this ObjectAssertions objectAssertions, string reason, params object[] reasonArgs)
    {
      return CheckMock(objectAssertions, true, "mock", reason, reasonArgs);
    }

    public static AndConstraint<ObjectAssertions> NotBeMock(this ObjectAssertions objectAssertions)
    {
      return NotBeMock(objectAssertions, string.Empty, new object[0]);
    }

    public static AndConstraint<ObjectAssertions> NotBeMock(
      this ObjectAssertions objectAssertions, string reason, params object[] reasonArgs)
    {
      return CheckMock(objectAssertions, false, "real", reason, reasonArgs);
    }

    private static AndConstraint<ObjectAssertions> CheckMock(
      ObjectAssertions objectAssertions, bool shouldBeMock, string objectType, string reason, params object[] reasonArgs)
    {
      object subject = objectAssertions.Subject;

      Execute.Verification
        .ForCondition(subject is IMocked == shouldBeMock)
        .BecauseOf(reason, reasonArgs)
        .FailWith("Expected a " + objectType + " object{reason}, but found {0}", subject.GetType());

      return new AndConstraint<ObjectAssertions>(objectAssertions);
    }
  }
}