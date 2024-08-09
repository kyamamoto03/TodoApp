using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTO.Todo.GetStatus;
using TodoApp.Api.DTO.Todo.StartTodo;
using TodoApp.Api.Usecase.Todos.Add;
using TodoApp.Api.Usecase.Todos.FindById;
using TodoApp.Api.Usecase.Todos.GetStatus;
using TodoApp.Api.Usecase.Todos.StartTodo;
using TodoApp.API.DTO;
using TodoApp.API.DTO.Todo.AddTodo;
using TodoApp.API.DTO.Todo.FindById;

namespace TodoApp.API.Apis;

public static class TodoApi
{
    public static RouteGroupBuilder MapTodoApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/todo");

        api.MapPost("/AddTodo", AddTodoAsync);
        api.MapPost("/FindById", FindByIdAsync);
        api.MapPost("/StartTodo", StartTodoAsync);
        api.MapPost("/GetStatus", GetStatusAsync);
        api.MapPost("/test", Test);

        return api;
    }

    public static TestResponse Test([FromBody] TestRequest request)
    {
        TestResponse testResponse = new TestResponse();
        if (!request.IsValid())
        {
            testResponse.Fail(request.validationResult);

        }
        else
        {
            testResponse.Success();
        }
        return testResponse;
    }

    public class TestRequest : IRequestBase
    {
        public string TestId { get; set; }

        public int Value { get; set; }

        public override bool IsValid()
        {
            var validator = new InlineValidator<TestRequest>
            {
                v => v.RuleFor(x => x.TestId).NotEmpty(),
                v => v.RuleFor(x => x.Value).GreaterThan(10)

            };
            validationResult = validator.Validate(this);

            return validationResult.IsValid;

        }
    }
    public class TestResponse : IResponseBase
    {
        public string TestId { get; set; }
        public int Value { get; set; }

    }
    public static async Task<AddTodoResponse> AddTodoAsync([FromBody] AddTodoRequest request, IAddTodoUsecase addTodoUsecase)
    {

        //AddTodoUsecaseRequestに詰め替える
        AddTodoCommand addTodoUsecaseRequest = new AddTodoCommand();
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

        var response = await addTodoUsecase.AddAsync(addTodoUsecaseRequest);

        //responseをAddTodoResponseに詰め替える
        AddTodoResponse addTodoResponse = new AddTodoResponse();
        addTodoResponse.TodoId = response.TodoId;
        addTodoResponse.Title = response.Title;
        addTodoResponse.Description = response.Description;
        addTodoResponse.ScheduleStartDate = response.ScheduleStartDate;
        addTodoResponse.ScheduleEndDate = response.ScheduleEndDate;
        addTodoResponse.AddTodoItemResponses = response.TodoItems.Select(x => new AddTodoResponse.AddTodoItemResponse
        {
            TodoItemId = x.TodoItemId,
            Title = x.Title,
            ScheduleStartDate = x.ScheduleStartDate,
            ScheduleEndDate = x.ScheduleEndDate
        }).ToArray();

        return addTodoResponse;
    }
    public static async Task<FindByIdResponse> FindByIdAsync([FromBody] FindByIdRequest findByIdRequest, IFindByIdUsecase findByIdUsecase)
    {
        var response = await findByIdUsecase.ExecuteAsync(findByIdRequest.TodoId);
        FindByIdResponse findByIdResponse = new FindByIdResponse();
        //FindByIdResponseにresponseを詰め替える
        if (response == null)
        {
            findByIdResponse.Fail("Todoが見つかりませんでした");
            return findByIdResponse;
        }
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

        return findByIdResponse;
    }

    public static async Task<StartTodoResponse> StartTodoAsync([FromBody] StartTodoRequest request, IStartTodoUsecase startTodoUsecase)
    {
        StartTodoResponse response = new StartTodoResponse();
        try
        {
            await startTodoUsecase.ExecuteAsync(new StartTodoCommand(request.TodoId,request.TodoItemId, request.StartDate));
            response.Success();
        }
        catch (Exception ex)
        {
            response.Fail(ex.ToString());
        }

        return response;
    }

    public static async Task<GetStatusResponse> GetStatusAsync([FromBody] GetStatusRequest request,IGetStatusUsecase getStatusUsecase)
    {
        GetStatusResponse response = new GetStatusResponse();
        try
        {
            var result = await getStatusUsecase.Execute(new GetStatusCommand(request.TodoId));
            response.Status = result.Status;
            response.Success();
        }
        catch (Exception ex)
        {
            response.Fail(ex.ToString());
        }

        return response;
    }
}
