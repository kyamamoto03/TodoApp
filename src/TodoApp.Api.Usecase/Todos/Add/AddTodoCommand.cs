namespace TodoApp.Api.Usecase.Todos.Add;

public class AddTodoCommand
{
    public string UserId { get; set; } = default!;
    public string TodoId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime ScheduleStartDate { get; set; } = default!;
    public DateTime ScheduleEndDate { get; set; } = default!;
    public TodoItem[] TodoItems { get; set; } = default!;

    public class TodoItem
    {
        public string TodoItemId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateTime ScheduleStartDate { get; set; } = default!;
        public DateTime ScheduleEndDate { get; set; } = default!;
        public DateTime? StartDate { get; set; } = default!;
        public DateTime? EndDate { get; set; } = default!;
    }
}
