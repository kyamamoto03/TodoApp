using Domain.TodoModel;

namespace TodoApp.Api.Usecase.TodoUsecase.StartTodo;

public interface IStartTodoUsecase
{
    Task<StartTodoResult> ExecuteAsync(StartTodoCommand startTodoCommand);
}


public class StartTodoUsecase(ITodoRepository todoReposity) : IStartTodoUsecase
{
    private readonly ITodoRepository _todoReposity = todoReposity;
    public async Task<StartTodoResult> ExecuteAsync(StartTodoCommand startTodoCommand)
    {
        var todo = await _todoReposity.FindByIdAsync(startTodoCommand.TodoId);

        if (todo == null)
        {
            throw new Exception("Todoが見つかりませんでした");
        }

        todo.StartTodoItem(startTodoCommand.TodoItemId, startTodoCommand.StartDate);

        await _todoReposity.UpdateAsync(todo);
        await _todoReposity.UnitOfWork.SaveEntitiesAsync();

        return new StartTodoResult();
    }
}
