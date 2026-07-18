using StudentManagement.Models;

namespace StudentManagement.Repositories;

public interface IUserRepository
{
    AppUser? GetByEmail(string email);

    void Register(AppUser user);
}