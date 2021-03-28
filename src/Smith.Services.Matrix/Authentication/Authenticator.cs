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
            new(AuthenticationState.WaitingHomeserver);

        public Authenticator(IAgent agent)
        {
            _agent = agent;
        }

        public IObservable<AuthenticationState> ObserveState() => _authenticationState;

        public void SetHomeserver(Uri homeserver)
        {
            _agent.SetHomeserver(homeserver);
            _authenticationState.OnNext(AuthenticationState.WaitingLoginAndPassword);
        }

        public IObservable<Unit> CheckLoginAndPassword(string login, string password) =>
            Observable.FromAsync(async ct =>
            {
                _authenticationState.OnNext(AuthenticationState.Authenticating);
                var result = await _agent.AuthenticateAsync(login, password, ct);
                _agent.SetAccessToken(result.AccessToken.NotNull());
                _authenticationState.OnNext(AuthenticationState.Authenticated);
            });
    }
}
