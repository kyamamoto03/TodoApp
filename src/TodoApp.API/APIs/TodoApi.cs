using Microsoft.AspNetCore.Mvc;
using Todo.Usecase.Todos;
using TodoApp.API.DTO;
using TodoApp.API.DTO.Todo.AddTodo;
using TodoApp.API.DTO.Todo.FindById;
using FluentValidation;
using FluentValidation.Results;

namespace TodoApp.API.APIs;

public static class TodoApi
{
    public static RouteGroupBuilder MapTodoApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/todo");

        api.MapPost("/AddTodo", AddTodoAsync);
        api.MapPost("/FindById", FindByIdAsync);
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
        findByIdResponse.TodoItemResponses = response.TodoItemRequests.Select(x => new FindByIdResponse.TodoItemResponse
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

}
