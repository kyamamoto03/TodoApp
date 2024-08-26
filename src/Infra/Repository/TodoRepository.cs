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

    public async Task<Todo> AddAsync(Todo todo)
    {
        if (await IsExistAsync(todo.TodoId))
        {
            throw new ArgumentException("指定されたTodoは既に存在します");
        }

        _todoDbContext.Todos.Add(todo);

        return todo;
    }

    public Task UpdateAsync(Todo todo)
    {
        _todoDbContext.Entry(todo).State = EntityState.Modified;
        return Task.CompletedTask;
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
