namespace TodoApp.Api.Service.UserService.GetAll;

public class GetAllResult
{
    public IEnumerable<User> Users { get; set; } = [];
    public class User
    {
        public string UserId { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsStarted { get; set; } = default!;
    }
}
