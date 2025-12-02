using StreamVault.Application.DTOs.Statistics;

namespace StreamVault.Application.Interfaces.Repositories;

public interface IStatisticsRepository
{
    Task<int> GetUserCountAsync();
    Task<long> GetMaxViewCountAsync();
    Task<decimal> GetVarianceViewCountAsync();
    Task<QuartilesDto> GetViewCountQuartilesAsync();
    Task<IEnumerable<CategoryStatsDto>> GetMaxViewsPerCategoryAsync();
}