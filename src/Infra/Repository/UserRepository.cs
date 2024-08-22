using Domain.SeedOfWork;
using Domain.UserModel;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class UserRepository(TodoDbContext todoMemDbContext) : IUserRepository
{
    private readonly TodoDbContext _todoDbContext = todoMemDbContext;

    public IUnitOfWork UnitOfWork => _todoDbContext;

    public Task<User?> FindByIdAsync(string userId)
    {
        return _todoDbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _todoDbContext.Users.ToArrayAsync();
    }

}
