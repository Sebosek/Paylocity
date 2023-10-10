using Api.Domain.Entities;

namespace Api.Application.Interfaces;

public interface IDependentsService
{
    public Task<IReadOnlyCollection<Dependent>> ReadDependentsAsync(CancellationToken token);
    
    public Task<Dependent?> FindDependentAsync(int id, CancellationToken token);

    Task CreateDependentAsync(Dependent entity, CancellationToken token);

    Task RemoveDependentAsync(int id, CancellationToken token);
}