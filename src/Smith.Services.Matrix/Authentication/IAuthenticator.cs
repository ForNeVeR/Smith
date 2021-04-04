using System;
using System.Reactive;

namespace Smith.Services.Matrix.Authentication
{
    public interface IAuthenticator
    {
        IObservable<AuthenticationState> ObserveState();
        IObservable<Unit> CheckLoginAndPassword(string login, string password);
    }
}
