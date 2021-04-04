using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Smith.Model.Workspace;

namespace Smith.Views.Workspace
{
    public class WorkspaceControl : BaseControl<WorkspaceModel>
    {
        public WorkspaceControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
