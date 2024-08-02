namespace Todo.Domain;

public class TodoItem
{
    public string TodoItemId { get; internal set; } = default!;
    public string TodoId { get; internal set; } = default!;
    public string Title { get; internal set; } = default!;
    public DateTime ScheduleStartDate { get; internal set; } = default!;
    public DateTime ScheduleEndDate { get; internal set; } = default!;
    public DateTime? StartDate { get; internal set; } = default!;
    public DateTime? EndDate { get; internal set; } = default!;
    public TodoItemStatus TodoItemStatus { get; internal set; } = default!;

    internal TodoItem()
    {
    }
    
    public void TaskStart(DateTime startDate)
    {
        StartDate = startDate;
        TodoItemStatus = TodoItemStatus.進行中;
    }

    public void TaskEnd(DateTime endDate)
    {
        if(StartDate == null)
        {
            throw new ArgumentException("開始日が設定されていません");
        }
        if(StartDate > endDate)
        {
            throw new ArgumentException("開始日よりも前の日付は設定できません");
        }
        EndDate = endDate;
        TodoItemStatus = TodoItemStatus.完了;
    }
}
