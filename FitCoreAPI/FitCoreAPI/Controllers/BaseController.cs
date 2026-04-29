using Microsoft.AspNetCore.Mvc;

namespace FitCoreAPI.Controllers;

public abstract class BaseController : ControllerBase
{
    protected async Task<ActionResult> ExecuteSafely(Func<Task<ActionResult>> action)
    {
        try
        {
            return await action();
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499, new { message = "Operation was canceled" });
        }
        catch (NullReferenceException e)
        {
            return StatusCode(404, new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message }); 
        }
    }
}