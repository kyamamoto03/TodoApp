using Todo.Domain;

namespace Todo.Usecase.Todos;

public interface IAddTodoUsecase
{
    Task<AddTodoUsecaseReponse> AddAsync(AddTodoUsecaseRequest addTodoUsecaseRequest);
}
public class AddTodoUsecase(ITodoReposity todoReposity) : IAddTodoUsecase
{
    private readonly ITodoReposity _todoReposity = todoReposity;

    public async Task<AddTodoUsecaseReponse> AddAsync(AddTodoUsecaseRequest addTodoUsecaseRequest)
    {
        Domain.Todo todo = Domain.Todo.Create(
            addTodoUsecaseRequest.TodoId,
            addTodoUsecaseRequest.Title, 
            addTodoUsecaseRequest.Description, 
            addTodoUsecaseRequest.ScheduleStartDate, 
            addTodoUsecaseRequest.ScheduleEndDate);

        foreach (var item in addTodoUsecaseRequest.AddTodoUsecaseItemRequests)
        {
            TodoItem todoItem = Domain.Todo.CreateNewTodoItem(item.Title, item.ScheduleStartDate, item.ScheduleEndDate);
            todo.AddTodoItem(todoItem);
        }

        var saveTodo = await _todoReposity.SaveAsync(todo);

        //saveTodoをAddTodoUsecaseReponseに詰め替える
        AddTodoUsecaseReponse addTodoUsecaseReponse = new AddTodoUsecaseReponse();
        addTodoUsecaseReponse.TodoId = saveTodo.TodoId;
        addTodoUsecaseReponse.Title = saveTodo.Title;
        addTodoUsecaseReponse.Description = saveTodo.Description;
        addTodoUsecaseReponse.ScheduleStartDate = saveTodo.ScheduleStartDate;
        addTodoUsecaseReponse.ScheduleEndDate = saveTodo.ScheduleEndDate;
        addTodoUsecaseReponse.AddTodoUsecaseItemRequests = saveTodo.TodoItems.Select(x => new AddTodoUsecaseReponse.AddTodoUsecaseItemRequest
        {
            TodoItemId = x.TodoItemId,
            Title = x.Title,
            ScheduleStartDate = x.ScheduleStartDate,
            ScheduleEndDate = x.ScheduleEndDate
        }).ToArray();

        return addTodoUsecaseReponse;
    }
}

public class AddTodoUsecaseRequest
{
    public string TodoId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime ScheduleStartDate { get; set; } = default!;
    public DateTime ScheduleEndDate { get; set; } = default!;
    public AddTodoUsecaseItemRequest[] AddTodoUsecaseItemRequests { get; set; } = default!;

    public class AddTodoUsecaseItemRequest
    {
        public string TodoItemId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateTime ScheduleStartDate { get; set; } = default!;
        public DateTime ScheduleEndDate { get; set; } = default!;
    }
}

public class AddTodoUsecaseReponse
{
    public string TodoId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime ScheduleStartDate { get; set; } = default!;
    public DateTime ScheduleEndDate { get; set; } = default!;
    public AddTodoUsecaseItemRequest[] AddTodoUsecaseItemRequests { get; set; } = default!;

    public class AddTodoUsecaseItemRequest
    {
        public string TodoItemId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateTime ScheduleStartDate { get; set; } = default!;
        public DateTime ScheduleEndDate { get; set; } = default!;
    }
}
