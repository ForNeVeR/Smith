using System.Reactive.Disposables;
using PropertyChanged;
using ReactiveUI;
using Smith.Services.Graphics.Avatars;
using Smith.Services.Messaging.Chats;

namespace Smith.Model.Messenger.Informer
{
    [AddINotifyPropertyChangedInterface]
    public class InformerModel : ISupportsActivation
    {
        public bool IsVisible { get; set; } = true;

        public string Title { get; set; }

        public string Label { get; set; }

        public Avatar Avatar { get; set; }

        public InformerModel(Chat chat)
        {
            this.WhenActivated(disposables =>
            {
                this.BindInformer(chat)
                    .DisposeWith(disposables);
            });
        }

        public InformerModel(Aggregate aggregate)
        {
            this.WhenActivated(disposables =>
            {
                this.BindInformer(aggregate)
                    .DisposeWith(disposables);
            });
        }

        private InformerModel()
        {
        }

        public static InformerModel Hidden()
        {
            return new InformerModel
            {
                IsVisible = false
            };
        }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}
