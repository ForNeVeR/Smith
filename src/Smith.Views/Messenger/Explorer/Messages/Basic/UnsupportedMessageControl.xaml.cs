using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Explorer.Messages;

namespace Smith.Views.Messenger.Explorer.Messages.Basic
{
    public class UnsupportedMessageControl : BaseControl<UnsupportedMessageModel>
    {
        public UnsupportedMessageControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
