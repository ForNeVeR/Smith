using System;
using TdLib;

namespace Smith.Services.Messaging.Messages
{
    public interface IMessageSender
    {
        IObservable<TdApi.Message> SendMessage(
            TdApi.Chat chat,
            TdApi.InputMessageContent.InputMessageText messageTextContent);
    }
}