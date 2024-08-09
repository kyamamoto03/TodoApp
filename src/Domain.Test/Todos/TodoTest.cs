namespace Domain.Todos;

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

    [Fact]
    public void ���z�ݒ�e�X�g()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);

        todoItem.SetAmount(100);

        Assert.Equal(100, todoItem.Amount);
    }
    [Fact]
    public void ���z�ݒ�_�Ńv���X_�e�X�g()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);

        todoItem.SetAmount(100);

        Assert.Equal(110, todoItem.TaxIncludedAmount);
    }

    [Fact]
    public void ���z���v�e�X�g_TodoItem10��()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        for (int i = 0; i < 10; i++)
        {
            TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);

            todoItem.SetAmount(100);
            todo.AddTodoItem(todoItem);
        }

        Assert.Equal(1000, todo.TotalAmount);
    }

    [Fact]
    public void ���z���v�e�X�g_�Ńv���X_TodoItem10��()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        for (int i = 0; i < 10; i++)
        {
            TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);

            todoItem.SetAmount(100);
            todo.AddTodoItem(todoItem);
        }

        Assert.Equal(1100, todo.TotalTaxIncludedAmount);
    }
}