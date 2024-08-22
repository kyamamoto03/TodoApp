namespace Domain.UserModel;

public class User
{
    public string UserId { get; private set; } = default!;

    public string UserName { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public User(string userId, string userName, string email)
    {
        UserId = userId;
        UserName = userName;
        Email = email;
    }
}
