using Microsoft.AspNetCore.Mvc;
using Todo.Usecase.Todos;
using TodoApp.API.DTO.Todo.AddTodo;
using TodoApp.API.DTO.Todo.FindById;

namespace TodoApp.API.APIs;

public static class TodoApi
{
    public static RouteGroupBuilder MapTodoApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/todo");

        api.MapPost("/AddTodo", AddTodoAsync);
        api.MapGet("/FindById/{TodoId}", FindByIdAsync);

        return api;
    }

    public static async Task<AddTodoResponse> AddTodoAsync([FromBody] AddTodoRequest request, IAddTodoUsecase addTodoUsecase)
    {

        //AddTodoUsecaseRequestに詰め替える
        AddTodoUsecaseRequest addTodoUsecaseRequest = new AddTodoUsecaseRequest();
        addTodoUsecaseRequest.TodoId = request.TodoId;
        addTodoUsecaseRequest.Title = request.Title;
        addTodoUsecaseRequest.Description = request.Description;
        addTodoUsecaseRequest.ScheduleStartDate = request.ScheduleStartDate;
        addTodoUsecaseRequest.ScheduleEndDate = request.ScheduleEndDate;
        addTodoUsecaseRequest.AddTodoUsecaseItemRequests = request.TodoItemRequests.Select(x => new AddTodoUsecaseRequest.AddTodoUsecaseItemRequest
        {
            TodoItemId = x.TodoItemId,
            Title = x.Title,
            ScheduleStartDate = x.ScheduleStartDate,
            ScheduleEndDate = x.ScheduleEndDate
        }).ToArray();

        var response = await addTodoUsecase.AddAsync(addTodoUsecaseRequest);

        //responseをAddTodoResponseに詰め替える
        AddTodoResponse addTodoResponse = new AddTodoResponse();
        addTodoResponse.TodoId = response.TodoId;
        addTodoResponse.Title = response.Title;
        addTodoResponse.Description = response.Description;
        addTodoResponse.ScheduleStartDate = response.ScheduleStartDate;
        addTodoResponse.ScheduleEndDate = response.ScheduleEndDate;
        addTodoResponse.AddTodoItemResponses = response.AddTodoUsecaseItemRequests.Select(x => new AddTodoResponse.AddTodoItemResponse
        {
            TodoItemId = x.TodoItemId,
            Title = x.Title,
            ScheduleStartDate = x.ScheduleStartDate,
            ScheduleEndDate = x.ScheduleEndDate
        }).ToArray();

        return addTodoResponse;
    }
    public static async Task<FindByIdResponse?> FindByIdAsync(string TodoId, IFindByIdUsecase findByIdUsecase)
    {
        var response = await findByIdUsecase.ExecuteAsync(TodoId);

        //FindByIdResponseにresponseを詰め替える
        if (response == null)
        {
            return null;
        }
        FindByIdResponse findByIdResponse = new FindByIdResponse();
        findByIdResponse.TodoId = response.TodoId;
        findByIdResponse.Title = response.Title;
        findByIdResponse.Description = response.Description;
        findByIdResponse.ScheduleStartDate = response.ScheduleStartDate;
        findByIdResponse.ScheduleEndDate = response.ScheduleEndDate;
        findByIdResponse.FindByIdTodoItemResponses = response.TodoItemRequests.Select(x => new FindByIdResponse.FindByIdTodoItemResponse
        {
            TodoItemId = x.TodoItemId,
            Title = x.Title,
            ScheduleStartDate = x.ScheduleStartDate,
            ScheduleEndDate = x.ScheduleEndDate,
            StartDate = x.StartDate,
            EndDate = x.EndDate
        }).ToArray();
        return findByIdResponse;
    }

}
