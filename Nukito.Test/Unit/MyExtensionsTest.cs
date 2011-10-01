using System.Collections.Generic;
using FluentAssertions;
using Nukito.Internal;

namespace Nukito.Test.Unit
{
  public class MyExtensionsTest
  {
    [NukitoFact]
    public void AddOrReplaceAllShouldAdd()
    {
      // Arrange
      var mergeInto = new Dictionary<int, int>();
      var additional = new[] {new KeyValuePair<int, int>()};

      // Act
      mergeInto.AddOrReplaceAll(additional);

      // Assert
      mergeInto.Count.Should().Be(1);
    }

    [NukitoFact]
    public void AddOrReplaceAllShouldReplace()
    {
      // Arrange
      var mergeInto = new Dictionary<int, int> {{1, 10}};
      var additional = new Dictionary<int, int> {{1, 20}};

      // Act
      mergeInto.AddOrReplaceAll(additional);

      // Assert
      mergeInto.Count.Should().Be(1);
      mergeInto[1].Should().Be(20);
    }

    [NukitoFact]
    public void AddOrReplaceAllShouldAddAndReplace()
    {
      // Arrange
      var mergeInto = new Dictionary<int, int> {{1, 10}, {2, 20}};
      var additional = new Dictionary<int, int> {{2, -1}, {3, -2}};

      // Act
      mergeInto.AddOrReplaceAll(additional);

      // Assert
      mergeInto.Count.Should().Be(3);
      mergeInto[1].Should().Be(10);
      mergeInto[2].Should().Be(-1);
      mergeInto[3].Should().Be(-2);
    }
  }
}