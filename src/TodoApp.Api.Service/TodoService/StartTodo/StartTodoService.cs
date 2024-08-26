using Domain.Exceptions;
using Domain.TodoModel;

namespace TodoApp.Api.Service.TodoService.StartTodo;

public interface IStartTodoService
{
    Task<StartTodoResult> ExecuteAsync(StartTodoCommand startTodoCommand);
}


public class StartTodoService(ITodoRepository todoReposity) : IStartTodoService
{
    private readonly ITodoRepository _todoReposity = todoReposity;
    public async Task<StartTodoResult> ExecuteAsync(StartTodoCommand startTodoCommand)
    {
        var todo = await _todoReposity.FindByIdAsync(startTodoCommand.TodoId);

        if (todo == null)
        {
            throw new TodoDoaminExceptioon("Todoが見つかりませんでした");
        }

        todo.StartTodoItem(startTodoCommand.TodoItemId, startTodoCommand.StartDate);

        await _todoReposity.UpdateAsync(todo);
        await _todoReposity.UnitOfWork.SaveEntitiesAsync();

        return new StartTodoResult();
    }
}
