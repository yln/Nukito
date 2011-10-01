using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Nukito.Internal;

namespace Nukito.Test.Unit
{
  public class NukitoSettingsTest
  {
    [NukitoFact]
    public void Defaults()
    {
      // Arrange
      var settings = new NukitoSettings();

      // Assert
      settings.MockBehavior.Should().Be(MockBehavior.Loose);
      settings.MockVerification.Should().Be(MockVerification.All);
    }

    [NukitoFact]
    public void Merge()
    {
      // Arrange
      var settings0 = new NukitoSettings {MockBehavior = MockBehavior.Strict, MockVerification = MockVerification.None};
      var settings1 = new NukitoSettings {MockBehavior = MockBehavior.Loose};

      // Act
      var merged = NukitoSettings.Merge(new[] {settings0, settings1});

      // Assert
      merged.MockBehavior.Should().Be(MockBehavior.Loose);
      merged.MockVerification.Should().Be(MockVerification.None);
    }
  }
}