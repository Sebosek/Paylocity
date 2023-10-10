using Api.Application.DTOs.Employee;

namespace Api.Application.Services.Interfaces;

public interface IDeductionComputation
{
    public decimal Compute(GetEmployeeDto employee);
}