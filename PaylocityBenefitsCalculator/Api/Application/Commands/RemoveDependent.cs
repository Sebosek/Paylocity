using LanguageExt.Common;

using MediatR;

namespace Api.Application.Commands;

public class RemoveDependent : IRequest<Result<Unit>>
{
    public RemoveDependent(int employee, int id)
    {
        Employee = employee;
        Id = id;
    }
    
    public int Employee { get; private set; }
    
    public int Id { get; private set; }
}