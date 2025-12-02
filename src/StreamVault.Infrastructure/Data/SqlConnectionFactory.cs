using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using StreamVault.Application.Interfaces.Data;

namespace StreamVault.Infrastructure.Data;

public class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") 
                                                ?? throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}