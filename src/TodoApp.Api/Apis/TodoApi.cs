using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTO.Todo.FindByUserId;
using TodoApp.Api.DTO.Todo.GetStatus;
using TodoApp.Api.DTO.Todo.StartTodo;
using TodoApp.Api.Service.TodoService.Add;
using TodoApp.Api.Service.TodoService.FindById;
using TodoApp.Api.Service.TodoService.FindByUserId;
using TodoApp.Api.Service.TodoService.GetStatus;
using TodoApp.Api.Service.TodoService.StartTodo;
using TodoApp.API.DTO.Todo.AddTodo;
using TodoApp.API.DTO.Todo.FindById;

namespace TodoApp.Api.Apis;

public static class TodoApi
{
    public static RouteGroupBuilder MapTodoApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/todo");

        api.MapPost("/AddTodo", AddTodoAsync);
        api.MapPost("/FindById", FindByIdAsync);
        api.MapPost("/StartTodo", StartTodoAsync);
        api.MapPost("/GetStatus", GetStatusAsync);
        api.MapPost("/FindByUserId", FindByUserIdAsync);
        return api;
    }

    private static async Task<FindByUserIdResponse> FindByUserIdAsync([FromBody] FindByUserIdRequest findByUserIdRequest, IFindByUserIdService findByUserIdService)
    {
        FindByUserIdResponse findByUserIdResponse = new FindByUserIdResponse();
        try
        {
            if (findByUserIdRequest.IsValid() == false)
            {
                findByUserIdResponse.Fail(findByUserIdRequest.validationResult.ToString());
                return findByUserIdResponse;
            }

            var response = await findByUserIdService.Execute(findByUserIdRequest.UserId);
            //responseをFindByUserIdResponseに詰め替える
            findByUserIdResponse.Todos = response.Select(x => new FindByUserIdResponse.Todo
            {
                UserId = x.UserId,
                TodoId = x.TodoId,
                Title = x.Title,
                Description = x.Description,
                ScheduleStartDate = x.ScheduleStartDate,
                ScheduleEndDate = x.ScheduleEndDate,
                TodoItemResponses = x.TodoItemResults.Select(y => new FindByUserIdResponse.Todo.TodoItemResponse
                {
                    TodoItemId = y.TodoItemId,
                    Title = y.Title,
                    ScheduleStartDate = y.ScheduleStartDate,
                    ScheduleEndDate = y.ScheduleEndDate,
                    StartDate = y.StartDate,
                    EndDate = y.EndDate
                }).ToArray()
            }).ToArray();
        }
        catch (TodoDoaminExceptioon tde)
        {
            findByUserIdResponse.Fail(tde.Message);
        }
        catch (Exception ex)
        {
            findByUserIdResponse.Fail(ex.Message);
        }

        return findByUserIdResponse;
    }

    public static async Task<AddTodoResponse> AddTodoAsync([FromBody] AddTodoRequest request, IAddTodoService addTodoService)
    {
        var addTodoResponse = new AddTodoResponse();

        try
        {
            if (request.IsValid() == false)
            {
                addTodoResponse.Fail(request.validationResult.ToString());
            }
            else
            {
                //AddTodoUsecaseRequestに詰め替える
                AddTodoCommand addTodoUsecaseRequest = new AddTodoCommand();
                addTodoUsecaseRequest.UserId = request.UserId;
                addTodoUsecaseRequest.TodoId = request.TodoId;
                addTodoUsecaseRequest.Title = request.Title;
                addTodoUsecaseRequest.Description = request.Description;
                addTodoUsecaseRequest.ScheduleStartDate = request.ScheduleStartDate;
                addTodoUsecaseRequest.ScheduleEndDate = request.ScheduleEndDate;
                addTodoUsecaseRequest.TodoItems = request.TodoItemRequests.Select(x => new AddTodoCommand.TodoItem
                {
                    TodoItemId = x.TodoItemId,
                    Title = x.Title,
                    ScheduleStartDate = x.ScheduleStartDate,
                    ScheduleEndDate = x.ScheduleEndDate
                }).ToArray();

                await addTodoService.ExecuteAsync(addTodoUsecaseRequest);
                addTodoResponse.Success();
            }
        }
        catch (TodoDoaminExceptioon tde)
        {
            addTodoResponse.Fail(tde.Message);
        }
        catch (Exception ex)
        {
            addTodoResponse.Fail(ex.Message);
        }

        return addTodoResponse;
    }

    public static async Task<FindByIdResponse> FindByIdAsync([FromBody] FindByIdRequest findByIdRequest, IFindByIdService findByIdService)
    {
        FindByIdResponse findByIdResponse = new FindByIdResponse();

        try
        {
            var response = await findByIdService.ExecuteAsync(findByIdRequest.TodoId);
            //FindByIdResponseにresponseを詰め替える
            if (response == null)
            {
                findByIdResponse.Fail("Todoが見つかりませんでした");
                return findByIdResponse;
            }
            findByIdResponse.UserId = response.UserId;
            findByIdResponse.TodoId = response.TodoId;
            findByIdResponse.Title = response.Title;
            findByIdResponse.Description = response.Description;
            findByIdResponse.ScheduleStartDate = response.ScheduleStartDate;
            findByIdResponse.ScheduleEndDate = response.ScheduleEndDate;
            findByIdResponse.TodoItemResponses = response.TodoItemResults.Select(x => new FindByIdResponse.TodoItemResponse
            {
                TodoItemId = x.TodoItemId,
                Title = x.Title,
                ScheduleStartDate = x.ScheduleStartDate,
                ScheduleEndDate = x.ScheduleEndDate,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            }).ToArray();
            findByIdResponse.Success();
        }
        catch (TodoDoaminExceptioon tde)
        {
            findByIdResponse.Fail(tde.Message);
        }
        catch (Exception ex)
        {
            findByIdResponse.Fail(ex.Message);
        }
        return findByIdResponse;
    }

    public static async Task<StartTodoResponse> StartTodoAsync([FromBody] StartTodoRequest request, IStartTodoService startTodoService)
    {
        StartTodoResponse startTodoResponse = new StartTodoResponse();
        try
        {
            await startTodoService.ExecuteAsync(new StartTodoCommand(request.TodoId, request.TodoItemId, request.StartDate));
            startTodoResponse.Success();
        }
        catch (TodoDoaminExceptioon tde)
        {
            startTodoResponse.Fail(tde.Message);
        }
        catch (Exception ex)
        {
            startTodoResponse.Fail(ex.Message);
        }

        return startTodoResponse;
    }

    public static async Task<GetStatusResponse> GetStatusAsync([FromBody] GetStatusRequest request, IGetStatusService getStatusService)
    {
        GetStatusResponse getStatusResponse = new GetStatusResponse();
        try
        {
            var result = await getStatusService.Execute(new GetStatusCommand(request.TodoId));
            getStatusResponse.Status = result.Status;
            getStatusResponse.Success();
        }
        catch (TodoDoaminExceptioon tde)
        {
            getStatusResponse.Fail(tde.Message);
        }
        catch (Exception ex)
        {
            getStatusResponse.Fail(ex.Message);
        }

        return getStatusResponse;
    }
}