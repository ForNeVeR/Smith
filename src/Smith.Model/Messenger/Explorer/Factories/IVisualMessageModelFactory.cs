using Smith.Model.Messenger.Explorer.Messages;
using Smith.Model.Messenger.Explorer.Messages.Visual;
using Smith.Services.Messaging.Messages;
using TdLib;

namespace Smith.Model.Messenger.Explorer.Factories
{
    public interface IVisualMessageModelFactory
    {
        PhotoMessageModel CreatePhotoMessage(
            Message message,
            TdApi.MessageContent.MessagePhoto messagePhoto);

        StickerMessageModel CreateStickerMessage(
            Message message,
            TdApi.MessageContent.MessageSticker messageSticker);

        VideoMessageModel CreateVideoMessage(
            Message message,
            TdApi.MessageContent.MessageVideo messageVideo);

        MessageModel CreateExpiredPhotoMessage(Message message, TdApi.MessageContent.MessageExpiredPhoto expiredPhoto);
        MessageModel CreateAnimationMessage(Message message, TdApi.MessageContent.MessageAnimation messageAnimation);
        MessageModel CreateExpiredVideoMessage(Message message, TdApi.MessageContent.MessageExpiredVideo expiredVideo);
        MessageModel CreateVideoNoteMessage(Message message, TdApi.MessageContent.MessageVideoNote videoNote);
    }
}
