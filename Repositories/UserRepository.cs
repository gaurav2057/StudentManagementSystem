using Dapper;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public AppUser? GetByEmail(string email)
    {
        using var connection = _context.CreateConnection();

        return connection.QuerySingleOrDefault<AppUser>(
            "SELECT * FROM Users WHERE Email = @Email",
            new { Email = email });
    }

    public void Register(AppUser user)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(@"
            INSERT INTO Users
            (FullName, Email, PasswordHash, Role)
            VALUES
            (@FullName, @Email, @PasswordHash, @Role)", user);
    }
}
