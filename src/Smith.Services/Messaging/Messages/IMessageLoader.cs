﻿using System;
using Smith.Services.Messaging.Chats;

namespace Smith.Services.Messaging.Messages
{
    public interface IMessageLoader
    {
        IObservable<Message> LoadMessage(long chatId, long messageId);
        IObservable<Message> LoadMessages(Aggregate aggregate, AggregateLoadingState state);
        
        IObservable<Message> LoadNextMessages(Chat chat, long fromMessageId, int limit);
        IObservable<Message> LoadInitMessages(Chat chat, long fromMessageId, int limit);
        IObservable<Message> LoadPrevMessages(Chat chat, long fromMessageId, int limit);

        IObservable<Message> LoadPinnedMessage(Chat chat);
    }
}