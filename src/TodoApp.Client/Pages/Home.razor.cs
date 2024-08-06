using Microsoft.AspNetCore.Components;
using TodoApp.Client.PageModel;

namespace TodoApp.Client.Pages;

public partial class Home
{
    [Inject]
    public HomePageModel _homePageModel { get; set; } = default!;
}
