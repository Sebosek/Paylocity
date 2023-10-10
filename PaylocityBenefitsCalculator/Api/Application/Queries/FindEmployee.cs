using Api.Application.DTOs.Employee;

using LanguageExt.Common;

using MediatR;

namespace Api.Application.Queries;

public class FindEmployee : IRequest<Result<GetEmployeeDto>>
{
    public FindEmployee(int id)
    {
        Id = id;
    }
    
    public int Id { get; }
}