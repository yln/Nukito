using FluentAssertions;
using Moq;
using Nukito.Internal;
using Nukito.Internal.Moq;
using Nukito.Test.Scenario;

namespace Nukito.Test.Unit.Internal.Moq
{
  public class MoqMockRepositoryTest
  {
    private readonly MoqMockRepository _repository;

    public MoqMockRepositoryTest (ReflectionHelper reflectionHelper)
    {
      // Integration like test
      _repository = new MoqMockRepository(reflectionHelper);
    }

    [NukitoFact]
    public void IsMockRepositoryType ()
    {
      // Act + Assert
      _repository.IsMockRepositoryType (typeof (IA)).Should ().BeFalse ();
      _repository.IsMockRepositoryType (typeof (Mock<IA>)).Should ().BeFalse ();
      _repository.IsMockRepositoryType (typeof (MockRepository)).Should ().BeTrue ();
    }

    [NukitoFact]
    public void CreateMock ()
    {
      // Arrange
      var settings = new MockSettings
      {
        Behavior = MockBehavior.Loose,
        DefaultValue = DefaultValue.Empty,
        CallBase = true
      };

      // Act
      var result = _repository.CreateMock (typeof (IA), settings);

      // Assert
      result.Should().BeAssignableTo<IA>();
      result.Should().BeAssignableTo<IMocked<IA>> ();

      var configuration = ((IMocked<IA>) result).Mock;
      configuration.Behavior.Should ().Be (settings.Behavior);
      configuration.DefaultValue.Should ().Be (settings.DefaultValue);
      configuration.CallBase.Should ().Be (settings.CallBase);
    }
  }
}