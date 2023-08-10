
using System.Data;
using System.Data.SqlClient;

namespace EmployeePostTrace.DataLayer;

public class BaseRepository
{
    private readonly IDbConnection _connection;

    public BaseRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    protected IDbConnection _connectionString => new SqlConnection(_connection.ConnectionString); 
}
