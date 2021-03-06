﻿using System;
using FluentAssertions;
using FluentAssertions.Assertions;
using Moq;
using Nukito.Internal;
using Nukito.Test.Utility;

namespace Nukito.Test.Scenario
{
  public class VerifyMockScenarios
  {
    private void FulfillImplicitAndMarkedExpectations(Mock<IB> mock)
    {
      mock.Setup(b => b.DoSomething()).Verifiable();
      mock.Setup(b => b.DoSomethingElse());

      mock.Object.DoSomething();
      mock.Object.DoSomethingElse();
    }

    private void FulfillMarkedExpectations(Mock<IB> mock)
    {
      mock.Setup(b => b.DoSomething()).Verifiable();
      mock.Setup(b => b.DoSomethingElse());

      mock.Object.DoSomething();
    }

    private void DoNotFulfillAnyExpectations(Mock<IB> mock)
    {
      mock.Setup(b => b.DoSomething()).Verifiable();
      mock.Setup(b => b.DoSomethingElse());
      // Expectations are not fulfilled
    }

    private void DoNotFulfillImplicitExpectations(Mock<IB> mock)
    {
      mock.Setup(b => b.DoSomething());
      // Expectations are not fulfilled
    }

    private void DoNotFulfillMarkedExpectations(Mock<IB> mock)
    {
      mock.Setup(b => b.DoSomething()).Verifiable();
      // Expectations are not fulfilled
    }

    private Action GetTest(Action<Mock<IB>> method, MockSettings settings = null)
    {
      return TestUtility.GetTest(method, settings: settings);
    }


    [NukitoFact]
    public void VerifyAllIsDefault()
    {
      // Act + Assert
      GetTest(FulfillImplicitAndMarkedExpectations)
        .ShouldNotThrow();
    }

    [NukitoFact]
    public void VerifyMarkedDoesNotThrowForImplicitExpectations()
    {
      // Arrange + Act + Assert
      GetTest(FulfillMarkedExpectations, new MockSettings {Verification = MockVerification.Marked})
        .ShouldNotThrow();
    }

    [NukitoFact]
    public void VerifyNone()
    {
      // Arrange + Act + Assert
      GetTest(DoNotFulfillAnyExpectations, new MockSettings {Verification = MockVerification.None})
        .ShouldNotThrow();
    }

    [NukitoFact]
    public void VerifyAllThrowsForImplicitExpectations()
    {
      // Act + Assert
      GetTest(DoNotFulfillImplicitExpectations)
        .ShouldThrow<MockException>()
        .WithMessage("b => b.DoSomething()", ComparisonMode.Substring);
    }

    [NukitoFact]
    public void VerifyAllThrowsForMarkedExpectations()
    {
      // Act + Assert
      GetTest(DoNotFulfillMarkedExpectations)
        .ShouldThrow<MockException>()
        .WithMessage("b => b.DoSomething()", ComparisonMode.Substring);
    }

    [NukitoFact]
    public void VerifyMarkedThrowsForMarkedExpectations()
    {
      // Arrange + Act + Assert
      GetTest(DoNotFulfillMarkedExpectations, new MockSettings {Verification = MockVerification.Marked})
        .ShouldThrow<MockException>()
        .WithMessage("b => b.DoSomething()", ComparisonMode.Substring);
    }
  }
}