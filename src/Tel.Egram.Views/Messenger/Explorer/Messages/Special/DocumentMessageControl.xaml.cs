using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Explorer.Messages.Special;

namespace Tel.Egram.Views.Messenger.Explorer.Messages.Special
{
    public class DocumentMessageControl : BaseControl<DocumentMessageModel>
    {
        public DocumentMessageControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
