using Api.Application.DTOs.Dependent;
using Api.Application.Interfaces;

using MediatR;

namespace Api.Application.Queries;

internal class ReadDependentsHandler : IRequestHandler<ReadDependents, IReadOnlyCollection<GetDependentDto>>
{
    private readonly IDependentsService _service;

    public ReadDependentsHandler(IDependentsService service)
    {
        _service = service;
    }

    public async Task<IReadOnlyCollection<GetDependentDto>> Handle(ReadDependents request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entities = await _service.ReadDependentsAsync(cancellationToken);
        var items = entities.Select(s => new GetDependentDto(s)).ToList();

        return items;
    }
}