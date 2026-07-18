using Microsoft.Data.SqlClient;
using System.Data;

namespace StudentManagement.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

public IDbConnection CreateConnection()
{
    Console.WriteLine("=== Configuration Debug ===");
    Console.WriteLine("DefaultConnection = " + _configuration["ConnectionStrings:DefaultConnection"]);
    Console.WriteLine("===========================");

    return new SqlConnection(
        _configuration["ConnectionStrings:DefaultConnection"]);
}
}