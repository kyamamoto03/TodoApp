using Domain.UserModel;
using Domain.ValueObject;

namespace Domain.Test.UserModel;

public class UserTest
{
    [Fact]
    public void Start_Test()
    {
        var userId = "U01";
        var userName = "ユーザ１";
        var email = "test@example.com";

        User user = User.CreateNew(userId, userName, email, "123-0123");

        Assert.False(user.IsStarted);

        user.Start();

        Assert.True(user.IsStarted);
    }
}