using System.Reactive.Disposables;
using DynamicData.Binding;
using PropertyChanged;
using ReactiveUI;
using Smith.Model.Messenger.Explorer.Messages;

namespace Smith.Model.Messenger.Homepage
{
    [AddINotifyPropertyChangedInterface]
    public class HomepageModel : ISupportsActivation
    {
        public bool IsVisible { get; set; } = true;

        public string SearchText { get; set; }

        public ObservableCollectionExtended<MessageModel> PromotedMessages { get; set; }

        public HomepageModel()
        {
//            this.WhenActivated(disposables =>
//            {
//
//            });
        }

        public ViewModelActivator Activator => new ViewModelActivator();

        public static HomepageModel Hidden()
        {
            return new HomepageModel
            {
                IsVisible = false
            };
        }
    }
}
