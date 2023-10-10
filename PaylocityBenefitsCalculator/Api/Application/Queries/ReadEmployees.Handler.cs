using Api.Application.DTOs.Employee;
using Api.Application.Interfaces;

using MediatR;

namespace Api.Application.Queries;

internal class ReadEmployeesHandler : IRequestHandler<ReadEmployees, IReadOnlyCollection<GetEmployeeDto>>
{
    private readonly IEmployeesService _service;

    public ReadEmployeesHandler(IEmployeesService service)
    {
        _service = service;
    }

    public async Task<IReadOnlyCollection<GetEmployeeDto>> Handle(ReadEmployees request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entities = await _service.ReadEmployeesAsync(cancellationToken);
        var items = entities.Select(s => new GetEmployeeDto(s)).ToList();

        return items;
    }
}