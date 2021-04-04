﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Smith.Model.Authentication;

namespace Tel.Egram.Views.Authentication
{
    public class AuthenticationControl : BaseControl<AuthenticationModel>
    {
        public AuthenticationControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
