using System.Collections.Generic;
using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  public class InterferenceScenarios
  {
    private static readonly ISet<IA> _Interfaces = new HashSet<IA>();
    private static readonly ISet<A> _Classes = new HashSet<A>();
    private static readonly ISet<Mock<IA>> _Mocks = new HashSet<Mock<IA>>();

    private void TestsShouldBeIndependent(IA iface, A clazz, Mock<IA> mock)
    {
      // Assert
      _Interfaces.Should().NotContain(iface);
      _Classes.Should().NotContain(clazz);
      _Mocks.Should().NotContain(mock);

      // Add objects in static collection for next test
      _Interfaces.Add(iface);
      _Classes.Add(clazz);
      _Mocks.Add(mock);
    }

    [NukitoFact]
    public void TestsAreIndependent1(IA iface, A clazz, Mock<IA> mock)
    {
      TestsShouldBeIndependent(iface, clazz, mock);
    }

    [NukitoFact]
    public void TestsAreIndependent2(IA iface, A clazz, Mock<IA> mock)
    {
      TestsShouldBeIndependent(iface, clazz, mock);
    }
  }
}