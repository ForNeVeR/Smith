using System;
using System.Reactive.Linq;
using ReactiveUI;
using Smith.Model.Messenger;
using Smith.Model.Settings;
using Smith.Model.Workspace.Navigation;
using Smith.Services.Messaging.Chats;
using Smith.Services.Utils.Reactive;

namespace Smith.Model.Workspace
{
    public static class NavigationLogic
    {
        public static IDisposable BindNavigation(
            this WorkspaceModel model)
        {
            model.NavigationModel = new NavigationModel();

            return model.NavigationModel.WhenAnyValue(m => m.SelectedTabIndex)
                .Select(index => (ContentKind)index)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Accept(kind =>
                {
                    switch (kind)
                    {
                        case ContentKind.Settings:
                            InitSettings(model);
                            break;

                        default:
                            InitMessenger(model, kind);
                            break;
                    }
                });
        }

        private static void InitMessenger(WorkspaceModel model, ContentKind kind)
        {
            var section = (Section) kind;

            model.SettingsModel = null;
            model.MessengerModel = new MessengerModel(section);
        }

        private static void InitSettings(WorkspaceModel model)
        {
            model.ContentIndex = 1;

            model.MessengerModel = null;
            model.SettingsModel = new SettingsModel();
        }
    }
}
