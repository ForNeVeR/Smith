using System.Reactive.Disposables;
using PropertyChanged;
using ReactiveUI;
using Smith.Model.Messenger;
using Smith.Model.Settings;
using Smith.Model.Workspace.Navigation;

namespace Smith.Model.Workspace
{
    [AddINotifyPropertyChangedInterface]
    public class WorkspaceModel : ISupportsActivation
    {
        public NavigationModel NavigationModel { get; set; }

        public MessengerModel MessengerModel { get; set; }

        public SettingsModel SettingsModel { get; set; }

        public int ContentIndex { get; set; }

        public WorkspaceModel()
        {
            this.WhenActivated(disposables =>
            {
                this.BindNavigation()
                    .DisposeWith(disposables);
            });
        }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
