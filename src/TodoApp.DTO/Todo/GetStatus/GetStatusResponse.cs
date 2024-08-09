using TodoApp.API.DTO;

namespace TodoApp.Api.DTO.Todo.GetStatus;

public class GetStatusResponse : IResponseBase
{
    public int Status { get; set; }
}
