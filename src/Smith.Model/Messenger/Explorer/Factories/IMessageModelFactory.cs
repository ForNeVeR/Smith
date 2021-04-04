using Smith.Model.Messenger.Explorer.Messages;
using Smith.Services.Messaging.Messages;

namespace Smith.Model.Messenger.Explorer.Factories
{
    public interface IMessageModelFactory
    {
        MessageModel CreateMessage(Message message);
    }
}
