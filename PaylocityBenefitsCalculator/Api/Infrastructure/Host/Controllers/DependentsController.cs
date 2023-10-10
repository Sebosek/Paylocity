using System.ComponentModel.DataAnnotations;

using Api.Application.DTOs;
using Api.Application.DTOs.Dependent;
using Api.Application.Queries;
using Api.Infrastructure.Host.Controllers.Base;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Api.Infrastructure.Host.Controllers;

[ApiController]
[Route("api/v2/Dependents")]
public class Dependents2Controller : ApiBaseController
{
    [HttpGet("{id:int}", Name = "ReadAsync")]
    [SwaggerOperation(Summary = "Get dependent by id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<GetDependentDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public Task<IActionResult> ReadAsync(
        [FromRoute] [Required] int id,
        CancellationToken token) =>
        ResultedOkAsync(new FindDependent(id), token);

    [HttpGet]
    [SwaggerOperation(Summary = "Get all dependents")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IReadOnlyCollection<GetDependentDto>>))]
    public Task<IReadOnlyCollection<GetDependentDto>> ReadAllAsync(CancellationToken token) =>
        Mediator.Send(new ReadDependents(), token);
}

[ApiController]
[Obsolete("Use v2 instead")]
[Route("api/v1/[controller]")]
public class DependentsController : ApiBaseController
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get dependent by id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<GetDependentDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Get(
        [FromRoute] [Required] int id, 
        CancellationToken token)
    {
        var result = await Mediator.Send(new FindDependent(id), token);
        
        return result.Match<IActionResult>(data => Ok(new ApiResponse<GetDependentDto>
        {
            Success = true,
            Data = data,
        }), Error);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all dependents")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IReadOnlyCollection<GetDependentDto>>))]
    public async Task<ApiResponse<IReadOnlyCollection<GetDependentDto>>> GetAll(CancellationToken token)
    {
        var result = await Mediator.Send(new ReadDependents(), token);
        
        return new ApiResponse<IReadOnlyCollection<GetDependentDto>>
        {
            Success = true,
            Data = result,
        };
    }
}
