namespace Todo.Domain;

public class Todo
{
    public string TodoId { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public DateTime ScheduleStartDate { get; private set; } = default!;
    public DateTime ScheduleEndDate { get; private set; } = default!;

    public TodoItemStatus TodoItemStatus
    {
        get
        {
            if (TodoItems.All(x => x.TodoItemStatus == TodoItemStatus.未開始) == true)
            {
                return TodoItemStatus.未開始;
            }
            else if (TodoItems.Any(x => x.TodoItemStatus == TodoItemStatus.進行中) == true)
            {
                return TodoItemStatus.進行中;
            }
            else
            {
                return TodoItemStatus.完了;
            }
        }
    }

    private List<TodoItem>_todoItems = new List<TodoItem>();

    public IReadOnlyCollection<TodoItem> TodoItems => _todoItems;

    private Todo()
    {        
    }

    public static Todo CreateNew(string title,string description,DateTime scheduleStartDate,DateTime scheduleEndDate)
    {
        Todo todo = new Todo();

        todo.TodoId = Guid.NewGuid().ToString();
        todo.Title = title;
        todo.Description = description;
        todo.ScheduleStartDate = scheduleStartDate;
        todo.ScheduleEndDate = scheduleEndDate;

        return todo;
    }

    public static TodoItem CreateNewTodoItem(string title, DateTime shceduleStartDate, DateTime shceduleEndDate)
    {
        TodoItem todoItem = new TodoItem();
        todoItem.TodoItemId = Guid.NewGuid().ToString();
        todoItem.Title = title;
        todoItem.ScheduleStartDate = shceduleStartDate;
        todoItem.ScheduleEndDate = shceduleEndDate;

        todoItem.StartDate = null;
        todoItem.EndDate = null;
        todoItem.TodoItemStatus = TodoItemStatus.未開始;

        if (todoItem.ScheduleStartDate > todoItem.ScheduleEndDate)
        {
            throw new ArgumentException("開始日よりも前の日付は設定できません");
        }

        return todoItem;
    }

    public void AddTodoItem(TodoItem todoItem)
    {
        todoItem.TodoId = this.TodoId;
        _todoItems.Add(todoItem);
    }
}
