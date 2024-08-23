namespace TodoApp.Api.Usecase.User.Add;

public record AddCommand(string UserId, string UserName, string Email);
