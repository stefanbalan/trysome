using Microsoft.AspNetCore.Components;

namespace Lazy.Client.Shared;

public class BaseEditPage : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    public bool HasChanges { get; set; }
}