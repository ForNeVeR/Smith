using System;
using Avalonia;
using Avalonia.Markup.Xaml;

namespace Smith.Application
{
    public class MainApplication : Avalonia.Application
    {
        public event EventHandler? Initializing;

        public event EventHandler? Disposing;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            Initializing?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnExiting(object sender, EventArgs e)
        {
            Disposing?.Invoke(this, EventArgs.Empty);

            base.OnExiting(sender, e);
        }
    }
}
