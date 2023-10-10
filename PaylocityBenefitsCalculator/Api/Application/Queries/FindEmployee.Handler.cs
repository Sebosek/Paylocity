using Api.Application.DTOs.Employee;
using Api.Application.Exceptions;
using Api.Application.Interfaces;

using LanguageExt.Common;

using MediatR;

namespace Api.Application.Queries;

internal class FindEmployeeHandler : IRequestHandler<FindEmployee, Result<GetEmployeeDto>>
{
    private readonly IEmployeesService _service;

    public FindEmployeeHandler(IEmployeesService service)
    {
        _service = service;
    }

    public async Task<Result<GetEmployeeDto>> Handle(FindEmployee request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entity = await _service.FindEmployeeAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return new Result<GetEmployeeDto>(new NotFoundException($"Employee with ID {request.Id} not found"));
        }

        return new GetEmployeeDto(entity);
    }
}