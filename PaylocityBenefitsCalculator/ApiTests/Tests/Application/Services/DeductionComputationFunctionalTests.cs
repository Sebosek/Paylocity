using System;

using Api;
using Api.Application.DTOs.Dependent;
using Api.Application.DTOs.Employee;
using Api.Application.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace ApiTests.Tests.Application.Services;

public class DeductionComputationFunctionalTests
{
    [Fact]
    public void Compute_RichOldEmployeeWithFamily()
    {
        var provider = new ServiceCollection().AddApiServices().BuildServiceProvider();
        var sut = provider.GetService<IDeductionComputation>()!;
        var result = sut.Compute(new GetEmployeeDto
        {
            DateOfBirth = DateTime.UtcNow.AddYears(-60),
            Dependents = new[] { new GetDependentDto() },
            Salary = 100_000,
        });
        
        Assert.Equal(3_800, result);
    }
    
    [Fact]
    public void Compute_YoungPoorWithoutFamily()
    {
        var provider = new ServiceCollection().AddApiServices().BuildServiceProvider();
        var sut = provider.GetService<IDeductionComputation>()!;
        var result = sut.Compute(new GetEmployeeDto
        {
            DateOfBirth = DateTime.UtcNow.AddYears(-20),
            Dependents = Array.Empty<GetDependentDto>(),
            Salary = 10_000,
        });
        
        Assert.Equal(1_000, result);
    }
}