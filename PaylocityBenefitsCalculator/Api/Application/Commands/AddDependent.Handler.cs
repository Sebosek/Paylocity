using Api.Application.DTOs.Dependent;
using Api.Application.Exceptions;
using Api.Application.Interfaces;
using Api.Domain.Entities;
using LanguageExt.Common;
using MediatR;

namespace Api.Application.Commands;

internal class AddDependentHandler : IRequestHandler<AddDependent, Result<GetDependentDto>>
{
    private readonly IEmployeesService _employees;
    
    private readonly IDependentsService _dependents;

    public AddDependentHandler(
        IEmployeesService employees,
        IDependentsService dependents)
    {
        _employees = employees;
        _dependents = dependents;
    }

    public async Task<Result<GetDependentDto>> Handle(AddDependent request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var employee = await _employees.FindEmployeeAsync(request.Employee, cancellationToken);
        if (employee is null)
        {
            return new Result<GetDependentDto>(new NotFoundException($"Employee with ID {request.Employee} not found"));
        }

        // I've intentionally violated the Open/Closed principle just for the sake of brevity
        // Better would have standalone validations per business case following Single responsible principle
        var check = new[] { Relationship.DomesticPartner, Relationship.Spouse };
        if (check.Contains(request.Relationship) && employee.Dependents.Any(a => check.Contains(a.Relationship)))
        {
            var message = $"Employee with ID {request.Employee} already has Spouse or Domestic Partner";
            return new Result<GetDependentDto>(new ConflictException(message));
        }

        var entity = CreateEntity(request);
        await _dependents.CreateDependentAsync(entity, cancellationToken);
        
        return new GetDependentDto(entity);
    }

    private static Dependent CreateEntity(AddDependent request) =>
        new()
        {
            EmployeeId = request.Employee,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Relationship = request.Relationship,
        };
}