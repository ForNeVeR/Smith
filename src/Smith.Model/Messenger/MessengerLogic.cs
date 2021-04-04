using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Smith.Model.Messenger.Catalog;
using Smith.Model.Messenger.Catalog.Entries;
using Smith.Model.Messenger.Editor;
using Smith.Model.Messenger.Explorer;
using Smith.Model.Messenger.Homepage;
using Smith.Model.Messenger.Informer;
using Smith.Services.Messaging.Chats;
using Smith.Services.Utils.Reactive;

namespace Smith.Model.Messenger
{
    public static class MessengerLogic
    {
        public static IDisposable BindCatalog(this MessengerModel model, Section section)
        {
            model.CatalogModel = new CatalogModel(section);

            return Disposable.Empty;
        }

        public static IDisposable BindInformer(this MessengerModel model)
        {
            model.InformerModel = InformerModel.Hidden();

            return model.SubscribeToSelection(entry =>
            {
                switch (entry)
                {
                    case ChatEntryModel chatEntryModel:
                        model.InformerModel = new InformerModel(chatEntryModel.Chat);
                        break;

                    case AggregateEntryModel aggregateEntryModel:
                        model.InformerModel = new InformerModel(aggregateEntryModel.Aggregate);
                        break;

                    case HomeEntryModel _:
                        model.InformerModel = InformerModel.Hidden();
                        break;
                }
            });
        }

        public static IDisposable BindExplorer(this MessengerModel model)
        {
            model.ExplorerModel = ExplorerModel.Hidden();

            return model.SubscribeToSelection(entry =>
            {
                switch (entry)
                {
                    case ChatEntryModel chatEntryModel:
                        model.ExplorerModel = new ExplorerModel(chatEntryModel.Chat);
                        break;

                    case AggregateEntryModel aggregateEntryModel:
                        //model.ExplorerModel = new ExplorerModel(aggregateEntryModel.Aggregate);
                        break;

                    case HomeEntryModel _:
                        model.ExplorerModel = ExplorerModel.Hidden();
                        break;
                }
            });
        }

        public static IDisposable BindHome(this MessengerModel model)
        {
            model.HomepageModel = HomepageModel.Hidden();

            return model.SubscribeToSelection(entry =>
            {
                switch (entry)
                {
                    case HomeEntryModel _:
                        model.HomepageModel = new HomepageModel();
                        break;

                    default:
                        model.HomepageModel = HomepageModel.Hidden();
                        break;
                }
            });
        }

        public static IDisposable BindEditor(this MessengerModel model)
        {
            model.EditorModel = EditorModel.Hidden();

            return model.SubscribeToSelection(entry =>
            {
                switch (entry)
                {
                    case ChatEntryModel chatEntryModel:
                        model.EditorModel = new EditorModel(chatEntryModel.Chat);
                        break;

                    default:
                        model.EditorModel = EditorModel.Hidden();
                        break;
                }
            });
        }

        private static IDisposable SubscribeToSelection(this MessengerModel model, Action<EntryModel> action)
        {
            return model.WhenAnyValue(ctx => ctx.CatalogModel.SelectedEntry)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Accept(entry =>
                {
                    if (entry != null)
                    {
                        action(entry);
                    }
                });
        }
    }
}
