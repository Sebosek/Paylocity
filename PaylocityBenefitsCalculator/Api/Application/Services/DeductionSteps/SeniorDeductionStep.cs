using Api.Application.DTOs.Employee;
using Api.Application.Services.Interfaces;

using Microsoft.Extensions.Internal;

namespace Api.Application.Services.DeductionSteps;

internal class SeniorDeductionStep : IDeductionStep
{
    private readonly ISystemClock _clock;

    public SeniorDeductionStep(ISystemClock clock)
    {
        _clock = clock;
    }

    public decimal ComputeDeduction(GetEmployeeDto employee)
    {
        ArgumentNullException.ThrowIfNull(employee);
        
        var diff = _clock.UtcNow.ToUniversalTime() - employee.DateOfBirth;
        var origin = DateTime.MinValue.AddDays(diff.TotalDays);
        var years = origin.Year - DateTime.MinValue.Year;
        
        if (years >= 50) return 200;
        return 0;
    }
}