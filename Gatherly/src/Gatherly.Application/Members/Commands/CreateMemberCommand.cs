using Gatherly.Application.Abstractions.Messaging;
using MediatR;

namespace Gatherly.Application.Members.Commands;

public sealed record CreateMemberCommand(
    string FirstName,
    string LastName,
    string Email) : ICommand<Guid>;