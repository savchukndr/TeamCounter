using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamCounter.Application.Dtos;
using TeamCounter.Application.Handlers.CreateCounter;
using TeamCounter.Application.Handlers.CreateTeam;
using TeamCounter.Application.Handlers.DeleteCounter;
using TeamCounter.Application.Handlers.DeleteTeam;
using TeamCounter.Application.Handlers.GetCounters;
using TeamCounter.Application.Handlers.GetLeaderBoard;
using TeamCounter.Application.Handlers.GetTeams;
using TeamCounter.Application.Handlers.GetTeamTotal;
using TeamCounter.Application.Handlers.StepIncrement;

namespace TeamCounter.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] TeamCreateDto dto)
    {
        try
        {
            var command = new CreateTeamCommand(dto.Name);
        
            var teamId = await mediator.Send(command);
        
            return CreatedAtAction(nameof(GetTeamTotal), new { teamId }, new { teamId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTeams()
    {
        var command = new GetTeamsCommand();
        
        var teams = await mediator.Send(command);
        
        return Ok(teams);
    }
    
    [HttpDelete("{teamId:guid}")]
    public async Task<IActionResult> DeleteTeam(Guid teamId)
    {
        var command = new DeleteTeamCommand(teamId);
        
        await mediator.Send(command);
        
        return NoContent();
    }

    [HttpPost("{teamId:guid}/counters")]
    public async Task <IActionResult> AddCounter(Guid teamId, [FromBody] CounterCreateDto dto)
    {
        try
        {
            var command = new CreateCounterCommand(teamId, dto.Name);

            var counterId = await mediator.Send(command);

            return Ok(new { counterId });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{teamId:guid}/counters/{counterId:guid}")]
    public async Task<IActionResult> Increment(Guid teamId, Guid counterId, [FromBody] StepIncrementDto dto)
    {
        try
        {
            var command = new StepIncrementCommand(teamId, counterId, dto.Steps);

            await mediator.Send(command);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{teamId:guid}/total")]
    public async Task<IActionResult> GetTeamTotal(Guid teamId)
    {
        try
        {
            var command = new GetTeamTotalCommand(teamId);
            
            var total = await mediator.Send(command);
            
            return Ok(new { teamId, totalSteps = total });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetLeaderboard()
    {
        var command = new GetLeaderBoardCommand();
        
        var leaderboard = await mediator.Send(command);
        
        return Ok(leaderboard);
    }

    [HttpGet("{teamId:guid}/counters")]
    public async Task<IActionResult> GetCounters(Guid teamId)
    {
        try
        {
            var command = new GetCountersCommand(teamId);
            
            var counters = await mediator.Send(command);
            
            return Ok(counters);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpDelete("{teamId:guid}/counters/{counterId:guid}")]
    public async Task<IActionResult> DeleteCounter(Guid teamId, Guid counterId)
    {
        try
        {
            var command = new DeleteCounterCommand(teamId, counterId);
            
            await mediator.Send(command);
            
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}