using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Smith.Model.Application.Startup;
using Smith.Model.Authentication;
using Smith.Model.Workspace;
using Smith.Services.Matrix.Authentication;
using Smith.Services.Utils.Reactive;
using Splat;

namespace Smith.Model.Application
{
    public static class AuthenticationLogic
    {
        public static IDisposable BindAuthentication(
            this MainWindowModel model)
        {
            return BindAuthentication(
                model,
                Locator.Current.GetService<IAuthenticator>());
        }

        public static IDisposable BindAuthentication(
            this MainWindowModel model,
            IAuthenticator authenticator)
        {
            var disposable = new CompositeDisposable();

            var stateUpdates = authenticator
                .ObserveState()
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler);

            stateUpdates
                .Accept(state => HandleState(model, state))
                .DisposeWith(disposable);

            return disposable;
        }

        private static void HandleState(MainWindowModel model, AuthenticationState state)
        {
            switch (state)
            {
                case AuthenticationState.WaitingLoginAndPassword:
                case AuthenticationState.Authenticating:
                    GoToAuthenticationPage(model);
                    break;
                case AuthenticationState.Authenticated:
                    GoToWorkspacePage(model);
                    break;
                default:
                    throw new Exception($"Unknown authentication state: {state}");
            }
        }

        // TODO[F]: Remove this
        private static void GoToStartupPage(MainWindowModel model)
        {
            if (model.StartupModel == null)
            {
                model.StartupModel = new StartupModel();
            }

            model.WorkspaceModel = null;
            model.AuthenticationModel = null;

            model.PageIndex = (int) Page.Initial;
        }

        private static void GoToAuthenticationPage(MainWindowModel model)
        {
            if (model.AuthenticationModel == null)
            {
                model.AuthenticationModel = new AuthenticationModel();
            }

            model.PageIndex = (int) Page.Authentication;

            model.StartupModel = null;
            model.WorkspaceModel = null;
        }

        private static void GoToWorkspacePage(MainWindowModel model)
        {
            if (model.WorkspaceModel == null)
            {
                model.WorkspaceModel = new WorkspaceModel();
            }

            model.PageIndex = (int) Page.Workspace;

            model.StartupModel = null;
            model.AuthenticationModel = null;
        }
    }
}
