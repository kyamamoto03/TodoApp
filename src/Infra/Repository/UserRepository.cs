using Domain.SeedOfWork;
using Domain.UserModel;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class UserRepository(TodoDbContext todoMemDbContext) : IUserRepository
{
    private readonly TodoDbContext _todoDbContext = todoMemDbContext;

    public IUnitOfWork UnitOfWork => _todoDbContext;

    public Task AddAsync(string userId, string userName, string email)
    {
        User user = new(userId,userName,email);

        _todoDbContext.Users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> FindByIdAsync(string userId)
    {
        return _todoDbContext.Users.SingleOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _todoDbContext.Users.ToArrayAsync();
    }

    public async ValueTask<bool> IsExist(string email)
    {
        var user = await _todoDbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            return false;
        }
        return true;
    }

    public Task UpdateAsync(User user)
    {
        _todoDbContext.Entry(user).State = EntityState.Modified;
        return Task.CompletedTask;
    }
}
