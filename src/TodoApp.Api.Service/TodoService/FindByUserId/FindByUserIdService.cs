using Domain.TodoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Api.Service.TodoService.FindByUserId;

public interface IFindByUserIdService
{
    Task<IEnumerable<FindByUserIdResult>> Execute(string userId);
}

public class FindByUserIdService(ITodoRepository todoRepository) : IFindByUserIdService
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<IEnumerable<FindByUserIdResult>> Execute(string userId)
    {
        var result = await _todoRepository.FindByUserIdAsync(userId);

        List<FindByUserIdResult> findByUserIdResults = new();
        //resultをfindByUserIdResultsに詰め替える
        foreach (var todo in result)
        {
            findByUserIdResults.Add(new FindByUserIdResult
            {
                UserId = todo.UserId,
                TodoId = todo.TodoId,
                Title = todo.Title,
                Description = todo.Description,
                ScheduleStartDate = todo.ScheduleStartDate,
                ScheduleEndDate = todo.ScheduleEndDate,
                TodoItemResults = todo.TodoItems.Select(x => new FindByUserIdResult.TodoItemResult
                {
                    TodoItemId = x.TodoItemId,
                    Title = x.Title,
                    ScheduleStartDate = x.ScheduleStartDate,
                    ScheduleEndDate = x.ScheduleEndDate,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToArray()
            });
        }

        return findByUserIdResults.ToArray();
    }
}