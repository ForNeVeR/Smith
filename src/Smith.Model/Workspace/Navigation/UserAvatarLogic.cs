using System;
using System.Reactive.Linq;
using ReactiveUI;
using Smith.Services.Graphics.Avatars;
using Smith.Services.Messaging.Users;
using Smith.Services.Utils.Reactive;
using Splat;

namespace Smith.Model.Workspace.Navigation
{
    public static class UserAvatarLogic
    {
        public static IDisposable BindUserAvatar(
            this NavigationModel model)
        {
            return BindUserAvatar(
                model,
                Locator.Current.GetService<IAvatarLoader>(),
                Locator.Current.GetService<IUserLoader>());
        }

        public static IDisposable BindUserAvatar(
            this NavigationModel model,
            IAvatarLoader avatarLoader,
            IUserLoader userLoader)
        {
            return userLoader
                .GetMe()
                .SelectMany(user => avatarLoader.LoadAvatar(user.UserData, AvatarSize.Regular))
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Accept(avatar =>
                {
                    model.Avatar = avatar;
                });
        }
    }
}
