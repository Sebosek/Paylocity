using Api.Application.Interfaces;
using Api.Domain.Entities;

namespace Api.Infrastructure.Persistence;

public class EmployeesService : IEmployeesService
{
    public Task<IReadOnlyCollection<Employee>> ReadEmployeesAsync(CancellationToken token)
    {
        return Task.FromResult<IReadOnlyCollection<Employee>>(StaticData.DATA.OrderBy(o => o.Id).ToList());
    }

    public Task<Employee?> FindEmployeeAsync(int id, CancellationToken token)
    {
        var item = StaticData.DATA.FirstOrDefault(f => f.Id == id);
        
        return Task.FromResult(item);
    }
}