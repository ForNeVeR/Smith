using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Smith.MatrixSdk.Extensions;
using Smith.Services.Matrix;

namespace Smith.Services.Authentication
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAgent _agent;

        private readonly BehaviorSubject<AuthenticationState> _authenticationState =
            new(AuthenticationState.WaitingLoginAndPassword);

        public Authenticator(IAgent agent)
        {
            _agent = agent;
        }

        public IObservable<AuthenticationState> ObserveState() => _authenticationState;

        public IObservable<Unit> CheckLoginAndPassword(string login, string password) =>
            Observable.FromAsync(async ct =>
            {
                var loginComponents = login.Split(':', 2); // TODO: Proper UI workflow with homeserver
                var loginUser = loginComponents[0];
                var homeserverHost = loginComponents[1];

                _agent.SetHomeserver(new Uri($"https://{homeserverHost}"));
                _authenticationState.OnNext(AuthenticationState.Authenticating);
                var result = await _agent.AuthenticateAsync(loginUser, password, ct);
                _agent.SetAccessToken(result.AccessToken.NotNull());
                _authenticationState.OnNext(AuthenticationState.Authenticated);
            });
    }
}
