using Avalonia.Markup.Xaml;
using Smith.Model.Messenger.Homepage;

namespace Smith.Views.Messenger.Homepage
{
    public class HomepageControl : BaseControl<HomepageModel>
    {
        public HomepageControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
