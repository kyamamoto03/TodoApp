namespace TodoApp.Api.Usecase.User.GetAll;

public class GetAllResult
{
    public IEnumerable<User> Users { get; set; } = [];
    public class User
    {         
        public string UserId { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
