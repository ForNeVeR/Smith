using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Explorer.Messages.Special;

namespace Smith.Views.Messenger.Explorer.Messages.Special
{
    public class DocumentMessageControl : BaseControl<DocumentMessageModel>
    {
        public DocumentMessageControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
