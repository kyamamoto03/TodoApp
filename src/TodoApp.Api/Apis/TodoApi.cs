using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTO.Todo.GetStatus;
using TodoApp.Api.DTO.Todo.StartTodo;
using TodoApp.Api.Usecase.TodoUsecase.Add;
using TodoApp.Api.Usecase.TodoUsecase.FindById;
using TodoApp.Api.Usecase.TodoUsecase.GetStatus;
using TodoApp.Api.Usecase.TodoUsecase.StartTodo;
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

        return api;
    }

    public static async Task<AddTodoResponse> AddTodoAsync([FromBody] AddTodoRequest request, IAddTodoUsecase addTodoUsecase)
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

                await addTodoUsecase.ExecuteAsync(addTodoUsecaseRequest);
                addTodoResponse.Success();
            }
        }
        catch(TodoDoaminExceptioon tde)
        {
            addTodoResponse.Fail(tde.Message);
        }
        catch (Exception ex)
        {
            addTodoResponse.Fail(ex.Message);
        }

        return addTodoResponse;
    }
    public static async Task<FindByIdResponse> FindByIdAsync([FromBody] FindByIdRequest findByIdRequest, IFindByIdUsecase findByIdUsecase)
    {
        FindByIdResponse findByIdResponse = new FindByIdResponse();

        try
        {
            var response = await findByIdUsecase.ExecuteAsync(findByIdRequest.TodoId);
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

    public static async Task<StartTodoResponse> StartTodoAsync([FromBody] StartTodoRequest request, IStartTodoUsecase startTodoUsecase)
    {
        StartTodoResponse startTodoResponse = new StartTodoResponse();
        try
        {
            await startTodoUsecase.ExecuteAsync(new StartTodoCommand(request.TodoId, request.TodoItemId, request.StartDate));
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

    public static async Task<GetStatusResponse> GetStatusAsync([FromBody] GetStatusRequest request, IGetStatusUsecase getStatusUsecase)
    {
        GetStatusResponse getStatusResponse = new GetStatusResponse();
        try
        {
            var result = await getStatusUsecase.Execute(new GetStatusCommand(request.TodoId));
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
