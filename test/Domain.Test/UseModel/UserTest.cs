using Domain.UserModel;

namespace Domain.Test.UseModel;

public class UserTest
{
    [Fact]
    public void Start_Test()
    {
        var userId = "U01";
        var userName = "ユーザ１";
        var email = "test@example.com";

        User user = new User(userId, userName, email);

        Assert.False(user.IsStarted);

        user.Start();

        Assert.True(user.IsStarted);
    }
}
