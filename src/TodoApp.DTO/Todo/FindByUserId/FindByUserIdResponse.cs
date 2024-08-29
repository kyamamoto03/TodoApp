﻿using TodoApp.API.DTO;

namespace TodoApp.Api.DTO.Todo.FindByUserId;

public class FindByUserIdResponse : IResponseBase
{
    public Todo[] Todos { get; set; } = default!;

    public class Todo
    {
        public string UserId { get; set; } = default!;
        public string TodoId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime ScheduleStartDate { get; set; } = default!;
        public DateTime ScheduleEndDate { get; set; } = default!;

        public TodoItemResponse[] TodoItemResponses { get; set; } = default!;

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
}