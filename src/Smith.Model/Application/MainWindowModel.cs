using System.Reactive.Disposables;
using PropertyChanged;
using ReactiveUI;
using Smith.Model.Authentication;
using Smith.Model.Popups;
using Smith.Model.Workspace;

namespace Smith.Model.Application
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowModel : ISupportsActivation
    {
        public AuthenticationModel AuthenticationModel { get; set; }

        public WorkspaceModel WorkspaceModel { get; set; }

        public PopupModel PopupModel { get; set; }

        public int PageIndex { get; set; }

        public string WindowTitle { get; set; }

        public string ConnectionState { get; set; }

        public MainWindowModel()
        {
            this.WhenActivated(disposables =>
            {
                this.BindAuthentication()
                    .DisposeWith(disposables);

                this.BindConnectionInfo()
                    .DisposeWith(disposables);

                this.BindPopup()
                    .DisposeWith(disposables);
            });
        }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
