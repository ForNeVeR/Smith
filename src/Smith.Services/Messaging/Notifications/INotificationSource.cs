using System;

namespace Smith.Services.Messaging.Notifications
{
    public interface INotificationSource
    {
        IObservable<Notification> ChatNotifications();

        IObservable<Notification> MessagesNotifications();
    }
}