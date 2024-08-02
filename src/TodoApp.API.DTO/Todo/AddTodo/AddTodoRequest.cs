namespace TodoApp.API.DTO.Todo.AddTodo;

public record AddTodoRequest
{
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public DateTime ScheduleStartDate { get; init; } = default!;
    public DateTime ScheduleEndDate { get; init; } = default!;

    public TodoItemRequest[] TodoItemRequests { get; init; } = default!;

    public record TodoItemRequest
    {
        public string Title { get; init; } = default!;
        public DateTime ScheduleStartDate { get; init; } = default!;
        public DateTime ScheduleEndDate { get; init; } = default!;

        public DateTime StartDate { get; init; } = default!;
        public DateTime EndDate { get; init; } = default!;
    }
}
