using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReactiveUI;
using Smith.Application;
using Smith.Model.Messenger.Explorer.Factories;
using Smith.Model.Notifications;
using Smith.Model.Popups;
using Smith.Services.Graphics;
using Smith.Services.Graphics.Avatars;
using Smith.Services.Graphics.Previews;
using Smith.Services.Messaging.Chats;
using Smith.Services.Messaging.Messages;
using Smith.Services.Messaging.Notifications;
using Smith.Services.Messaging.Users;
using Smith.Services.Persistance;
using Smith.Services.Settings;
using Smith.Services.Utils.Formatting;
using Smith.Services.Utils.Platforms;
using Smith.Services.Utils.TdLib;
using Splat;
using TdLib;
using BitmapLoader = Smith.Services.Graphics.BitmapLoader;
using IBitmapLoader = Smith.Services.Graphics.IBitmapLoader;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using IMatrixAgent = Smith.Services.Matrix.Agents.IAgent;
using MatrixAgent = Smith.Services.Matrix.Agents.Agent;
using IMatrixAuthenticator = Smith.Services.Matrix.Authentication.IAuthenticator;
using MatrixAuthenticator = Smith.Services.Matrix.Authentication.Authenticator;

namespace Smith
{
    public static class Registry
    {
        public static void AddLogging(this IMutableDependencyResolver services)
        {
            // TODO: Update the logging to use Splat helper
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            services.RegisterLazySingleton(() => loggerFactory.CreateLogger("Smith"));

            RxApp.DefaultExceptionHandler = new LoggerExceptionHandler(services.GetService<ILogger>());
        }

        public static void AddHttp(this IMutableDependencyResolver services)
        {
            services.Register(() => new HttpClient());
        }

        public static void AddUtils(this IMutableDependencyResolver services)
        {
            services.RegisterLazySingleton<IPlatform>(Platform.GetPlatform);
            services.RegisterLazySingleton<IStringFormatter>(() => new StringFormatter());
        }

        public static void AddTdLib(this IMutableDependencyResolver services)
        {
            services.RegisterLazySingleton(() =>
            {
                var storage = services.GetService<IStorage>();

                Client.Log.SetFilePath(Path.Combine(storage.LogDirectory, "tdlib.log"));
                Client.Log.SetMaxFileSize(1_000_000); // 1MB
                Client.Log.SetVerbosityLevel(5);
                return new Client();
            });

            services.RegisterLazySingleton(() =>
            {
                var client = services.GetService<Client>();
                return new Hub(client);
            });

            services.RegisterLazySingleton(() =>
            {
                var client = services.GetService<Client>();
                var hub = services.GetService<Hub>();
                return new Dialer(client, hub);
            });

            services.RegisterLazySingleton<IAgent>(() =>
            {
                var hub = services.GetService<Hub>();
                var dialer = services.GetService<Dialer>();
                return new Agent(hub, dialer);
            });
        }

        public static void AddMatrixSdk(this IMutableDependencyResolver services)
        {
            services.RegisterLazySingleton<IMatrixAgent>(() =>
            {
                var logger = services.GetService<ILogger>();
                var httpClient = services.GetService<HttpClient>();
                return new MatrixAgent(logger, httpClient);
            });
        }

        public static void AddPersistance(this IMutableDependencyResolver services)
        {
            services.RegisterLazySingleton<IStorage>(() => new Storage());

            services.RegisterLazySingleton<IFileLoader>(() =>
            {
                var agent = services.GetService<IAgent>();
                return new FileLoader(agent);
            });

            services.RegisterLazySingleton<IFileExplorer>(() =>
            {
                var platform = services.GetService<IPlatform>();
                return new FileExplorer(platform);
            });

            services.RegisterLazySingleton<IDatabaseContextFactory>(() => new DatabaseContextFactory());

            services.RegisterLazySingleton(() =>
            {
                var factory = services.GetService<IDatabaseContextFactory>();
                return factory.CreateDbContext();
            });

            services.RegisterLazySingleton<IKeyValueStorage>(() =>
            {
                var db = services.GetService<DatabaseContext>();
                return new KeyValueStorage(db);
            });
        }

