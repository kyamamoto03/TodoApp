using Domain.Todos;

namespace TodoApp.Client.PageModel;

public class HomePageModel()
{
    private List<Todo> _todos = new();

    public List<Todo> Todos => _todos;

    public void CreateTodo()
    {
        Todo todo = Todo.CreateNew("TITLE", "DESCRIPTION", new DateTime(2024, 8, 1), new DateTime(2024, 8, 2));
        TodoItem todoItem = Todo.CreateNewTodoItem("ItemTitle", new DateTime(2024, 8, 1, 10, 0, 0), new DateTime(2024, 8, 2, 15, 0, 0));


        todo.AddTodoItem(todoItem);

        Todos.Add(todo);
    }

}
