
using Domain.Todos;

namespace TodoApp.Api.Usecase.Todos.Add;

public interface IAddTodoUsecase
{
    Task<AddTodoResult> AddAsync(AddTodoCommand addTodoUsecaseRequest);
}


public class AddTodoUsecase(ITodoReposity todoReposity) : IAddTodoUsecase
{
    private readonly ITodoReposity _todoReposity = todoReposity;

    public async Task<AddTodoResult> AddAsync(AddTodoCommand addTodoUsecaseRequest)
    {
        Todo todo = Todo.Create(
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

        //saveTodoをAddTodoUsecaseReponseに詰め替える
        AddTodoResult addTodoUsecaseReponse = new AddTodoResult();
        addTodoUsecaseReponse.TodoId = saveTodo.TodoId;
        addTodoUsecaseReponse.Title = saveTodo.Title;
        addTodoUsecaseReponse.Description = saveTodo.Description;
        addTodoUsecaseReponse.ScheduleStartDate = saveTodo.ScheduleStartDate;
        addTodoUsecaseReponse.ScheduleEndDate = saveTodo.ScheduleEndDate;
        addTodoUsecaseReponse.TodoItems = saveTodo.TodoItems.Select(x => new AddTodoResult.TodoItem
        {
            TodoItemId = x.TodoItemId,
            Title = x.Title,
            ScheduleStartDate = x.ScheduleStartDate,
            ScheduleEndDate = x.ScheduleEndDate
        }).ToArray();

        return addTodoUsecaseReponse;
    }
}

