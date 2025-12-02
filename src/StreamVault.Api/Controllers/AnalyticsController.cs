using Microsoft.AspNetCore.Mvc;
using StreamVault.Application.Interfaces.Repositories;

namespace StreamVault.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IStatisticsRepository statisticsRepository) : ControllerBase
{
    [HttpGet("general")]
    public async Task<IActionResult> GetGeneralStats()
    {
        var users = await statisticsRepository.GetUserCountAsync();
        var maxViews = await statisticsRepository.GetMaxViewCountAsync();
        var variance = await statisticsRepository.GetVarianceViewCountAsync();

        return Ok(new 
        { 
            UserCount = users, 
            MaxBroadcastViews = maxViews, 
            ViewCountVariance = variance 
        });
    }

    [HttpGet("quartiles")]
    public async Task<IActionResult> GetQuartiles()
    {
        var quartiles = await statisticsRepository.GetViewCountQuartilesAsync();
        return Ok(quartiles);
    }

    [HttpGet("categories/top")]
    public async Task<IActionResult> GetTopCategories()
    {
        var stats = await statisticsRepository.GetMaxViewsPerCategoryAsync();
        return Ok(stats);
    }
}