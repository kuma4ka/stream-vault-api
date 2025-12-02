using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamVault.Application.DTOs;
using StreamVault.Application.Interfaces.Services;

namespace StreamVault.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BroadcastsController(IBroadcastService broadcastService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBroadcast([FromBody] CreateBroadcastDto request)
    {
        var creatorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(creatorId))
        {
            return Unauthorized("User ID claim is missing.");
        }

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
    [AllowAnonymous]
    public IActionResult GetById(Guid id)
    {
        return Ok(new { Message = $"Placeholder for Broadcast {id}" });
    }
}