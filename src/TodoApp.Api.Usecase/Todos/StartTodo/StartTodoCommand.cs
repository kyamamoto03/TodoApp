namespace TodoApp.Api.Usecase.Todos.StartTodo;

public record StartTodoCommand(string TodoId,string TodoItemId,DateTime StartDate);
