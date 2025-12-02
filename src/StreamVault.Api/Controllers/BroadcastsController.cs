using Microsoft.AspNetCore.Mvc;
using StreamVault.Application.DTOs;
using StreamVault.Application.Interfaces.Services;

namespace StreamVault.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BroadcastsController(IBroadcastService broadcastService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBroadcast([FromBody] CreateBroadcastDto request)
    {
        var creatorId = "test-streamer-id";

        try
        {
            var result = await broadcastService.CreateBroadcastAsync(creatorId, request);
            
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(new { Message = $"Placeholder for Broadcast {id}" });
    }
}