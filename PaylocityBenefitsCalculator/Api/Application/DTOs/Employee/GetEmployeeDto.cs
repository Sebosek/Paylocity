using Api.Application.DTOs.Dependent;

using EmployeeEntity = Api.Domain.Entities.Employee;

namespace Api.Application.DTOs.Employee;

public class GetEmployeeDto
{
    public int Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public decimal Salary { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public IReadOnlyCollection<GetDependentDto> Dependents { get; set; } = new List<GetDependentDto>();
    
    public GetEmployeeDto() {}

    public GetEmployeeDto(EmployeeEntity entity)
    {
        Id = entity.Id;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        Salary = entity.Salary;
        DateOfBirth = entity.DateOfBirth;
        Dependents = entity.Dependents.Select(s => new GetDependentDto(s)).ToList();
    }
}
