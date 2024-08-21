using Domain.TodoModel;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Test.ITodoRepository;

public class AddeTest : IAsyncDisposable
{

    private readonly TodoDbContext _todoDbContext;
    public AddeTest()
    {
        _todoDbContext = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase("TodoMemDbContext")
            .Options);
    }

    public async ValueTask DisposeAsync()
    {
        await _todoDbContext.DisposeAsync();
    }

    [Fact]
    public async Task Todo_Add_OK()
    {
        ITodoReposity todoRepository = new TodoRepository(_todoDbContext);

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.Create(Guid.NewGuid().ToString(), "TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateTodoItem(Guid.NewGuid().ToString(), "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        await todoRepository.AddAsync(todo);
        await todoRepository.UnitOfWork.SaveChangesAsync();

        var savedTodo = await todoRepository.FindByIdAsync(todo.TodoId);
        await todoRepository.UnitOfWork.SaveChangesAsync();

        Assert.NotNull(savedTodo);
        Assert.Equal(todo.Title, savedTodo.Title);
    }
    [Fact]
    public async Task Todo_Add_重複ID_NG()
    {
        ITodoReposity todoRepository = new TodoRepository(_todoDbContext);

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var id = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(id, "TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateTodoItem(Guid.NewGuid().ToString(), "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        await todoRepository.AddAsync(todo);
        await todoRepository.UnitOfWork.SaveChangesAsync();

        Todo todo2 = Todo.Create(id, "TodoTitle", "TodoDescription", startDate, endDate);
        await Assert.ThrowsAsync<ArgumentException>(async () => await todoRepository.AddAsync(todo2));
    }

    [Fact]
    public async Task Todo_Add_TodoItem_Save_OK()
    {
        ITodoReposity todoRepository = new TodoRepository(_todoDbContext);

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.Create(Guid.NewGuid().ToString(), "TodoTitle", "TodoDescription", startDate, endDate);

        TodoItem todoItem = Todo.CreateTodoItem(Guid.NewGuid().ToString(), "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        TodoItem todoItem2 = Todo.CreateTodoItem(Guid.NewGuid().ToString(), "TodoItemTitle2", startDate, endDate);
        todo.AddTodoItem(todoItem2);


        await todoRepository.AddAsync(todo);
        await todoRepository.UnitOfWork.SaveChangesAsync();

        var savedTodo = await todoRepository.FindByIdAsync(todo.TodoId);
        Assert.NotNull(savedTodo);
        Assert.Equal(todo.Title, savedTodo.Title);

        Assert.Equal(2, savedTodo.TodoItems.Count);
        Assert.Equal(todoItem2.Title, savedTodo.TodoItems.ElementAt(1).Title);
    }

    [Fact]
    public async Task Todo_Addし更新_OK()
    {
        ITodoReposity todoRepository = new TodoRepository(_todoDbContext);

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.Create(Guid.NewGuid().ToString(), "TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateTodoItem(Guid.NewGuid().ToString(), "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        await todoRepository.AddAsync(todo);
        await todoRepository.UnitOfWork.SaveChangesAsync();

        var savedTodo = await todoRepository.FindByIdAsync(todo.TodoId);

        var todoItemStartDate = DateTime.Now;

        savedTodo.StartTodoItem(savedTodo.TodoItems.First().TodoItemId, todoItemStartDate);

        await todoRepository.UpdateAsync(savedTodo);
        await todoRepository.UnitOfWork.SaveChangesAsync();

        var savedTodo2 = await todoRepository.FindByIdAsync(todo.TodoId);

        Assert.NotNull(savedTodo2);
        Assert.Equal(todoItemStartDate, savedTodo2.TodoItems.First().StartDate);
    }

}
