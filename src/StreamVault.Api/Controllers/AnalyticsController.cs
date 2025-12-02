using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamVault.Application.Interfaces.Repositories;
using StreamVault.Domain.Constants;

namespace StreamVault.Api.Controllers;

[ApiController]
[Route("api/analytics")]
// [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Moderator)] 
public class AnalyticsController(IStatisticsRepository statisticsRepository) : ControllerBase
{
    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var users = await statisticsRepository.GetUserCountAsync();
        var maxViews = await statisticsRepository.GetMaxViewCountAsync();
        var variance = await statisticsRepository.GetVarianceViewCountAsync();

        return Ok(new 
        { 
            TotalUsers = users, 
            PeakBroadcastViews = maxViews, 
            ViewsVariance = variance 
        });
    }

    [HttpGet("views-distribution")]
    public async Task<IActionResult> GetViewsDistribution()
    {
        var quartiles = await statisticsRepository.GetViewCountQuartilesAsync();
        return Ok(quartiles);
    }

    [HttpGet("category-rankings")]
    public async Task<IActionResult> GetCategoryRankings()
    {
        var stats = await statisticsRepository.GetMaxViewsPerCategoryAsync();
        return Ok(stats);
    }
}