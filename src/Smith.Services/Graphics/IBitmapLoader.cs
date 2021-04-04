using System;
using Avalonia.Media.Imaging;
using Smith.Services.Persistance;
using TdLib;

namespace Smith.Services.Graphics
{
    public interface IBitmapLoader
    {
        IObservable<IBitmap> LoadFile(TdApi.File file, LoadPriority priority);
    }
}