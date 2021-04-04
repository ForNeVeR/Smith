using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Editor;

namespace Smith.Views.Messenger.Editor
{
    public class EditorControl : BaseControl<EditorModel>
    {
        public EditorControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
