using TdLib;

namespace Smith.Services.Messaging.Chats
{
    public class Chat
    {
        public TdApi.Chat ChatData { get; set; }
        
        public TdApi.User User { get; set; }
    }
}