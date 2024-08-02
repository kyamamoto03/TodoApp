namespace Todo.Domain.Test.Domain;

public class TodoTest
{
    [Fact]
    public void CreateNew����Todo��TodoItemStatus�����J�n�ɂȂ��Ă��邩()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        todo.AddTodoItem(todoItem);

        Assert.Equal(TodoItemStatus.���J�n, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItem�̊J�n����ݒ肵����TodoItemStatus���i�s���ɂȂ��Ă��邩()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        todo.TodoItems.First().TaskStart(DateTime.Now);

        Assert.Equal(TodoItemStatus.�i�s��, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItem�̏I������ݒ肵����TodoItemStatus�������ɂȂ��Ă��邩()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);

        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        todo.TodoItems.First().TaskStart(DateTime.Now);
        todo.TodoItems.First().TaskEnd(DateTime.Now);

        Assert.Equal(TodoItemStatus.����, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItem�̊J�n����ݒ肵����TodoItemStatus���i�s���ɂȂ��Ă��邩_TodoItem2��()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        TodoItem todoItem2 = Todo.CreateNewTodoItem("TodoItemTitle2", startDate, endDate);

        todo.AddTodoItem(todoItem2);

        todo.TodoItems.First().TaskStart(DateTime.Now);

        Assert.Equal(TodoItemStatus.�i�s��, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItem�̏I������ݒ肵����TodoItemStatus�������ɂȂ��Ă��邩_TodoItem2��()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        TodoItem todoItem2 = Todo.CreateNewTodoItem("TodoItemTitle2", startDate, endDate);
        todo.AddTodoItem(todoItem2);

        // ���ׂĂ�TodoItem���J�n���I������
        foreach (var item in todo.TodoItems)
        {
            item.TaskStart(DateTime.Now);
            item.TaskEnd(DateTime.Now);
        }

        Assert.Equal(TodoItemStatus.����, todo.TodoItemStatus);
    }

}