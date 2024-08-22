
using Domain.TodoModel;

namespace TodoApp.Api.Usecase.Todos.Add;

public interface IAddTodoUsecase
{
    Task ExecuteAsync(AddTodoCommand addTodoUsecaseRequest);
}


public class AddTodoUsecase(ITodoReposity todoReposity) : IAddTodoUsecase
{
    private readonly ITodoReposity _todoReposity = todoReposity;

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
            TodoItem todoItem = Todo.CreateTodoItem(item.TodoItemId,item.Title, item.ScheduleStartDate, item.ScheduleEndDate);
            todo.AddTodoItem(todoItem);
        }

        var saveTodo = await _todoReposity.AddAsync(todo);
        await _todoReposity.UnitOfWork.SaveEntitiesAsync();
    }
}

