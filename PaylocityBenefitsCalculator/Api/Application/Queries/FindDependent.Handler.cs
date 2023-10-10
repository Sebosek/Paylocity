using Api.Application.DTOs.Dependent;
using Api.Application.Exceptions;
using Api.Application.Interfaces;

using LanguageExt.Common;

using MediatR;

namespace Api.Application.Queries;

internal class FindDependentHandler : IRequestHandler<FindDependent, Result<GetDependentDto>>
{
    private readonly IDependentsService _service;

    public FindDependentHandler(IDependentsService service)
    {
        _service = service;
    }

    public async Task<Result<GetDependentDto>> Handle(FindDependent request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entity = await _service.FindDependentAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return new Result<GetDependentDto>(new NotFoundException($"Dependent with ID {request.Id} not found"));
        }

        return new GetDependentDto(entity);
    }
}