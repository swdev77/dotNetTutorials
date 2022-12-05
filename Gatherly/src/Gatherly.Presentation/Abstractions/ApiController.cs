using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Presentation.Abstractions;

public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
}