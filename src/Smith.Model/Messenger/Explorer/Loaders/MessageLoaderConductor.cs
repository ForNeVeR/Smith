namespace Smith.Model.Messenger.Explorer.Loaders
{
    public class MessageLoaderConductor
    {
        public object Locker { get; } = new object();

        public bool IsBusy { get; set; }
    }
}
