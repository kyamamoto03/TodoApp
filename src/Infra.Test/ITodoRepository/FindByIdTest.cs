using Domain.Todos;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Test.ITodoRepository;

public class FindByIdTest : IAsyncDisposable
{
    private readonly TodoMemDbContext _todoMemDbContext;

    public FindByIdTest()
    {
        _todoMemDbContext = new TodoMemDbContext(new DbContextOptionsBuilder<TodoMemDbContext>()
            .UseInMemoryDatabase("TodoMemDbContext")
            .Options);
    }

    public async ValueTask DisposeAsync()
    {
        await _todoMemDbContext.DisposeAsync();
    }

    [Fact]
    public async Task 保存したTodoを読み込む_OK()
    {
        ITodoReposity todoRepository = new TodoRepository(_todoMemDbContext);

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.Create(Guid.NewGuid().ToString(),"TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateTodoItem(Guid.NewGuid().ToString(), "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        await todoRepository.SaveAsync(todo);

        var savedTodo = await todoRepository.FindByIdAsync(todo.TodoId);
        Assert.NotNull(savedTodo);
        Assert.Equal(todo.Title, savedTodo.Title);
    }

    [Fact]
    public async Task 存在しないTodo読み込み_0件が返る()
    {
        ITodoReposity todoRepository = new TodoRepository(_todoMemDbContext);

        var todoId = Guid.NewGuid().ToString();
        var savedTodo = await todoRepository.FindByIdAsync(todoId);
        Assert.Null(savedTodo);

    }
}
