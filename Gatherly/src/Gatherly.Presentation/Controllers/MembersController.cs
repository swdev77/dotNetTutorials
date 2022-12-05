using Gatherly.Application.Members.Commands;
using Gatherly.Application.Members.Queries.GetMemberById;
using Gatherly.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Presentation.Controllers;

[Route("api/[controller]")]
public sealed class MembersController : ApiController
{
    public MembersController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> RegisterMember(CancellationToken cancellationToken)
    {
        var command = new CreateMemberCommand("John", "Doe", "john.doe@mail.net");
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMemberByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}