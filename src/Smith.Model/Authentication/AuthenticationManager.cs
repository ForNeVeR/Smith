using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Smith.Model.Authentication.Results;
using Smith.Services.Authentication;
using Splat;

namespace Smith.Model.Authentication
{
    public class AuthenticationManager
    {
        private readonly IAuthenticator _authenticator;

        public AuthenticationManager(
            IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public AuthenticationManager()
            : this (
                Locator.Current.GetService<IAuthenticator>())
        {
        }

        public IDisposable Bind(AuthenticationModel model)
        {
            var canAuthenticate = model
                .WhenAnyValue(x => x.UserId, x => x.Password)
                .Select(pair => AreCredentialsValid(pair.Item1, pair.Item2));

            var disposable = new CompositeDisposable();

            model.AuthenticateCommand = ReactiveCommand.CreateFromObservable(
                    (AuthenticationModel param) => Authenticate(param.UserId, param.Password),
                    canAuthenticate,
                    RxApp.MainThreadScheduler)
                .DisposeWith(disposable);

            return disposable;
        }

        private bool AreCredentialsValid(string userId, string password)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(password)) return false;
            return Authenticator.ExtractUserIdAndHomeServer(userId).HasValue;
        }

        private IObservable<AuthenticationResult> Authenticate(string userId, string password) =>
            _authenticator
                .CheckLoginAndPassword(userId, password)
                .Select(_ => new AuthenticationResult());
    }
}
