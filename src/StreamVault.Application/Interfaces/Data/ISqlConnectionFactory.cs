using System.Data;

namespace StreamVault.Application.Interfaces.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}