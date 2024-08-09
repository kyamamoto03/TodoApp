﻿namespace TodoApp.API.DTO.Todo.AddTodo;

public record AddTodoRequest
{
    public string TodoId { get; init; } = default!;
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public DateTime ScheduleStartDate { get; init; } = default!;
    public DateTime ScheduleEndDate { get; init; } = default!;

    public TodoItemRequest[] TodoItemRequests { get; init; } = default!;

    public record TodoItemRequest
    {
        public string TodoItemId { get; init; }
        public string Title { get; init; } = default!;
        public DateTime ScheduleStartDate { get; init; } = default!;
        public DateTime ScheduleEndDate { get; init; } = default!;
    }
}
