using Domain.Exceptions;
using Domain.TodoModel;
using Domain.UserModel;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.DTO.Todo.FindByUserId;
using TodoApp.Api.DTO.Todo.GetStatus;
using TodoApp.Api.DTO.Todo.StartTodo;
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

    public static async Task<FindByUserIdResponse> FindByUserIdAsync([FromBody] FindByUserIdRequest findByUserIdRequest,
        ITodoRepository _todoReposity,
        [AsParameters] ApiService apiService)
    {
        FindByUserIdResponse findByUserIdResponse = new FindByUserIdResponse();
        try
        {
            apiService.Logger.LogInformation("Start:FindByUserIdAsync");

            if (findByUserIdRequest.IsValid() == false)
            {
                findByUserIdResponse.Fail(findByUserIdRequest.validationResult.ToString());
                apiService.Logger.LogWarning(findByUserIdRequest.validationResult.ToString());

                return findByUserIdResponse;
            }

            var repositoryResponse = await _todoReposity.FindByUserIdAsync(findByUserIdRequest.UserId);

            //responseTodoをFindByUserIdResponseに詰め替える
            findByUserIdResponse.Todos = repositoryResponse.Select(x => new FindByUserIdResponse.Todo
            {
                UserId = x.UserId,
                TodoId = x.TodoId,
                Title = x.Title,
                Description = x.Description,
                ScheduleStartDate = x.ScheduleStartDate,
                ScheduleEndDate = x.ScheduleEndDate,
                TodoItemResponses = x.TodoItems.Select(y => new FindByUserIdResponse.Todo.TodoItemResponse
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

    public static async Task<AddTodoResponse> AddTodoAsync([FromBody] AddTodoRequest addTodoRequest,
        ITodoRepository _todoReposity,
        [AsParameters] ApiService apiService)
    {
        var addTodoResponse = new AddTodoResponse();

        try
        {
            apiService.Logger.LogInformation("Start:AddTodoAsync");
            if (addTodoRequest.IsValid() == false)
            {
                addTodoResponse.Fail(addTodoRequest.validationResult.ToString());
                apiService.Logger.LogWarning(addTodoRequest.validationResult.ToString());
            }
            else
            {
                //AddTodoUsecaseRequestに詰め替える
                Todo todo = Todo.Create(
                    addTodoRequest.UserId,
                    addTodoRequest.TodoId,
                    addTodoRequest.Title,
                    addTodoRequest.Description,
                    addTodoRequest.ScheduleStartDate,
                    addTodoRequest.ScheduleEndDate);

                foreach (var item in addTodoRequest.TodoItemRequests)
                {
                    TodoItem todoItem = Todo.CreateTodoItem(item.TodoItemId, item.Title, item.ScheduleStartDate, item.ScheduleEndDate);
                    todo.AddTodoItem(todoItem);
                }

                if (await _todoReposity.IsExistAsync(todo.TodoId))
                {
                    throw new TodoDoaminExceptioon("指定されたTodoは既に存在します");
                }

                var saveTodo = await _todoReposity.AddAsync(todo);
                await _todoReposity.UnitOfWork.SaveEntitiesAsync();

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

    public static async Task<FindByIdResponse> FindByIdAsync([FromBody] FindByIdRequest findByIdRequest,
        ITodoRepository _todoReposity,
        [AsParameters] ApiService apiService)
    {
        FindByIdResponse findByIdResponse = new FindByIdResponse();

        try
        {
            apiService.Logger.LogInformation("Start:FindByIdAsync");
            var responseTodo = await _todoReposity.FindByIdAsync(findByIdRequest.TodoId);

            //FindByIdResponseにresponseを詰め替える
            if (responseTodo == null)
            {
                findByIdResponse.Fail("Todoが見つかりませんでした");
                apiService.Logger.LogWarning("Todoが見つかりませんでした");

                return findByIdResponse;
            }
            findByIdResponse.UserId = responseTodo.UserId;
            findByIdResponse.TodoId = responseTodo.TodoId;
            findByIdResponse.Title = responseTodo.Title;
            findByIdResponse.Description = responseTodo.Description;
            findByIdResponse.ScheduleStartDate = responseTodo.ScheduleStartDate;
            findByIdResponse.ScheduleEndDate = responseTodo.ScheduleEndDate;
            findByIdResponse.TodoItemResponses = responseTodo.TodoItems.Select(x => new FindByIdResponse.TodoItemResponse
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

    public static async Task<StartTodoResponse> StartTodoAsync([FromBody] StartTodoRequest startTodoRequest,
        ITodoRepository _todoReposity,
        IUserRepository _userRepository,
        [AsParameters] ApiService apiService)
    {
        StartTodoResponse startTodoResponse = new StartTodoResponse();
        try
        {
            apiService.Logger.LogInformation("Start:StartTodoAsync");

            var todo = await _todoReposity.FindByItemIdAsync(startTodoRequest.TodoItemId);

            if (todo == null)
            {
                startTodoResponse.Fail("Todoが見つかりませんでした");
                apiService.Logger.LogWarning("Todoが見つかりませんでした");

                return startTodoResponse;
            }

            todo.StartTodoItem(startTodoRequest.TodoItemId, startTodoRequest.StartDate);

            //ユーザを開始状態にする
            var targetUser = await _userRepository.FindByIdAsync(todo.UserId);
            if (targetUser == null)
            {
                throw new TodoDoaminExceptioon("User not found");
            }
            targetUser.Start();

            await _todoReposity.UnitOfWork.SaveEntitiesAsync();

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

    public static async Task<GetStatusResponse> GetStatusAsync([FromBody] GetStatusRequest getStatusRequest,
        ITodoRepository _todoReposity,
        [AsParameters] ApiService apiService)
    {
        GetStatusResponse getStatusResponse = new GetStatusResponse();
        try
        {
            apiService.Logger.LogInformation("Start:StartTodoAsync");

            var todo = await _todoReposity.FindByIdAsync(getStatusRequest.TodoId);

            //todoがnullの場合、例外をスローする
            if (todo == null)
            {
                getStatusResponse.Fail("Todoが見つかりませんでした");
                apiService.Logger.LogWarning("Todoが見つかりませんでした");

                return getStatusResponse;
            }

            getStatusResponse.Status = todo.TodoItemStatus.Id;
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