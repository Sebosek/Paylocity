using Api.Application.DTOs.Employee;

using MediatR;

namespace Api.Application.Queries;

public class ReadEmployees : IRequest<IReadOnlyCollection<GetEmployeeDto>>
{
}