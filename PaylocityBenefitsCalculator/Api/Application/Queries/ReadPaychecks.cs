using Api.Application.DTOs.Employee;

using LanguageExt.Common;

using MediatR;

namespace Api.Application.Queries;

public class ReadPaychecks : IRequest<Result<PaycheckDto>>
{
    public ReadPaychecks(int id)
    {
        Id = id;
    }
    
    public int Id { get; }
}