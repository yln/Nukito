using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  public class RequestMockRepositoryScenarios
  {
    [NukitoFact]
    public void ShouldInjectMockRepositorySingleton(MockRepository repository1, MockRepository repository2)
    {
      // Assert
      repository1.Should().NotBeNull().And.BeSameAs(repository2);
    }
  }
}