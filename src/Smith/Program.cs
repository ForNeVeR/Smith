using Avalonia;
using Avalonia.Platform;
using JetBrains.Annotations;
using Splat;
using Smith.Application;
using Smith.Model.Application;
using Tel.Egram.Views.Application;

namespace Smith
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = BuildAvaloniaApp();
            var model = new MainWindowModel();

            model.Activator.Activate();
            builder.Start<MainWindow>(() => model);
            model.Activator.Deactivate();
        }

        private static void ConfigureServices(
            IMutableDependencyResolver services)
        {
            services.AddUtils();
            services.AddTdLib();
            services.AddMatrixSdk();
            services.AddPersistance();
            services.AddServices();

            services.AddComponents();
            services.AddApplication();
            services.AddAuthentication();
            services.AddWorkspace();
            services.AddSettings();
            services.AddMessenger();
        }

        [UsedImplicitly]
        public static AppBuilder BuildAvaloniaApp()
        {
            ConfigureServices(Locator.CurrentMutable);
            return BuildApp(Locator.Current);
        }

        private static AppBuilder BuildApp(IDependencyResolver resolver)
        {
            var app = resolver.GetService<MainApplication>();
            var builder = AppBuilder.Configure(app);
            var runtime = builder.RuntimePlatform.GetRuntimeInfo();

            switch (runtime.OperatingSystem)
            {
                case OperatingSystemType.OSX:
                    builder.UseAvaloniaNative()
                        .With(new AvaloniaNativePlatformOptions
                        {
                            UseGpu = true,
                            UseDeferredRendering = true
                        })
                        .UseSkia();
                    break;

                case OperatingSystemType.Linux:
                    builder.UseX11()
                        .With(new X11PlatformOptions
                        {
                            UseGpu = true
                        })
                        .UseSkia();
                    break;

                default:
                    builder.UseWin32()
                        .With(new Win32PlatformOptions
                        {
                            UseDeferredRendering = true
                        })
                        .UseSkia();
                    break;
            }

            builder.UseReactiveUI();

            return builder;
        }
    }
}
