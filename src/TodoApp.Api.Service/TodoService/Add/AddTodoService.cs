
using Domain.Exceptions;
using Domain.TodoModel;

namespace TodoApp.Api.Service.TodoService.Add;

public interface IAddTodoService
{
    Task ExecuteAsync(AddTodoCommand addTodoServiceRequest);
}


public class AddTodoService(ITodoRepository todoReposity) : IAddTodoService
{
    private readonly ITodoRepository _todoReposity = todoReposity;

    public async Task ExecuteAsync(AddTodoCommand addTodoServiceRequest)
    {
        Todo todo = Todo.Create(
            addTodoServiceRequest.UserId,
            addTodoServiceRequest.TodoId,
            addTodoServiceRequest.Title,
            addTodoServiceRequest.Description,
            addTodoServiceRequest.ScheduleStartDate,
            addTodoServiceRequest.ScheduleEndDate);

        foreach (var item in addTodoServiceRequest.TodoItems)
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
    }
}

