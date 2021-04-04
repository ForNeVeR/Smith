using System;

namespace Smith.Services.Messaging.Users
{
    public interface IUserLoader
    {
        IObservable<User> GetMe();
    }
}