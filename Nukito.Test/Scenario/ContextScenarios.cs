using FluentAssertions;

namespace Nukito.Test.Scenario
{
  public class ContextScenarios
  {
    private readonly object _a;

    public ContextScenarios (object a)
    {
      _a = a;
    }

    [NukitoFact]
    public void Default (object a, object b)
    {
      // Assert
      a.Should().BeSameAs (b);
      a.Should().BeSameAs (_a);
    }

    [NukitoFact]
    public void Same ([Ctx ("a")] object a, [Ctx ("a")] object b)
    {
      // Assert
      a.Should ().BeSameAs (b);
    }

    [NukitoFact (Skip = "TODO")]
    public void Different ([Ctx ("a")] object a, [Ctx ("b")] object b)
    {
      // Assert
      a.Should ().NotBeSameAs (b);
    }

    [NukitoFact (Skip = "TODO")]
    public void DifferentFromDefault (object a, [Ctx ("b")] object b)
    {
      // Assert
      a.Should ().NotBeSameAs (b);
    }

    public class ConstructorContextScenario
    {
      private readonly object _a;

      public ConstructorContextScenario ([Ctx("ctor")] object a)
      {
        _a = a;
      }

      [NukitoFact(Skip = "TODO")]
      public void Default (object a)
      {
        // Assert
        a.Should().NotBeSameAs (_a);
      }

      [NukitoFact]
      public void SameAsConstructor ([Ctx("ctor")] object a)
      {
        // Assert
        a.Should().BeSameAs (_a);
      }
    }
  }
}