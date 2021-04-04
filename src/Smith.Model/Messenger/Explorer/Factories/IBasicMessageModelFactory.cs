using Smith.Model.Messenger.Explorer.Messages;
using Smith.Model.Messenger.Explorer.Messages.Basic;
using TdLib;
using Tel.Egram.Services.Messaging.Messages;

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
