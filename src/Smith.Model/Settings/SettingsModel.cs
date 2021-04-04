using PropertyChanged;
using ReactiveUI;

namespace Smith.Model.Settings
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsModel : ISupportsActivation
    {
        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
