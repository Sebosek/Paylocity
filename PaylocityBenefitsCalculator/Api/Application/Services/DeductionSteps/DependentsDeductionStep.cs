using Api.Application.DTOs.Employee;
using Api.Application.Services.Interfaces;

namespace Api.Application.Services.DeductionSteps;

internal class DependentsDeductionStep : IDeductionStep
{
    public decimal ComputeDeduction(GetEmployeeDto employee)
    {
        ArgumentNullException.ThrowIfNull(employee);
        if (employee.Dependents is null) return 0;
        
        return employee.Dependents.Count * 600;
    }
}