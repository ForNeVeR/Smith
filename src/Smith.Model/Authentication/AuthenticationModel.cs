using System.Reactive.Disposables;
using PropertyChanged;
using ReactiveUI;
using Smith.Model.Authentication.Results;

namespace Smith.Model.Authentication
{
    [AddINotifyPropertyChangedInterface]
    public class AuthenticationModel : ISupportsActivation
    {
        public string UserId { get; set; }
        public string Password { get; set; }

        public ReactiveCommand<AuthenticationModel, AuthenticationResult> AuthenticateCommand { get; set; }

        public AuthenticationModel()
        {
            this.WhenActivated(disposables =>
            {
                new AuthenticationManager()
                    .Bind(this)
                    .DisposeWith(disposables);
            });
        }

        public ViewModelActivator Activator { get; } = new();
    }
}
