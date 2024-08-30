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

    private Task CreateTodo()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };

        return DialogService.ShowAsync<CreateTodoDialog>("Create Todo Dialog", options);
    }
}