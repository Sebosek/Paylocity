using System;

using Api.Application.DTOs.Employee;
using Api.Application.Services.DeductionSteps;

using Xunit;

namespace ApiTests.Tests.Application.Services.DeductionSteps;

public class BaseDeductionStepTests
{
    [Fact]
    public void ComputeDeduction_ExpectedResult()
    {
        var sut = new BaseDeductionStep();
        
        var result = sut.ComputeDeduction(new GetEmployeeDto());
        
        Assert.Equal(1_000, result);
    }

    [Fact]
    public void ComputeDeduction_NullArgument_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => new BaseDeductionStep().ComputeDeduction(null!));
    }
}