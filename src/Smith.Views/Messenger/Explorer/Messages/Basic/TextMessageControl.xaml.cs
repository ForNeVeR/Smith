using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Explorer.Messages.Basic;

namespace Smith.Views.Messenger.Explorer.Messages.Basic
{
    public class TextMessageControl : BaseControl<TextMessageModel>
    {
        public TextMessageControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
