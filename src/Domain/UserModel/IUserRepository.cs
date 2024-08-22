using Domain.SeedOfWork;

namespace Domain.UserModel;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> FindByIdAsync(string userId);
}
