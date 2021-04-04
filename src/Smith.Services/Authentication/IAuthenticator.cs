using System;
using TdLib;

namespace Smith.Services.Authentication
{
    // TODO: Drop this
    public interface IAuthenticator
    {
        IObservable<TdApi.AuthorizationState> ObserveState();
        IObservable<TdApi.Ok> SetupParameters();
        IObservable<TdApi.Ok> CheckEncryptionKey();
        IObservable<TdApi.Ok> SetPhoneNumber(string phoneNumber);
        IObservable<TdApi.Ok> CheckCode(string code, string firstName = null, string lastName = null);
        IObservable<TdApi.Ok> CheckPassword(string password);
    }
}
