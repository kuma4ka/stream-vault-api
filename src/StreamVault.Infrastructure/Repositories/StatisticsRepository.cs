using Dapper;
using StreamVault.Application.DTOs.Statistics;
using StreamVault.Application.Interfaces.Data;
using StreamVault.Application.Interfaces.Repositories;
using StreamVault.Infrastructure.Sql;

namespace StreamVault.Infrastructure.Repositories;

public class StatisticsRepository(ISqlConnectionFactory connectionFactory) : IStatisticsRepository
{
    public async Task<int> GetUserCountAsync()
    {
        using var connection = connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(SqlCommands.GetUserCount);
    }

    public async Task<long> GetMaxViewCountAsync()
    {
        using var connection = connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<long>(SqlCommands.GetMaxViewCount);
    }

    public async Task<decimal> GetVarianceViewCountAsync()
    {
        using var connection = connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<decimal>(SqlCommands.GetVarianceViewCount);
    }

    public async Task<QuartilesDto> GetViewCountQuartilesAsync()
    {
        using var connection = connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<QuartilesDto>(SqlCommands.GetViewCountQuartiles) 
               ?? new QuartilesDto(0, 0, 0);
    }

    public async Task<IEnumerable<CategoryStatsDto>> GetMaxViewsPerCategoryAsync()
    {
        using var connection = connectionFactory.CreateConnection();
        return await connection.QueryAsync<CategoryStatsDto>(SqlCommands.GetMaxViewsPerCategory);
    }
}