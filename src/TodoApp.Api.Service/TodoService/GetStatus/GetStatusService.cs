using Domain.Exceptions;
using Domain.TodoModel;

namespace TodoApp.Api.Service.TodoService.GetStatus;

public interface IGetStatusService
{
    Task<GetStatusResult> Execute(GetStatusCommand command);
}

public class GetStatusService(ITodoRepository todoReposity) : IGetStatusService
{
    private readonly ITodoRepository _todoReposity = todoReposity;
    public async Task<GetStatusResult> Execute(GetStatusCommand command)
    {
        var todo = await _todoReposity.FindByIdAsync(command.TodoId);

        //todoがnullの場合、例外をスローする
        if (todo == null)
        {
            throw new TodoDoaminExceptioon("Todo not found");
        }

        return new GetStatusResult(todo.TodoItemStatus.Id);
    }
}
