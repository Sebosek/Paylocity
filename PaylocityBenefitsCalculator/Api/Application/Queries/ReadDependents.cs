using Api.Application.DTOs.Dependent;

using MediatR;

namespace Api.Application.Queries;

public class ReadDependents : IRequest<IReadOnlyCollection<GetDependentDto>>
{
}