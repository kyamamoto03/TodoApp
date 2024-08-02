using Todo.Domain;

namespace Todo.Usecase.Todos;

public interface IFindByIdUsecase
{
    Task<FindByIdUsecaseResponse?> ExecuteAsync(string todoId);
}
public class FindByIdUsecase(ITodoReposity todoReposity) : IFindByIdUsecase
{
    private readonly ITodoReposity _todoReposity = todoReposity;

    public async Task<FindByIdUsecaseResponse?> ExecuteAsync(string todoId)
    {
        var responseTodo = await _todoReposity.FindByIdAsync(todoId);

        if (responseTodo == null)
        {
            return null;
        }
        //TodoをGetAllTodoUsecaseResponseに詰め替える
        FindByIdUsecaseResponse todo = new FindByIdUsecaseResponse();
        todo.TodoId = responseTodo.TodoId;
        todo.Title = responseTodo.Title;
        todo.Description = responseTodo.Description;
        todo.ScheduleStartDate = responseTodo.ScheduleStartDate;
        todo.ScheduleEndDate = responseTodo.ScheduleEndDate;
        todo.TodoItemRequests = responseTodo.TodoItems.Select(x => new FindByIdUsecaseResponse.TodoItemResponse
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


public class FindByIdUsecaseResponse
{
    public string TodoId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime ScheduleStartDate { get; set; } = default!;
    public DateTime ScheduleEndDate { get; set; } = default!;

    public TodoItemResponse[] TodoItemRequests { get; set; } = default!;

    public record TodoItemResponse
    {
        public string TodoItemId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateTime ScheduleStartDate { get; set; } = default!;
        public DateTime ScheduleEndDate { get; set; } = default!;

        public DateTime? StartDate { get; set; } = default!;
        public DateTime? EndDate { get; set; } = default!;
    }
}
