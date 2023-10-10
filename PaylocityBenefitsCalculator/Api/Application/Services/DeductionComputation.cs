using Api.Application.DTOs.Employee;
using Api.Application.Services.Interfaces;

namespace Api.Application.Services;

internal class DeductionComputation : IDeductionComputation
{
    private readonly IEnumerable<IDeductionStep> _steps;

    public DeductionComputation(IEnumerable<IDeductionStep> steps)
    {
        _steps = steps;
    }

    public decimal Compute(GetEmployeeDto employee)
    {
        return _steps.Aggregate<IDeductionStep, decimal>(0, (acc, curr) => acc + curr.ComputeDeduction(employee));
    }
}