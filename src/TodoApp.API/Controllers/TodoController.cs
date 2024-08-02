using Microsoft.AspNetCore.Mvc;
using Todo.Domain;
using Todo.Usecase.Todos;
using TodoApp.API.DTO.Todo.AddTodo;
using TodoApp.API.DTO.Todo.FindById;

namespace TodoApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController(IAddTodoUsecase addTodoUsecase,IFindByIdUsecase findByIdUsecase) : ControllerBase
{
    private readonly IAddTodoUsecase _addTodoUsecase = addTodoUsecase;
    private readonly IFindByIdUsecase _findByIdUsecase = findByIdUsecase;

    [HttpPost("AddTodo")]
    public async Task<ActionResult<AddTodoResponse>> AddAsync([FromBody] AddTodoRequest request)
    {

        //AddTodoUsecaseRequestに詰め替える
        AddTodoUsecaseRequest addTodoUsecaseRequest = new AddTodoUsecaseRequest();
        addTodoUsecaseRequest.Title = request.Title;
        addTodoUsecaseRequest.Description = request.Description;
        addTodoUsecaseRequest.ScheduleStartDate = request.ScheduleStartDate;
        addTodoUsecaseRequest.ScheduleEndDate = request.ScheduleEndDate;
        addTodoUsecaseRequest.AddTodoUsecaseItemRequests = request.TodoItemRequests.Select(x => new AddTodoUsecaseRequest.AddTodoUsecaseItemRequest
        {
            Title = x.Title,
            ScheduleStartDate = x.ScheduleStartDate,
            ScheduleEndDate = x.ScheduleEndDate
        }).ToArray();

        var response = await _addTodoUsecase.AddAsync(addTodoUsecaseRequest);

        //responseをAddTodoResponseに詰め替える
        AddTodoResponse addTodoResponse = new AddTodoResponse();
        addTodoResponse.TodoId = response.TodoId;
        addTodoResponse.Title = response.Title;
        addTodoResponse.Description = response.Description;
        addTodoResponse.ScheduleStartDate = response.ScheduleStartDate;
        addTodoResponse.ScheduleEndDate = response.ScheduleEndDate;
        addTodoResponse.TodoItemResponses = response.AddTodoUsecaseItemRequests.Select(x => new AddTodoResponse.TodoItemResponse
        {
            TodoItemId = x.TodoItemId,
            Title = x.Title,
            ScheduleStartDate = x.ScheduleStartDate,
            ScheduleEndDate = x.ScheduleEndDate
        }).ToArray();

        return Ok(addTodoResponse);
    }

    [HttpGet("FindById/{TodoId}")]
    public async Task<ActionResult<FindByIdResponse?>> FindByIdAsync(string TodoId)
    {
        var response = await _findByIdUsecase.ExecuteAsync(TodoId);

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
        findByIdResponse.TodoItemRequests = response.TodoItemRequests.Select(x => new FindByIdResponse.TodoItemResponse
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
