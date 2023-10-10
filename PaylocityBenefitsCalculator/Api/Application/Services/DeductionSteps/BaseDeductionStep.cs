using Api.Application.DTOs.Employee;
using Api.Application.Services.Interfaces;

namespace Api.Application.Services.DeductionSteps;

internal class BaseDeductionStep : IDeductionStep
{
    public decimal ComputeDeduction(GetEmployeeDto employee)
    {
        ArgumentNullException.ThrowIfNull(employee);
        
        return 1_000;
    }
}