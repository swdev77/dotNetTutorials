using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error() => Problem();
}