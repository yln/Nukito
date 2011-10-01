using Moq;

namespace Nukito.Internal
{
  internal class MoqVerifier : IVerifier
  {
    private readonly MockRepository _mockRepository;
    private readonly MockVerification _mockVerification;

    public MoqVerifier(MockRepository mockRepository, MockVerification mockVerification)
    {
      _mockRepository = mockRepository;
      _mockVerification = mockVerification;
    }

    public void VerifyMocks()
    {
      switch (_mockVerification)
      {
        case MockVerification.All:
          _mockRepository.VerifyAll();
          break;
        case MockVerification.Marked:
          _mockRepository.Verify();
          break;
        case MockVerification.None:
          // Do Nothing
          break;
      }
    }
  }
}