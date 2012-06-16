using FluentAssertions;
using Moq;
using Nukito.Internal;

namespace Nukito.Test.Unit.Internal
{
  public class MockSettingsTest
  {
    [NukitoFact]
    public void Defaults()
    {
      // Arrange
      var settings = new MockSettings();

      // Assert
      settings.Behavior.Should().Be(MockBehavior.Loose);
      settings.Verification.Should().Be(MockVerification.All);
    }

    [NukitoFact]
    public void Merge()
    {
      // Arrange
      var classSettings = new MockSettings {Behavior = MockBehavior.Strict, Verification = MockVerification.None};
      var methodSettings = new MockSettings {Behavior = MockBehavior.Loose};

      // Act
      var merged = MockSettings.Merge(classSettings, methodSettings);

      // Assert
      merged.Behavior.Should().Be(MockBehavior.Loose);
      merged.Verification.Should().Be(MockVerification.None);
    }
  }
}