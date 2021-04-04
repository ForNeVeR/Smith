using Smith.Model.Messenger.Explorer.Messages;
using Smith.Model.Messenger.Explorer.Messages.Basic;
using Smith.Services.Messaging.Messages;
using TdLib;

namespace Smith.Model.Messenger.Explorer.Factories
{
    public class BasicMessageModelFactory : IBasicMessageModelFactory
    {
        public BasicMessageModelFactory()
        {

        }

        public TextMessageModel CreateTextMessage(
            Message message,
            TdApi.MessageContent.MessageText messageText)
        {
            var text = messageText.Text.Text;

            return new TextMessageModel
            {
                Text = text
            };
        }

        public MessageModel CreateUnsupportedMessage(Message message)
        {
            return new UnsupportedMessageModel
            {
                Message = message
            };
        }
    }
}
