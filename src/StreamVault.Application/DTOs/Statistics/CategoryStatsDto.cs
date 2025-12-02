namespace StreamVault.Application.DTOs.Statistics;

public record CategoryStatsDto(string CategoryName, long MaxViews);

public record QuartilesDto(double Q1, double Q2, double Q3);

public record GeneralStatsDto(
    int UserCount, 
    long MaxViewCount, 
    decimal VarianceViewCount
);