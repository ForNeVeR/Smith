using System;
using TdLib;

namespace Smith.Services.Persistance
{
    public interface IFileLoader
    {
        IObservable<TdApi.File> LoadFile(TdApi.File file, LoadPriority priority);
    }
}