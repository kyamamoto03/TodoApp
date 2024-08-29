using TodoApp.Client.Domain.TodoModel;
using TodoApp.Client.WebApiRepository;

namespace TodoApp.Client.PageModel;

public class HomePageModel(ITodoWebApi todoWebApi)
{
    private readonly ITodoWebApi _todoWebApi = todoWebApi;

    private List<Todo> _todos = new();

    public IReadOnlyList<Todo> Todos => _todos.AsReadOnly();

    public void CreateTodo()
    {
        Todo todo = Todo.CreateNew("TITLE", "DESCRIPTION", new DateTime(2024, 8, 1), new DateTime(2024, 8, 2));
        TodoItem todoItem = Todo.CreateNewTodoItem("ItemTitle", new DateTime(2024, 8, 1, 10, 0, 0), new DateTime(2024, 8, 2, 15, 0, 0));

        todo.AddTodoItem(todoItem);

        _todos.Add(todo);
    }

    public async Task LoadTodo()
    {
        var findByUserIdResponse = await todoWebApi.FindByUserIdAsync("USER01");

        _todos.Clear();
        foreach (var item in findByUserIdResponse.Todos)
        {
            Todo todo = Todo.Create(item.TodoId,
                item.Title,
                item.Description,
                item.ScheduleStartDate,
                item.ScheduleEndDate);

            _todos.Add(todo);
        }
    }
}