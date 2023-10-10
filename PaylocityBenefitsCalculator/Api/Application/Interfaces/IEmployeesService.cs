using Api.Domain.Entities;

namespace Api.Application.Interfaces;

public interface IEmployeesService
{
    public Task<IReadOnlyCollection<Employee>> ReadEmployeesAsync(CancellationToken token);
    
    public Task<Employee?> FindEmployeeAsync(int id, CancellationToken token);
}