﻿using FluentAssertions;
using Moq;
using Nukito.Internal;
using Nukito.Internal.Moq;

namespace Nukito.Test.Unit.Internal.Moq
{
  public class MoqResolverTest
  {
    [NukitoFact]
    public void GetReturnsInnerValue (MoqResolver moqResolver, Mock<IResolver> innerResolver, Context context)
    {
      // Arrange
      var request = new Request (typeof (string), false, context);
      var fakeInnerValue = new object();
      innerResolver.Setup(x => x.Get (request)).Returns (fakeInnerValue);

      // Act
      var result = moqResolver.Get (request);

      // Assert
      result.Should().BeSameAs (fakeInnerValue);
    }

    [NukitoFact, MockSettings(Behavior = MockBehavior.Strict)]
    public void GetRetrievesConfigFromInnerValue (MoqResolver moqResolver, Mock<IResolver> innerResolver, Context context)
    {
      // Arrange
      var fakeInnerValue = new Mock<IMocked>();
      innerResolver
          .Setup (x => x.Get (It.Is ((Request r) => r.Type == typeof (string) && r.ForceMockCreation && r.Context == context)))
          .Returns (fakeInnerValue.Object);

      // Act
      var result = moqResolver.Get (new Request (typeof (Mock<string>), false, context));

      // Assert
      result.Should ().BeSameAs (fakeInnerValue);
    }
  }
}