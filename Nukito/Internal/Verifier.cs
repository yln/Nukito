namespace Nukito.Internal
{
  internal class Verifier : IVerifier
  {
    private readonly IMockHandler _mockHandler;
    private readonly MockVerification _mockVerification;

    public Verifier(IMockHandler mockHandler, MockVerification mockVerification)
    {
      _mockHandler = mockHandler;
      _mockVerification = mockVerification;
    }

    public void VerifyMocks()
    {
      switch (_mockVerification)
      {
        case MockVerification.All:
          _mockHandler.VerifyAll();
          break;
        case MockVerification.Marked:
          _mockHandler.VerifyMarked();
          break;
        case MockVerification.None:
          // Do Nothing
          break;
      }
    }
  }
}