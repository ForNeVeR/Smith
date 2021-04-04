using System;
using Microsoft.Extensions.Logging;

namespace Smith
{
    public class LoggerExceptionHandler : IObserver<Exception>
    {
        private readonly ILogger _logger;

        public LoggerExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }


        public void OnError(Exception error)
        {
            _logger.LogError(error, "Error during error processing");
        }

        public void OnNext(Exception value)
        {
            _logger.LogError(value, "Application error");
        }

        public void OnCompleted()
        {
        }
    }
}
