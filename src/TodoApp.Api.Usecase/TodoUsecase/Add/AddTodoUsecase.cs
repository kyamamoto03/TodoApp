
using Domain.Exceptions;
using Domain.TodoModel;

namespace TodoApp.Api.Usecase.TodoUsecase.Add;

public interface IAddTodoUsecase
{
    Task ExecuteAsync(AddTodoCommand addTodoUsecaseRequest);
}


public class AddTodoUsecase(ITodoRepository todoReposity) : IAddTodoUsecase
{
    private readonly ITodoRepository _todoReposity = todoReposity;

    public async Task ExecuteAsync(AddTodoCommand addTodoUsecaseRequest)
    {
        Todo todo = Todo.Create(
            addTodoUsecaseRequest.UserId,
            addTodoUsecaseRequest.TodoId,
            addTodoUsecaseRequest.Title,
            addTodoUsecaseRequest.Description,
            addTodoUsecaseRequest.ScheduleStartDate,
            addTodoUsecaseRequest.ScheduleEndDate);

        foreach (var item in addTodoUsecaseRequest.TodoItems)
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

