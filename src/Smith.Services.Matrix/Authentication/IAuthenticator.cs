using System;
using System.Reactive;

namespace Smith.Services.Authentication
{
    public interface IAuthenticator
    {
        IObservable<AuthenticationState> ObserveState();
        void SetHomeserver(Uri homeserver);
        IObservable<Unit> CheckLoginAndPassword(string login, string password);
    }
}
