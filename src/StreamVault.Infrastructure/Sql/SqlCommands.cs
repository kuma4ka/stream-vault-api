namespace StreamVault.Infrastructure.Sql;

public static class SqlCommands
{
    public const string GetUserCount = @"SELECT COUNT(""Id"") FROM ""AspNetUsers"";";

    public const string GetMaxViewCount = @"SELECT MAX(""ViewCount"") FROM ""Broadcasts"";";

    public const string GetVarianceViewCount = @"SELECT VARIANCE(""ViewCount"") FROM ""Broadcasts"";";

    public const string GetViewCountQuartiles = @"
        WITH percentiles AS (
            SELECT
                percentile_cont(0.25) WITHIN GROUP (ORDER BY ""ViewCount"") AS Q1,
                percentile_cont(0.50) WITHIN GROUP (ORDER BY ""ViewCount"") AS Q2,
                percentile_cont(0.75) WITHIN GROUP (ORDER BY ""ViewCount"") AS Q3
            FROM ""Broadcasts""
        )
        SELECT Q1, Q2, Q3 FROM percentiles;";

    public const string GetMaxViewsPerCategory = @"
        SELECT 
            c.""Name"" AS CategoryName,
            MAX(b.""ViewCount"") AS MaxViews
        FROM ""Broadcasts"" b
        JOIN ""BroadcastCategories"" bc ON b.""Id"" = bc.""BroadcastId""
        JOIN ""Categories"" c ON bc.""CategoryId"" = c.""Id""
        GROUP BY c.""Name""
        ORDER BY MaxViews DESC;";
}