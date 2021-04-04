using System;
using System.Reactive.Linq;
using ReactiveUI;
using Smith.Model.Notifications;
using Smith.Services.Messaging.Notifications;
using Smith.Services.Utils.Reactive;
using Splat;

namespace Smith.Model.Messenger
{
    public static class NotificationLogic
    {
        public static IDisposable BindNotifications(
            this MessengerModel model)
        {
            return BindNotifications(
                model,
                Locator.Current.GetService<INotificationSource>(),
                Locator.Current.GetService<INotificationController>());
        }

        public static IDisposable BindNotifications(
            this MessengerModel model,
            INotificationSource notificationSource,
            INotificationController notificationController)
        {
            //var chats = notificationSource.ChatNotifications();
            var messages = notificationSource.MessagesNotifications();

            return messages // .Merge(chats)
                .Buffer(TimeSpan.FromSeconds(2))
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Accept(notifications =>
                {
                    switch (notifications.Count)
                    {
                        case 0:
                            break;

                        case 1:
                            notificationController.Show(
                                NotificationModel.FromNotification(notifications[0]));
                            break;

                        default:
                            notificationController.Show(
                                NotificationModel.FromNotificationList(notifications));
                            break;
                    }
                });
        }
    }
}
