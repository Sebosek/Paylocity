using Api.Application.Interfaces;
using Api.Domain.Entities;
using Api.Domain.Exceptions;

namespace Api.Infrastructure.Persistence;

internal class DependentsService : IDependentsService
{
    public Task<IReadOnlyCollection<Dependent>> ReadDependentsAsync(CancellationToken token)
    {
        var items = StaticData.DATA.SelectMany(s => s.Dependents).OrderBy(o => o.Id).ToList();
        
        return Task.FromResult<IReadOnlyCollection<Dependent>>(items);
    }

    public Task<Dependent?> FindDependentAsync(int id, CancellationToken token)
    {
        var item = StaticData.DATA.SelectMany(s => s.Dependents).FirstOrDefault(f => f.Id == id);
        
        return Task.FromResult(item);
    }
    
    public Task CreateDependentAsync(Dependent entity, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var id = StaticData.DATA.SelectMany(s => s.Dependents).Select(s => s.Id).Max() + 1;
        entity.Id = id;

        var employee = StaticData.DATA.FirstOrDefault(f => f.Id == entity.EmployeeId);
        if (employee is null)
        {
            throw new PaylocityBaseException("Employee not found");
        }
        
        employee.Dependents.Add(entity);
        return Task.CompletedTask;
    }
    
    public Task RemoveDependentAsync(int id, CancellationToken token)
    {
        var item = StaticData.DATA.SelectMany(s => s.Dependents).FirstOrDefault(f => f.Id == id);
        if (item is null)
        {
            throw new PaylocityBaseException("Dependent not found");
        }
        
        var employee = StaticData.DATA.FirstOrDefault(f => f.Id == item.EmployeeId);
        if (employee is null)
        {
            throw new PaylocityBaseException("Employee not found");
        }

        employee.Dependents.Remove(item);
        return Task.CompletedTask;
    }
}