using Microsoft.AspNetCore.Components;
using TodoApp.Client.PageModel;

namespace TodoApp.Client.Pages;

public partial class Home
{
    [Inject]
    public IHomePageModel _homePageModel { get; set; } = default!;
}
