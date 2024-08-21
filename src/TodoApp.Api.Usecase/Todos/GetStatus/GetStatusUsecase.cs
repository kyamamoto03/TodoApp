using Domain.TodoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Api.Usecase.Todos.GetStatus;

public interface IGetStatusUsecase
{
   Task<GetStatusResult> Execute(GetStatusCommand command);
}

public class GetStatusUsecase(ITodoReposity todoReposity) : IGetStatusUsecase
{
    private readonly ITodoReposity _todoReposity = todoReposity;
    public async Task<GetStatusResult> Execute(GetStatusCommand command)
    {
        var todo = await _todoReposity.FindByIdAsync(command.TodoId);
        
        //todoがnullの場合、例外をスローする
        if (todo == null)
        {
            throw new Exception("Todo not found");
        }

        return new GetStatusResult(todo.TodoItemStatus.Id);
    }
}
