using Api.Application.DTOs.Employee;
using Api.Application.Services.Interfaces;

namespace Api.Application.Services.DeductionSteps;

internal class RichDeductionStep : IDeductionStep
{
    public decimal ComputeDeduction(GetEmployeeDto employee)
    {
        ArgumentNullException.ThrowIfNull(employee);
        
        if (employee.Salary >= 80_000) return employee.Salary * 0.02m;
        return 0;
    }
}