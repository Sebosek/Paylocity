using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure.Host;

public class ExceptionActionResult : IActionResult
{
    public Task ExecuteResultAsync(ActionContext context)
    {
        throw new NotImplementedException();
    }
}