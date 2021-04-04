using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Smith.MatrixSdk.Extensions;
using Smith.Services.Matrix.Agents;

namespace Smith.Services.Matrix.Authentication
{
    public class Authenticator : IAuthenticator
    {
        public static (string userId, string homeserver)? ExtractUserIdAndHomeServer(string userIdString)
        {
            var components = userIdString.Split(':', 2); // TODO: Proper UI workflow with homeserver
            if (components.Length < 2) return null;
            return (components[0], components[1]);
        }

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
                var (userId, homeserverHost) = ExtractUserIdAndHomeServer(login)!.Value;

                _agent.SetHomeserver(new Uri($"https://{homeserverHost}"));
                _authenticationState.OnNext(AuthenticationState.Authenticating);
                var result = await _agent.AuthenticateAsync(userId, password, ct);
                _agent.SetAccessToken(result.AccessToken.NotNull());
                _authenticationState.OnNext(AuthenticationState.Authenticated);
            });
    }
}
