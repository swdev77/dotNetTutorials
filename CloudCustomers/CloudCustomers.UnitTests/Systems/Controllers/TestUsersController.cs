using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var userServiceMock = new Mock<IUserService>();
        var sut = new UsersController(userServiceMock.Object);
        // Act
        var result = (OkObjectResult)await sut.Get();
        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
    {
        // Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock
            .Setup(x => x.GetUsersAsync())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(userServiceMock.Object);
        // Act
        await sut.Get();
        // Assert
        userServiceMock.Verify(x => x.GetUsersAsync(), Times.Once);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        // Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock
            .Setup(x => x.GetUsersAsync())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(userServiceMock.Object);
        // Act
        var result = (OkObjectResult)await sut.Get();
        // Assert
        result.Value.Should().BeOfType<List<User>>();
    }
}