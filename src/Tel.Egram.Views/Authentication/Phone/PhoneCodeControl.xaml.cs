using Avalonia.Markup.Xaml;
using Smith.Model.Authentication;
using Smith.Model.Authentication.Phone;

namespace Tel.Egram.Views.Authentication.Phone
{
    public class PhoneCodeControl : BaseControl<PhoneCodeModel>
    {
        public PhoneCodeControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
