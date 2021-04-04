using System;
using System.Reactive.Disposables;
using DynamicData;
using DynamicData.Binding;
using PropertyChanged;
using ReactiveUI;
using Smith.Model.Messenger.Explorer.Items;
using Smith.Model.Messenger.Explorer.Loaders;
using Smith.Services.Messaging.Chats;
using Smith.Services.Utils.Reactive;
using Range = Smith.Services.Utils.Range;

namespace Smith.Model.Messenger.Explorer
{
    [AddINotifyPropertyChangedInterface]
    public class ExplorerModel : ISupportsActivation
    {
        public bool IsVisible { get; set; } = true;

        public Range VisibleRange { get; set; }

        public ItemModel TargetItem { get; set; }

        public ObservableCollectionExtended<ItemModel> Items { get; set; }
            = new ObservableCollectionExtended<ItemModel>();

        public SourceList<ItemModel> SourceItems { get; set; }
            = new SourceList<ItemModel>();

        public ExplorerModel(Chat chat)
        {
            this.WhenActivated(disposables =>
            {
                BindSource()
                    .DisposeWith(disposables);

                var conductor = new MessageLoaderConductor();

                new InitMessageLoader(conductor)
                    .Bind(this, chat)
                    .DisposeWith(disposables);

                new NextMessageLoader(conductor)
                    .Bind(this, chat)
                    .DisposeWith(disposables);

                new PrevMessageLoader(conductor)
                    .Bind(this, chat)
                    .DisposeWith(disposables);
            });
        }

        private IDisposable BindSource()
        {
            return SourceItems.Connect()
                .Bind(Items)
                .Accept();
        }

        private ExplorerModel()
        {
        }

        public static ExplorerModel Hidden()
        {
            return new ExplorerModel
            {
                IsVisible = false
            };
        }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
