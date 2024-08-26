namespace TodoApp.Api.Service.TodoService.StartTodo;

public record StartTodoCommand(string TodoId, string TodoItemId, DateTime StartDate);
