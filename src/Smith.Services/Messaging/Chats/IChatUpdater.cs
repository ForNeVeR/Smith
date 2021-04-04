using System;
using System.Reactive;

namespace Smith.Services.Messaging.Chats
{
    public interface IChatUpdater
    {
        IObservable<Unit> GetOrderUpdates();

        IObservable<Chat> GetChatUpdates();
    }
}