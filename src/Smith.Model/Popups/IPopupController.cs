namespace Smith.Model.Popups
{
    public interface IPopupController
    {
        void Show(PopupContext context);
        void Hide();
    }
}
