using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Explorer.Messages.Visual;

namespace Smith.Views.Messenger.Explorer.Messages.Visual
{
    public class VideoMessageControl : BaseControl<VideoMessageModel>
    {
        public VideoMessageControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
