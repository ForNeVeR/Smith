using Smith.Model.Messenger.Explorer.Messages;
using Smith.Model.Messenger.Explorer.Messages.Basic;
using Smith.Services.Messaging.Messages;
using TdLib;

namespace Smith.Model.Messenger.Explorer.Factories
{
    public interface IBasicMessageModelFactory
    {
        TextMessageModel CreateTextMessage(
            Message message,
            TdApi.MessageContent.MessageText messageText);

        MessageModel CreateUnsupportedMessage(Message message);
    }
}
