using System;

namespace Smith.Services.Utils.Formatting
{
    public interface IStringFormatter
    {
        string FormatShortTime(DateTimeOffset dateTimeOffset);

        string FormatShortTime(int timestamp);

        string FormatMemorySize(long bytes);
    }
}