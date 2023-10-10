using System.ComponentModel.DataAnnotations;

using Api.Application.Commands;
using Api.Application.DTOs;
using Api.Application.DTOs.Dependent;
using Api.Application.DTOs.Employee;
using Api.Application.Queries;
using Api.Infrastructure.Host.Controllers.Base;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Api.Infrastructure.Host.Controllers;

[ApiController]
[Route("api/v2/Employees")]
public class Employees2Controller : ApiBaseController
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get employee by id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetEmployeeDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public Task<IActionResult> FindAsync([FromRoute] [Required] int id, CancellationToken token) =>
        ResultedOkAsync(new FindEmployee(id), token);

    [HttpGet("{id:int}/paycheck")]
    [SwaggerOperation(Summary = "Get paycheck for employee by id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<PaycheckDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public Task<IActionResult> PaycheckAsync([FromRoute] [Required] int id, CancellationToken token) => 
        ResultedOkAsync(new ReadPaychecks(id), token);

    [HttpGet]
    [SwaggerOperation(Summary = "Get all employees")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<GetEmployeeDto>))]
    public Task<IReadOnlyCollection<GetEmployeeDto>> ReadAsync(CancellationToken token) =>
        Mediator.Send(new ReadEmployees(), token);

    [HttpPost("{employee:int}/dependents")]
    [SwaggerOperation(Summary = "Add dependent")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetDependentDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> AddDependentAsync(
        [FromRoute] [Required] int employee,
        [FromBody] [Required] AddDependent create,
        CancellationToken token)
    {
        var result = await Mediator.Send(create.ForEmployee(employee), token);

        return result.Match<IActionResult>(
            data => CreatedAtAction(
                "Read",
                "Dependents2",
                new { data.Id },
                data),
            Error);
    }

    [HttpDelete("{employee:int}/dependents/{id:int}")]
    [SwaggerOperation(Summary = "Remove dependent")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> RemoveDependentAsync(
        [FromRoute] [Required] int employee,
        [FromRoute] [Required] int id,
        CancellationToken token)
    {
        var result = await Mediator.Send(new RemoveDependent(employee, id), token);

        return result.Match<IActionResult>(_ => NoContent(), Error);
    }
}

[ApiController]
[Obsolete("Use v2 instead")]
[Route("api/v1/[controller]")]
public class EmployeesController : ApiBaseController
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get employee by id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<GetEmployeeDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Get([FromRoute] [Required] int id, CancellationToken token)
    {
        var result = await Mediator.Send(new FindEmployee(id), token);
        
        return result.Match<IActionResult>(data => Ok(new ApiResponse<GetEmployeeDto>
        {
            Success = true,
            Data = data,
        }), Error);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all employees")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IReadOnlyCollection<GetEmployeeDto>>))]
    public async Task<ApiResponse<IReadOnlyCollection<GetEmployeeDto>>> GetAll(CancellationToken token)
    {
        var result = await Mediator.Send(new ReadEmployees(), token);
        
        return new ApiResponse<IReadOnlyCollection<GetEmployeeDto>>
        {
            Success = true,
            Data = result,
        };
    }
}