        public static void AddServices(this IMutableDependencyResolver services)
        {
            // graphics
            services.RegisterLazySingleton<IColorMapper>(() => new ColorMapper());

            services.RegisterLazySingleton<IBitmapLoader>(() =>
            {
                var fileLoader = services.GetService<IFileLoader>();
                return new BitmapLoader(fileLoader);
            });

            // avatars
            services.RegisterLazySingleton<IAvatarCache>(() =>
            {
                var options = Options.Create(new MemoryCacheOptions
                {
                    SizeLimit = 128 // maximum 128 cached bitmaps
                });
                return new AvatarCache(new MemoryCache(options));
            });

            services.RegisterLazySingleton<IAvatarLoader>(() =>
            {
                var platform = services.GetService<IPlatform>();
                var storage = services.GetService<IStorage>();
                var fileLoader = services.GetService<IFileLoader>();
                var avatarCache = services.GetService<IAvatarCache>();
                var colorMapper = services.GetService<IColorMapper>();

                return new AvatarLoader(
                    platform,
                    storage,
                    fileLoader,
                    avatarCache,
                    colorMapper);
            });

            // previews
            services.RegisterLazySingleton<IPreviewCache>(() =>
            {
                var options = Options.Create(new MemoryCacheOptions
                {
                    SizeLimit = 16 // maximum 16 cached bitmaps
                });
                return new PreviewCache(new MemoryCache(options));
            });

            services.RegisterLazySingleton<IPreviewLoader>(() =>
            {
                var fileLoader = services.GetService<IFileLoader>();
                var previewCache = services.GetService<IPreviewCache>();

                return new PreviewLoader(
                    fileLoader,
                    previewCache);
            });

            // chats
            services.RegisterLazySingleton<IChatLoader>(() =>
            {
                var agent = services.GetService<IAgent>();
                return new ChatLoader(agent);
            });

            services.RegisterLazySingleton<IChatUpdater>(() =>
            {
                var agent = services.GetService<IAgent>();
                return new ChatUpdater(agent);
            });

            services.RegisterLazySingleton<IFeedLoader>(() =>
            {
                var agent = services.GetService<IAgent>();
                return new FeedLoader(agent);
            });

            // messages
            services.RegisterLazySingleton<IMessageLoader>(() =>
            {
                var agent = services.GetService<IAgent>();
                return new MessageLoader(agent);
            });
            services.RegisterLazySingleton<IMessageSender>(() =>
            {
                var agent = services.GetService<IAgent>();
                return new MessageSender(agent);
            });

            // notifications
            services.RegisterLazySingleton<INotificationSource>(() =>
            {
                var agent = services.GetService<IAgent>();
                return new NotificationSource(agent);
            });

            // users
            services.RegisterLazySingleton<IUserLoader>(() =>
            {
                var agent = services.GetService<IAgent>();
                return new UserLoader(agent);
            });

            // auth
            services.RegisterLazySingleton<IMatrixAuthenticator>(() =>
            {
                var agent = services.GetService<IMatrixAgent>();
                return new MatrixAuthenticator(agent);
            });

            // settings
            services.RegisterLazySingleton<IProxyManager>(() =>
            {
                var agent = services.GetService<IAgent>();
                return new ProxyManager(agent);
            });
        }

        public static void AddComponents(this IMutableDependencyResolver services)
        {
            services.RegisterLazySingleton<INotificationController>(() => new NotificationController());
            services.RegisterLazySingleton<IPopupController>(() => new PopupController());
        }

        public static void AddApplication(this IMutableDependencyResolver services)
        {
            services.RegisterLazySingleton(() =>
            {
                var application = new MainApplication();

                application.Initializing += (_, _) =>
                {
                    var db = services.GetService<DatabaseContext>();
                    db.Database.Migrate();

                    var hub = services.GetService<Hub>();
                    var task = Task.Factory.StartNew(
                        () => hub.Start(),
                        TaskCreationOptions.LongRunning);

                    task.ContinueWith(t =>
                    {
                        var exception = t.Exception;
                        if (exception != null)
                        {
                            // TODO: handle exception and shutdown
                        }
                    });
                };

                application.Disposing += (_, _) =>
                {
                    var hub = services.GetService<Hub>();
                    hub.Stop();
                };

                return application;
            });
        }

        public static void AddAuthentication(this IMutableDependencyResolver services)
        {
            //
        }

        public static void AddMessenger(this IMutableDependencyResolver services)
        {
            // messenger
            services.RegisterLazySingleton<IBasicMessageModelFactory>(() =>
            {
                return new BasicMessageModelFactory();
            });

            services.RegisterLazySingleton<INoteMessageModelFactory>(() =>
            {
                return new NoteMessageModelFactory();
            });

            services.RegisterLazySingleton<ISpecialMessageModelFactory>(() =>
            {
                var stringFormatter = new StringFormatter();
                return new SpecialMessageModelFactory(stringFormatter);
            });

            services.RegisterLazySingleton<IVisualMessageModelFactory>(() =>
            {
                return new VisualMessageModelFactory();
            });

            services.RegisterLazySingleton<IMessageModelFactory>(() =>
            {
                var basicMessageModelFactory = services.GetService<IBasicMessageModelFactory>();
                var noteMessageModelFactory = services.GetService<INoteMessageModelFactory>();
                var specialMessageModelFactory = services.GetService<ISpecialMessageModelFactory>();
                var visualMessageModelFactory = services.GetService<IVisualMessageModelFactory>();

                var stringFormatter = new StringFormatter();

                return new MessageModelFactory(
                    basicMessageModelFactory,
                    noteMessageModelFactory,
                    specialMessageModelFactory,
                    visualMessageModelFactory,
                    stringFormatter);
            });
        }

        public static void AddSettings(this IMutableDependencyResolver services)
        {
            //
        }

        public static void AddWorkspace(this IMutableDependencyResolver services)
        {
            //
        }
    }
}
