using System.Collections.Generic;
using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  public class InterferenceScenarios
  {
    private static readonly ISet<IA> s_interfaces = new HashSet<IA>();
    private static readonly ISet<A> s_classes = new HashSet<A>();
    private static readonly ISet<Mock<IA>> s_mocks = new HashSet<Mock<IA>>();

    private void TestsShouldBeIndependent(IA iface, A clazz, Mock<IA> mock)
    {
      // Assert
      s_interfaces.Should().NotContain(iface);
      s_classes.Should().NotContain(clazz);
      s_mocks.Should().NotContain(mock);

      // Add objects in static collection for next test
      s_interfaces.Add(iface);
      s_classes.Add(clazz);
      s_mocks.Add(mock);
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