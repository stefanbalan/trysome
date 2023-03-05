namespace KeySome
{
    [Command(PackageIds.OpenKeyEditorWindow)]
    internal sealed class OpenEditorWindow : BaseCommand<OpenEditorWindow>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await KeyEditorWindow.ShowAsync();
        }
    }
}
