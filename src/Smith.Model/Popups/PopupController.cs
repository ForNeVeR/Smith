using System.Reactive.Subjects;

namespace Smith.Model.Popups
{
    public class PopupController : IPopupController
    {
        public Subject<PopupContext> Trigger { get; } = new Subject<PopupContext>();

        public void Show(PopupContext context)
        {
            Trigger.OnNext(context);
        }

        public void Hide()
        {
            Trigger.OnNext(null);
        }
    }
}
