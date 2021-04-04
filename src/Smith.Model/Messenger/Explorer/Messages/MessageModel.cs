using Smith.Model.Messenger.Explorer.Items;
using Smith.Services.Graphics.Avatars;
using Smith.Services.Messaging.Messages;

namespace Smith.Model.Messenger.Explorer.Messages
{
    public abstract class MessageModel : ItemModel
    {
        public string AuthorName { get; set; }

        public string Time { get; set; }

        public Avatar Avatar { get; set; }

        public Message Message { get; set; }

        public bool HasReply { get; set; }

        public ReplyModel Reply { get; set; }
    }
}
