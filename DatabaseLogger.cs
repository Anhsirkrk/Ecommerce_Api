namespace Ecommerce_Api
{
    public class DatabaseLogger : ILogger

    {
        private readonly ExceptionLoggerService _exceptionLoggerService;
        private string _userId; // Added field to store the userId


        public DatabaseLogger(ExceptionLoggerService exceptionLoggerService)
        {
            _exceptionLoggerService = exceptionLoggerService;
        }
        public void SetUserId(string userId)
        {
            _userId = userId;
        }
        public IDisposable? BeginScope<TState>(TState state) 
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (exception != null)
            {
                _exceptionLoggerService.LogException(exception,_userId);
            }
        }

        
    }
}
