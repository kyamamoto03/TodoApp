using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TodoApp.Api.DTO.Todo.FindByUserId;

namespace TodoApp.Client.WebApiRepository;

public interface ITodoWebApi
{
    Task<FindByUserIdResponse> FindByUserIdAsync(string userId);
}

public class TodoWebApi(HttpClient httpClient) : ITodoWebApi
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<FindByUserIdResponse> FindByUserIdAsync(string userId)
    {
        var url = "/api/Todo/FindByUserId";

        HttpRequestMessage httpRequestMessage = new(HttpMethod.Post, url);
        httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(new { UserId = userId }), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            FindByUserIdResponse findByUserIdResponse = await response.Content.ReadFromJsonAsync<FindByUserIdResponse>();
            if (findByUserIdResponse == null)
            {
                throw new Exception("データエラー");
            }

            return findByUserIdResponse;
        }
        else
        {
            throw new Exception("通信エラー");
        }
    }
}