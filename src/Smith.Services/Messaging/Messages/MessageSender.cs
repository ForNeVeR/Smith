using System;
using Smith.Services.Utils.TdLib;
using TdLib;

namespace Smith.Services.Messaging.Messages
{
    public class MessageSender : IMessageSender
    {
        private readonly IAgent _agent;

        public MessageSender(
            IAgent agent
            )
        {
            _agent = agent;
        }
        
        public IObservable<TdApi.Message> SendMessage(
            TdApi.Chat chat,
            TdApi.InputMessageContent.InputMessageText messageTextContent)
        {
            return _agent.Execute(new TdApi.SendMessage
            {
                ChatId = chat.Id,
                InputMessageContent = messageTextContent
            });
        }
    }
}