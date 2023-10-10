using Api.Application.Exceptions;
using Api.Application.Interfaces;

using LanguageExt.Common;

using MediatR;

namespace Api.Application.Commands;

internal class RemoveDependentHandler : IRequestHandler<RemoveDependent, Result<Unit>>
{
    private readonly IEmployeesService _employees;
    
    private readonly IDependentsService _dependents;

    public RemoveDependentHandler(
        IEmployeesService employees,
        IDependentsService dependents)
    {
        _employees = employees;
        _dependents = dependents;
    }

    public async Task<Result<Unit>> Handle(RemoveDependent request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var employee = await _employees.FindEmployeeAsync(request.Employee, cancellationToken);
        if (employee is null)
        {
            return new Result<Unit>(new NotFoundException($"Employee with ID {request.Employee} not found"));
        }

        var dependent = await _dependents.FindDependentAsync(request.Id, cancellationToken);
        if (dependent is null)
        {
            // Personally I prefer to NOT return 404 when deleting not existing resource (due to the idempotency)
            // But this is what the most people do and expect, so...
            return new Result<Unit>(new NotFoundException($"Dependent with ID {request.Employee} not found"));
        }

        await _dependents.RemoveDependentAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}