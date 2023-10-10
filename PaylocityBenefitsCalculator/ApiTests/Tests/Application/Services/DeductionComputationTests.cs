using Api.Application.DTOs.Employee;
using Api.Application.Services;
using Api.Application.Services.Interfaces;

using Xunit;

namespace ApiTests.Tests.Application.Services;

public class DeductionComputationTests
{
    [Fact]
    public void Compute_ValidData_Success()
    {
        var spy = new SpyDeductionStep();
        var sut = new DeductionComputation(new[] { spy });

        var result = sut.Compute(new GetEmployeeDto());
        
        Assert.Equal(SpyDeductionStep.VALUE, result);
        Assert.True(spy.HasBeenCalled);
    }
    
    private class SpyDeductionStep : IDeductionStep
    {
        public const decimal VALUE = 1;
        
        public bool HasBeenCalled { get; private set; }
        
        public decimal ComputeDeduction(GetEmployeeDto employee)
        {
            HasBeenCalled = true;

            return VALUE;
        }
    }
}