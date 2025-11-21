using Domain.SeedOfWork;
using Domain.ValueObject;

namespace Domain.UserModel;

public class User : IModelBase
{
    public string UserId { get; private set; } = default!;

    public string UserName { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public bool IsStarted { get; private set; } = default!;
    public Address Address { get; private set; } = default!;


    public static User CreateNew(string userId, string userName, string email, Address address)
    {
        User User = new User();
        User.UserId = userId;
        User.UserName = userName;
        User.Email = email;
        User.Address = address;

        User.IsStarted = false;

        return User;
    }

    public void Start()
    {
        IsStarted = true;
    }
}