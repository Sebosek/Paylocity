using Api.Domain;
using Api.Domain.Entities;
using DependentEntity = Api.Domain.Entities.Dependent;

namespace Api.Application.DTOs.Dependent;

public class GetDependentDto
{
    public int Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public Relationship Relationship { get; set; }
    
    public GetDependentDto() {}

    public GetDependentDto(DependentEntity entity)
    {
        Id = entity.Id;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        DateOfBirth = entity.DateOfBirth;
        Relationship = entity.Relationship;
    }
}
