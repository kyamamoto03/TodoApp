using Microsoft.AspNetCore.Components;
using MudBlazor;
using TodoApp.Client.Dialog;
using TodoApp.Client.PageModel;

namespace TodoApp.Client.Pages;

public partial class Home
{
    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [Inject]
    public HomePageModel _homePageModel { get; set; } = default!;

    private async Task CreateTodo()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };

        var dialog = await DialogService.ShowAsync<CreateTodoDialog>("Create Todo Dialog", options);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await _homePageModel.LoadTodo();
        }
    }
}