using System;
using Api.Application.DTOs.Dependent;
using Api.Application.DTOs.Employee;
using Api.Application.Services.DeductionSteps;

using Xunit;

namespace ApiTests.Tests.Application.Services.DeductionSteps;

public class RichDeductionStepTests
{
    [Fact]
    public void ComputeDeduction_BellowThreshold()
    {
        var sut = new RichDeductionStep();
        var result = sut.ComputeDeduction(new GetEmployeeDto
        {
            Salary = 1
        });
        
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void ComputeDeduction_ExpectedResult()
    {
        const decimal VALUE = 80_000;
        
        var sut = new RichDeductionStep();
        var result = sut.ComputeDeduction(new GetEmployeeDto
        {
            Salary = VALUE
        });
        
        Assert.Equal(VALUE * 0.02m, result);
    }
    
    [Fact]
    public void ComputeDeduction_NegativeValue_ZeroResult()
    {
        var sut = new RichDeductionStep();
        var result = sut.ComputeDeduction(new GetEmployeeDto
        {
            Salary = -1
        });
        
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void ComputeDeduction_NullArgument_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => new RichDeductionStep().ComputeDeduction(null!));
    }
}