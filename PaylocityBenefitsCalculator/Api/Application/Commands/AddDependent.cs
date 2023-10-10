using System.ComponentModel.DataAnnotations;
using Api.Application.DTOs.Dependent;
using Api.Domain.Entities;
using LanguageExt.Common;
using MediatR;

namespace Api.Application.Commands;

public class AddDependent : IRequest<Result<GetDependentDto>>
{
    public AddDependent ForEmployee(int employee)
    {
        Employee = employee;
        
        return this;
    }
    
    public int Employee { get; private set; }
    
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    public Relationship Relationship { get; set; }
}