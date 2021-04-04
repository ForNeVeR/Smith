using Smith.Model.Messenger.Explorer.Messages;
using Tel.Egram.Services.Messaging.Messages;

namespace Smith.Model.Messenger.Explorer.Factories
{
    public interface IMessageModelFactory
    {
        MessageModel CreateMessage(Message message);
    }
}
