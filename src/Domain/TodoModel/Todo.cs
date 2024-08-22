namespace Domain.TodoModel;

public class Todo
{
    public string UserId { get; private set; } = default!;
    public string TodoId { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public DateTime ScheduleStartDate { get; private set; } = default!;
    public DateTime ScheduleEndDate { get; private set; } = default!;

    /// <summary>
    /// 金額合計
    /// </summary>
    public decimal TotalAmount
    {
        get
        {
            return TodoItems.Sum(x => x.Amount);
        }
    }

    /// <summary>
    /// 税込み金額合計
    /// </summary>
    public decimal TotalTaxIncludedAmount
    {
        get
        {
            return TodoItems.Sum(x => x.TaxIncludedAmount);
        }
    }

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

    private List<TodoItem> _todoItems = new List<TodoItem>();

    public IReadOnlyCollection<TodoItem> TodoItems => _todoItems;

    private Todo()
    {
    }

    public static Todo Create(string userId,string todoId, string title, string description, DateTime scheduleStartDate, DateTime scheduleEndDate)
    {
        Todo todo = new Todo();

        todo.UserId = userId;
        todo.TodoId = todoId;
        todo.Title = title;
        todo.Description = description;
        todo.ScheduleStartDate = scheduleStartDate;
        todo.ScheduleEndDate = scheduleEndDate;

        return todo;
    }

    public static TodoItem CreateTodoItem(string todoItemId, string title, DateTime shceduleStartDate, DateTime shceduleEndDate)
    {
        TodoItem todoItem = new TodoItem();
        todoItem.TodoItemId = todoItemId;
        todoItem.Title = title;
        todoItem.ScheduleStartDate = shceduleStartDate;
        todoItem.ScheduleEndDate = shceduleEndDate;

        todoItem.StartDate = null;
        todoItem.EndDate = null;

        if (todoItem.ScheduleStartDate > todoItem.ScheduleEndDate)
        {
            throw new ArgumentException("開始日よりも前の日付は設定できません");
        }

        return todoItem;
    }

    public void SetTitle(string title)
    {
        this.Title = title;
    }

    public void AddTodoItem(TodoItem todoItem)
    {
        todoItem.TodoId = this.TodoId;
        _todoItems.Add(todoItem);
    }

    /// <summary>
    /// TodoItemを開始する
    /// </summary>
    /// <param name="todoItemId"></param>
    /// <param name="startDate"></param>
    /// <exception cref="ArgumentException"></exception>
    public void StartTodoItem(string todoItemId,DateTime startDate)
    {
        var todoItem = _todoItems.SingleOrDefault(x => x.TodoItemId == todoItemId);
        if (todoItem == null)
        {
            throw new ArgumentException("TodoItemが見つかりません");
        }

        todoItem.SetStart(startDate);
    }

    public void EndTodoItem(string todoItemId, DateTime endDate)
    {
        var todoItem = _todoItems.SingleOrDefault(x => x.TodoItemId == todoItemId);
        if (todoItem == null)
        {
            throw new ArgumentException("TodoItemが見つかりません");
        }

        todoItem.SetEnd(endDate);
    }

}
