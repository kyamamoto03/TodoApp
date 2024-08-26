
using Domain.TodoModel;

namespace TodoApp.Api.Service.TodoService.FindById;

public interface IFindByIdService
{
    Task<FindByIdResult?> ExecuteAsync(string todoId);
}
public class FindByIdService(ITodoRepository todoReposity) : IFindByIdService
{
    private readonly ITodoRepository _todoReposity = todoReposity;

    public async Task<FindByIdResult?> ExecuteAsync(string todoId)
    {
        var responseTodo = await _todoReposity.FindByIdAsync(todoId);

        if (responseTodo == null)
        {
            return null;
        }
        //TodoをGetAllTodoServiceResponseに詰め替える
        FindByIdResult todo = new FindByIdResult();
        todo.UserId = responseTodo.UserId;
        todo.TodoId = responseTodo.TodoId;
        todo.Title = responseTodo.Title;
        todo.Description = responseTodo.Description;
        todo.ScheduleStartDate = responseTodo.ScheduleStartDate;
        todo.ScheduleEndDate = responseTodo.ScheduleEndDate;
        todo.TodoItemResults = responseTodo.TodoItems.Select(x => new FindByIdResult.TodoItemResult
        {
            TodoItemId = x.TodoItemId,
            Title = x.Title,
            ScheduleStartDate = x.ScheduleStartDate,
            ScheduleEndDate = x.ScheduleEndDate,
            StartDate = x.StartDate,
            EndDate = x.EndDate
        }).ToArray();

        return todo;
    }
}


