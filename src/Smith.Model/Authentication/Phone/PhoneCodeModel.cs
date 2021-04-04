using Avalonia.Media.Imaging;
using PropertyChanged;
using ReactiveUI;

namespace Smith.Model.Authentication.Phone
{
    [AddINotifyPropertyChangedInterface]
    public class PhoneCodeModel
    {
        public string Code { get; set; }

        public string CountryCode { get; set; }

        public IBitmap Flag { get; set; }

        public string Mask { get; set; }
    }
}
