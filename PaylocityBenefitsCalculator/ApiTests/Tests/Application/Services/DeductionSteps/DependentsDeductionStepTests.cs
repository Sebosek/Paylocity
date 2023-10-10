using System;
using Api.Application.DTOs.Dependent;
using Api.Application.DTOs.Employee;
using Api.Application.Services.DeductionSteps;

using Xunit;

namespace ApiTests.Tests.Application.Services.DeductionSteps;

public class DependentsDeductionStepTests
{
    [Fact]
    public void ComputeDeduction_ExpectedResult()
    {
        var dependents = new[]
        {
            new GetDependentDto(),
            new GetDependentDto(),
            new GetDependentDto(),
        };
        
        var sut = new DependentsDeductionStep();
        var result = sut.ComputeDeduction(new GetEmployeeDto
        {
            Dependents = dependents
        });
        
        Assert.Equal(1_800, result);
    }
    
    [Fact]
    public void ComputeDeduction_NullDependents_ZeroResult()
    {
        var sut = new DependentsDeductionStep();
        var result = sut.ComputeDeduction(new GetEmployeeDto
        {
            Dependents = null!
        });
        
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void ComputeDeduction_NullArgument_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => new DependentsDeductionStep().ComputeDeduction(null!));
    }
}