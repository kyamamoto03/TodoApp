using Domain.Todos;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
namespace Infra.Test;

public class UnitTest1 : IDisposable
{
    private readonly TodoMemDbContext _todoMemDbContext;
    public UnitTest1()
    {
        _todoMemDbContext = new TodoMemDbContext(new DbContextOptionsBuilder<TodoMemDbContext>()
            .UseInMemoryDatabase("TodoMemDbContext")
            .Options);
    }
    public void Dispose()
    {
        _todoMemDbContext.Dispose();
    }

    [Fact]
    public async Task Todo_Save_OK()
    {
        ITodoReposity todoRepository = new TodoRepository(_todoMemDbContext);

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        await todoRepository.SaveAsync(todo);

        var savedTodo = await todoRepository.FindByIdAsync(todo.TodoId);
        Assert.NotNull(savedTodo);
        Assert.Equal(todo.Title, savedTodo.Title);
    }

    [Fact]
    public async Task Todo_Save_TodoItem_Save_OK()
    {
        ITodoReposity todoRepository = new TodoRepository(_todoMemDbContext);

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);

        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        TodoItem todoItem2 = Todo.CreateNewTodoItem("TodoItemTitle2", startDate, endDate);
        todo.AddTodoItem(todoItem2);


        await todoRepository.SaveAsync(todo);

        var savedTodo = await todoRepository.FindByIdAsync(todo.TodoId);
        Assert.NotNull(savedTodo);
        Assert.Equal(todo.Title, savedTodo.Title);

        Assert.Equal(2, savedTodo.TodoItems.Count);
        Assert.Equal(todoItem2.Title, savedTodo.TodoItems.ElementAt(1).Title);
    }
}