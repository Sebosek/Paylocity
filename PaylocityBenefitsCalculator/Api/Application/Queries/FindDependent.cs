using Api.Application.DTOs.Dependent;

using LanguageExt.Common;

using MediatR;

namespace Api.Application.Queries;

public class FindDependent : IRequest<Result<GetDependentDto>>
{
    public FindDependent(int id)
    {
        Id = id;
    }
    
    public int Id { get; }
}