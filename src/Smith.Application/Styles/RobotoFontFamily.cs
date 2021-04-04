using System;
using Avalonia.Media;

namespace Smith.Application.Styles
{
    /// <summary>
    /// Temp workaround for using fonts in styles
    /// </summary>
    public class RobotoFontFamily : FontFamily
    {
        public RobotoFontFamily()
            : base ("Roboto", new Uri("resm:Smith.Application.Fonts?assembly=Smith.Application"))
        {
        }
    }
}