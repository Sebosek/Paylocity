using Api.Infrastructure.Host.ExceptionWriters;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Host.Controllers.Base;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
public class ApiBaseController : ControllerBase
{
    private IDictionary<Type, IExceptionWriter>? _writers;
    
    private ISender? _mediator;

    private IDictionary<Type, IExceptionWriter> Writers
    {
        get
        {
            if (_writers is null)
            {
                var writers = HttpContext.RequestServices.GetService<IEnumerable<IExceptionWriter>>()!;
                _writers = writers.ToDictionary(k => k.Type, v => v);
            }

            return _writers;
        }
    }
    
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>()!;
    
    protected async Task<IActionResult> ResultedOkAsync<T>(IRequest<Result<T>> q, CancellationToken token)
    {
        var result = await Mediator.Send(q, token);
        
        return result.Match(data => Ok(data), Error);
    }
    
    protected IActionResult Error(Exception ex)
    {
        var type = ex.GetType();
        
        if (Writers.TryGetValue(type, out var writer))
        {
            return writer.Write(ex);
        }

        var common = Writers.FirstOrDefault(f => f.Key == typeof(Exception));
        if (common.Key is null)
        {
            throw ex;
        }

        return common.Value.Write(ex);
    }
}