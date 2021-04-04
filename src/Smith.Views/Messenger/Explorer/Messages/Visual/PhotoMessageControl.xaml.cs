using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Explorer.Messages.Visual;

namespace Smith.Views.Messenger.Explorer.Messages.Visual
{
    public class PhotoMessageControl : BaseControl<PhotoMessageModel>
    {
        public PhotoMessageControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
