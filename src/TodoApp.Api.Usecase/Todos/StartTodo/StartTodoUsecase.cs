using Domain.TodoModel;

namespace TodoApp.Api.Usecase.Todos.StartTodo;

public interface IStartTodoUsecase
{
    Task<StartTodoResult> ExecuteAsync(StartTodoCommand startTodoCommand);
}


public class StartTodoUsecase(ITodoReposity todoReposity) : IStartTodoUsecase
{
    private readonly ITodoReposity _todoReposity = todoReposity;
    public async Task<StartTodoResult> ExecuteAsync(StartTodoCommand startTodoCommand)
    {
        var todo = await _todoReposity.FindByIdAsync(startTodoCommand.TodoId);

        if (todo == null)
        {
            throw new Exception("Todoが見つかりませんでした");
        }

        todo.StartTodoItem(startTodoCommand.TodoItemId, startTodoCommand.StartDate);

        await _todoReposity.UpdateAsync(todo);

        return new StartTodoResult();
    }
}
