using System;
using System.Globalization;

using Api.Application.DTOs.Employee;
using Api.Application.Services.DeductionSteps;
using Api.Application.Services.Interfaces;

using Microsoft.Extensions.Internal;

using Xunit;

namespace ApiTests.Tests.Application.Services.DeductionSteps;

public class SeniorDeductionStepTests
{
    private readonly IDeductionStep _sut = new SeniorDeductionStep(new MockSystemClock());
    
    [Fact]
    public void ComputeDeduction_ExpectedResult()
    {
        var date = new DateTime(2023, 10, 1, CultureInfo.InvariantCulture.Calendar);
        var birthday = date.AddYears(-50);
        
        var result = _sut.ComputeDeduction(new GetEmployeeDto
        {
            DateOfBirth = birthday
        });
        
        Assert.Equal(200, result);
    }
    
    [Fact]
    public void ComputeDeduction_OverThreshold_ExpectedResult()
    {
        var date = new DateTime(2023, 10, 1, CultureInfo.InvariantCulture.Calendar);
        var birthday = date.AddYears(-90);
        
        var result = _sut.ComputeDeduction(new GetEmployeeDto
        {
            DateOfBirth = birthday
        });
        
        Assert.Equal(200, result);
    }
    
    [Fact]
    public void ComputeDeduction_BellowThreshold_ZeroResult()
    {
        var date = new DateTime(2023, 10, 1, CultureInfo.InvariantCulture.Calendar);
        var birthday = date.AddYears(-20);
        
        var result = _sut.ComputeDeduction(new GetEmployeeDto
        {
            DateOfBirth = birthday
        });
        
        Assert.Equal(0, result);
    }

    [Fact]
    public void ComputeDeduction_NullArgument_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => _sut.ComputeDeduction(null!));
    }
    
    private class MockSystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => new(new DateTime(2023, 10, 1, CultureInfo.InvariantCulture.Calendar));
    }
}