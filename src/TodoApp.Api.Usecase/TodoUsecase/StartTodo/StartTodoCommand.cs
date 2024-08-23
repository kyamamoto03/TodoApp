namespace TodoApp.Api.Usecase.TodoUsecase.StartTodo;

public record StartTodoCommand(string TodoId, string TodoItemId, DateTime StartDate);
