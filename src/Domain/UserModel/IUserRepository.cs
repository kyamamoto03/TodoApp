using Domain.SeedOfWork;

namespace Domain.UserModel;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> FindByIdAsync(string userId);

    Task AddAsync(string userId, string userName, string email);

    Task UpdateAsync(User user);

    ValueTask<bool> IsExist(string email);
}
