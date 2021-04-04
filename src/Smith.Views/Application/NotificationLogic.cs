using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using ReactiveUI;
using Smith.Model.Notifications;
using Smith.Services.Utils.Platforms;
using Smith.Services.Utils.Reactive;
using Smith.Views.Notifications;
using Splat;

namespace Smith.Views.Application
{
    public static class NotificationLogic
    {
        public static IDisposable BindNotifications(
            this MainWindow mainWindow)
        {
            return mainWindow.BindNotifications(
                Locator.Current.GetService<IPlatform>(),
                Locator.Current.GetService<INotificationController>());
        }

        public static IDisposable BindNotifications(
            this MainWindow mainWindow,
            IPlatform platform,
            INotificationController controller)
        {
            var trigger = (controller as NotificationController)?.Trigger;

            if (trigger != null)
            {
                return trigger
                    .SubscribeOn(RxApp.TaskpoolScheduler)
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Accept(model =>
                    {
                        var screen = mainWindow.Screens.Primary;
                        var window = new NotificationWindow();
                        window.Show();

                        window.DataContext = model;
                        window.Position = new PixelPoint(
                            GetXForNotification(platform, screen.Bounds, window.Bounds),
                            GetYForNotification(platform, screen.Bounds, window.Bounds));
                    });
            }

            return Disposable.Empty;
        }

        private static int GetXForNotification(IPlatform platform, PixelRect outer, Rect inner)
        {
            switch (platform)
            {
                //case WindowsPlatform _:
                //    return outer.Width - inner.Width;
                default:
                    return outer.Width - (int)inner.Width;
            }
        }

        private static int GetYForNotification(IPlatform platform, PixelRect outer, Rect inner)
        {
            switch (platform)
            {
                //case WindowsPlatform _:
                //    return outer.Height - inner.Height;
                default:
                    return 0;
            }
        }
    }
}
