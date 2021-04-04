using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Smith.Model.Application;

namespace Smith.Views.Application
{
    public class MainWindow : BaseWindow<MainWindowModel>
    {
        public MainWindow() : base(false)
        {
            this.WhenActivated(disposables =>
            {
                this.BindNotifications()
                    .DisposeWith(disposables);
            });

            AvaloniaXamlLoader.Load(this);
            this.AttachDevTools();
        }
    }
}
