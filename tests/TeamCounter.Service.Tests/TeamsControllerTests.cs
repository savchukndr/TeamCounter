using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TeamCounter.Application.Dtos;
using TeamCounter.Application.Handlers.CreateCounter;
using TeamCounter.Application.Handlers.CreateTeam;
using TeamCounter.Application.Handlers.DeleteCounter;
using TeamCounter.Application.Handlers.DeleteTeam;
using TeamCounter.Application.Handlers.GetTeams;
using TeamCounter.Application.Handlers.GetTeamTotal;
using TeamCounter.Domain.Models;
using TeamCounter.Service.Controllers;

namespace TeamCounter.Service.Tests;

public class TeamsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TeamsController _controller;
    private readonly Fixture _fixture;

    public TeamsControllerTests()
    {
        _fixture = new Fixture();
        _mediatorMock = new Mock<IMediator>();
        _controller = new TeamsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateTeam_Returns_CreatedAtAction_When_Successful()
    {
        // Arrange
        var teamCreateDto = _fixture.Create<TeamCreateDto>();
        var expectedTeamId = _fixture.Create<Guid>();
        Arrange_CreateTeamHandler(expectedTeamId);

        // Act
        var result = await _controller.CreateTeam(teamCreateDto);

        // Assert
        using (new AssertionScope())
        {
            result.Should().BeOfType<CreatedAtActionResult>();
        
            var createdResult = result as CreatedAtActionResult;
            
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(201);
            createdResult.ActionName.Should().Be("GetTeamTotal");
            createdResult.RouteValues!["teamId"].Should().Be(expectedTeamId);
        }
    }

    [Fact]
    public async Task GetTeams_Returns_Ok_With_Team_List()
    {
        // Arrange
        var expectedTeams = _fixture.Create<List<Team>>();
        Arrange_GetTeamsHandler(expectedTeams);
    
        // Act
        var result = await _controller.GetTeams();
    
        // Assert
        using (new AssertionScope())
        {
            result.Should().BeOfType<OkObjectResult>();
            
            var okResult = result as OkObjectResult;
            var actualTeams = okResult!.Value as List<Team>;
            
            actualTeams.Should().BeEquivalentTo(expectedTeams);
        }
    }

    [Fact]
    public async Task AddCounter_Returns_Ok_With_CounterId_When_Successful()
    {
        // Arrange
        var teamId = _fixture.Create<Guid>();
        var counterCreateDto = _fixture.Create<CounterCreateDto>();
        var expectedCounterId = _fixture.Create<Guid>();
        Arrange_CreateCounterHandler(expectedCounterId);

        // Act
        var result = await _controller.AddCounter(teamId, counterCreateDto);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task AddCounter_Returns_NotFound_When_Team_Not_Found()
    {
        // Arrange
        var teamId = _fixture.Create<Guid>();
        var counterCreateDto = _fixture.Create<CounterCreateDto>();
        Arrange_CreateCounterHandlerWithException(teamId);

        // Act
        var result = await _controller.AddCounter(teamId, counterCreateDto);

        // Assert
        using (new AssertionScope())
        {
            result.Should().BeOfType<NotFoundObjectResult>();
            
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult!.Value.Should().Be($"Team {teamId} does not exist");
        }
    }

    [Fact]
    public async Task GetTeamTotal_Returns_Ok_With_Total_Steps()
    {
        // Arrange
        var teamId = _fixture.Create<Guid>();
        var expectedTotal = _fixture.Create<int>();
        Arrange_GetTeamTotalHandler(expectedTotal);

        // Act
        var result = await _controller.GetTeamTotal(teamId);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeleteTeam_Returns_NoContent_When_Successful()
    {
        // Arrange
        var teamId = _fixture.Create<Guid>();
        Arrange_DeleteTeamHandler();
    
        // Act
        var result = await _controller.DeleteTeam(teamId);
    
        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteCounter_Returns_NoContent_When_Successful()
    {
        // Arrange
        var teamId = _fixture.Create<Guid>();
        var counterId = _fixture.Create<Guid>();
        Arrange_DeleteCounterHandler();
    
        // Act
        var result = await _controller.DeleteCounter(teamId, counterId);
    
        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    private void Arrange_CreateTeamHandler(Guid expectedTeamId)
    {
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<CreateTeamCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(expectedTeamId);
    }

    private void Arrange_GetTeamsHandler(IEnumerable<Team> expectedTeams)
    {
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<GetTeamsCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(expectedTeams);
    }

    private void Arrange_CreateCounterHandler(Guid expectedCounterId)
    {
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<CreateCounterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCounterId);
    }

    private void Arrange_CreateCounterHandlerWithException(Guid teamId)
    {
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<CreateCounterCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException($"Team {teamId} does not exist"));
    }

    private void Arrange_GetTeamTotalHandler(int expectedTotal)
    {
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<GetTeamTotalCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTotal);
    }

    private void Arrange_DeleteTeamHandler()
    {
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<DeleteTeamCommand>(), It.IsAny<CancellationToken>())).Verifiable();
    }

    private void Arrange_DeleteCounterHandler()
    {
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<DeleteCounterCommand>(), It.IsAny<CancellationToken>())).Verifiable();
    }
}