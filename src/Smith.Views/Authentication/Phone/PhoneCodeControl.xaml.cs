using Avalonia.Markup.Xaml;
using Smith.Model.Authentication;
using Smith.Model.Authentication.Phone;

namespace Smith.Views.Authentication.Phone
{
    public class PhoneCodeControl : BaseControl<PhoneCodeModel>
    {
        public PhoneCodeControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
