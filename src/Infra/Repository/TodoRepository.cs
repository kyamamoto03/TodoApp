using Domain.SeedOfWork;
using Domain.TodoModel;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class TodoRepository(TodoDbContext todoMemDbContext) : ITodoRepository
{
    private readonly TodoDbContext _todoDbContext = todoMemDbContext;

    public IUnitOfWork UnitOfWork => _todoDbContext;

    public async Task DeleteAsync(string todoId)
    {
        var targetTodo = await _todoDbContext.Todos.SingleOrDefaultAsync(x => x.TodoId == todoId);
        if (targetTodo == null)
        {
            throw new ArgumentException("指定されたTodoが存在しません");
        }
        _todoDbContext.Todos.Remove(targetTodo);
    }

    public Task<Todo?> FindByIdAsync(string todoId)
    {
        return _todoDbContext.Todos
            .Include(x => x.TodoItems)
            .FirstOrDefaultAsync(x => x.TodoId == todoId);
    }

    public Task<Todo?> FindByItemIdAsync(string todoItemId)
    {
        return _todoDbContext.Todos
            .Include(x => x.TodoItems)
            .Where(x => x.TodoItems.Any(x => x.TodoItemId.Contains(todoItemId)))
            .SingleOrDefaultAsync();
    }

    public async Task<Todo> AddAsync(Todo todo)
    {
        _todoDbContext.Todos.Add(todo);

        return todo;
    }

    public async ValueTask<bool> IsExistAsync(string todoId)
    {
        return await _todoDbContext.Todos.AnyAsync(x => x.TodoId == todoId);
    }

    public async Task<IEnumerable<Todo>> FindByUserIdAsync(string userId)
    {
        return await _todoDbContext.Todos
            .Include(x => x.TodoItems)
            .Where(x => x.UserId == userId)
            .ToArrayAsync();
    }
}