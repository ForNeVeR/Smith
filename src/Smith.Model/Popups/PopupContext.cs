﻿using System.Reactive;
using PropertyChanged;
using ReactiveUI;

namespace Smith.Model.Popups
{
    [AddINotifyPropertyChangedInterface]
    public class PopupContext
    {
        public string Title { get; set; }

        public ReactiveCommand<Unit, Unit> CloseCommand { get; }

        public PopupContext()
        {
            CloseCommand = ReactiveCommand.Create(() => { }, null, RxApp.MainThreadScheduler);
        }
    }
}
