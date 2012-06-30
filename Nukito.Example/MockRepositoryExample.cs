using FluentAssertions;
using Moq;

namespace Nukito.Example
{
  public class MockRepositoryExample
  {
    [NukitoFact]
    public void MockRepositoryCanBeRetrieved (MockRepository mockRepository)
    {
      // Assert
      mockRepository.Should().NotBeNull();

      // Do stuff with the repository ...
    }
  }
}