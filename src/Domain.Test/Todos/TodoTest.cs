namespace Domain.TodoModel;

public class TodoTest
{
    [Fact]
    public void CreateNew����Todo��TodoItemStatus�����J�n�ɂȂ��Ă��邩()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId,"TodoTitle", "TodoDescription", startDate, endDate);

        var todoItemId = Guid.NewGuid().ToString();

        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        Assert.Equal(TodoItemStatus.���J�n, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItem�̊J�n����ݒ肵����TodoItemStatus���i�s���ɂȂ��Ă��邩()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);

        var todoItemId = Guid.NewGuid().ToString();

        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        todo.TodoItems.First().TaskStart(DateTime.Now);

        Assert.Equal(TodoItemStatus.�i�s��, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItem�̏I������ݒ肵����TodoItemStatus�������ɂȂ��Ă��邩()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);

        var todoItemId = Guid.NewGuid().ToString();

        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);
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

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);

        var todoItemId = Guid.NewGuid().ToString();

        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        var todoItemId2 = Guid.NewGuid().ToString();

        TodoItem todoItem2 = Todo.CreateTodoItem(todoItemId2, "TodoItemTitle2", startDate, endDate);

        todo.AddTodoItem(todoItem2);

        todo.TodoItems.First().TaskStart(DateTime.Now);

        Assert.Equal(TodoItemStatus.�i�s��, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItem�̏I������ݒ肵����TodoItemStatus�������ɂȂ��Ă��邩_TodoItem2��()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);

        var todoItemId = Guid.NewGuid().ToString();

        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        var todoItemId2 = Guid.NewGuid().ToString();

        TodoItem todoItem2 = Todo.CreateTodoItem(todoItemId2, "TodoItemTitle2", startDate, endDate);
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

        var todoItemId = Guid.NewGuid().ToString();
        TodoItem todoItem = Todo.CreateTodoItem(todoItemId,"TodoItemTitle", startDate, endDate);

        todoItem.SetAmount(100);

        Assert.Equal(100, todoItem.Amount);
    }
    [Fact]
    public void ���z�ݒ�_�Ńv���X_�e�X�g()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var todoItemId = Guid.NewGuid().ToString();
        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);

        todoItem.SetAmount(100);

        Assert.Equal(110, todoItem.TaxIncludedAmount);
    }

    [Fact]
    public void ���z���v�e�X�g_TodoItem10��()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);
        for (int i = 0; i < 10; i++)
        {
            var todoItemId = Guid.NewGuid().ToString();
            TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);

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

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);
        for (int i = 0; i < 10; i++)
        {
            var todoItemId = Guid.NewGuid().ToString();
            TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);

            todoItem.SetAmount(100);
            todo.AddTodoItem(todoItem);
        }

        Assert.Equal(1100, todo.TotalTaxIncludedAmount);
    }

    [Fact]
    public void �J�n�e�X�g_�i�s���ɂȂ邱��()
    {

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);
        var todoItemId = Guid.NewGuid().ToString();
        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);

        todo.AddTodoItem(todoItem);

        //���J�n���m�F
        Assert.Equal(TodoItemStatus.���J�n, todo.TodoItemStatus);

        //�J�n����
        var StartDate = DateTime.Now;
        todo.StartTodoItem(todoItem.TodoItemId, StartDate);


        //�J�n�ɂȂ������Ƃ��m�F����
        Assert.Equal(TodoItemStatus.�i�s��, todo.TodoItemStatus);
    }

    [Fact]
    public void �I���e�X�g_�����ɂȂ邱��()
    {

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);
        var todoItemId = Guid.NewGuid().ToString();
        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);

        todo.AddTodoItem(todoItem);

        //���J�n���m�F
        Assert.Equal(TodoItemStatus.���J�n, todo.TodoItemStatus);

        //�J�n����
        var StartDate = DateTime.Now;
        todo.StartTodoItem(todoItem.TodoItemId, StartDate);

        //�J�n�ɂȂ������Ƃ��m�F����
        Assert.Equal(TodoItemStatus.�i�s��, todo.TodoItemStatus);

        //�I������
        var EndDate = DateTime.Now;
        todo.EndTodoItem(todoItem.TodoItemId, EndDate);

        //�J�n�ɂȂ������Ƃ��m�F����
        Assert.Equal(TodoItemStatus.����, todo.TodoItemStatus);
    }

}