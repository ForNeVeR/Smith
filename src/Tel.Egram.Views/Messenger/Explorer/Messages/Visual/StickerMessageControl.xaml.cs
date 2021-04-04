using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Explorer.Messages.Visual;

namespace Tel.Egram.Views.Messenger.Explorer.Messages.Visual
{
    public class StickerMessageControl : BaseControl<StickerMessageModel>
    {
        public StickerMessageControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
