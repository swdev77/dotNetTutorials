using System;
using Gatherly.Domain.Shared;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Application.Members.Commands;
using Moq;
using FluentAssertions;
using Gatherly.Domain.ValueObjects;
using Gatherly.Domain.Entities;

namespace Gatherly.Application.UnitTests.Members.Commands;

public class CreateMemberCommandHandlerTests
{
    private readonly Mock<IMemberRepository> _memberRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateMemberCommand command;
    private readonly CreateMemberCommandHandler handler;

    public CreateMemberCommandHandlerTests()
    {
        _memberRepositoryMock = new();
        _unitOfWorkMock = new();
        command = new CreateMemberCommand("first", "last", "email@test.com");
        handler = new CreateMemberCommandHandler(_memberRepositoryMock.Object, _unitOfWorkMock.Object);   
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenEmailIsNotUnique()
    {
        _memberRepositoryMock.Setup(x => x.IsEmailUniqueAsync(
            It.IsAny<Email>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(false);


        Result<Guid> result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Member.EmailAlreadyInUse);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenEmailIsUnique()
    {
        _memberRepositoryMock.Setup(x => x.IsEmailUniqueAsync(
            It.IsAny<Email>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(true);


        Result<Guid> result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_CallAddOnRepository_WhenEmailIsUnique()
    {
        _memberRepositoryMock.Setup(x => x.IsEmailUniqueAsync(
            It.IsAny<Email>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(true);

        Result<Guid> result = await handler.Handle(command, default);

        _memberRepositoryMock.Verify(x => x.Add(It.Is<Member>(m => m.Id == result.Value)), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenEmailIsNotUnique()
    {
        _memberRepositoryMock.Setup(x => x.IsEmailUniqueAsync(
            It.IsAny<Email>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(false);

        Result<Guid> result = await handler.Handle(command, default);

        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}