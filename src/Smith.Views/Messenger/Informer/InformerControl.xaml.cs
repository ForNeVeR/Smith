using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Informer;

namespace Smith.Views.Messenger.Informer
{
    public class InformerControl : BaseControl<InformerModel>
    {
        public InformerControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
