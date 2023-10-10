using Api.Application.DTOs.Employee;

namespace Api.Application.Services.Interfaces;

public interface IDeductionStep
{
    public decimal ComputeDeduction(GetEmployeeDto employee);
}