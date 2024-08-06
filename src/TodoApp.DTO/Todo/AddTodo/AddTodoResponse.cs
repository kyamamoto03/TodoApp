﻿namespace TodoApp.API.DTO.Todo.AddTodo;

public class AddTodoResponse
{
    public string TodoId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime ScheduleStartDate { get; set; } = default!;
    public DateTime ScheduleEndDate { get; set; } = default!;

    public AddTodoItemResponse[] AddTodoItemResponses { get; set; } = default!;

    public record AddTodoItemResponse
    {
        public string TodoItemId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateTime ScheduleStartDate { get; set; } = default!;
        public DateTime ScheduleEndDate { get; set; } = default!;

        public DateTime? StartDate { get; set; } = default!;
        public DateTime? EndDate { get; set; } = default!;
    }

}