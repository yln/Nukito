using FluentAssertions;
using Moq;
using Nukito.Internal;
using Nukito.Test.Scenario;
using Nukito.Test.Utility;

namespace Nukito.Test.Unit
{
  public class MoqMockHandlerTest
  {
    [NukitoFact]
    public void CreateMock()
    {
      // Arrange
      var mockHandler = new MoqMockHandler(new MockRepository(MockBehavior.Loose));

      // Act
      object result = mockHandler.CreateMock(typeof (IA));

      // Assert
      result.Should().BeMock<IA>();
    }
  }
}