using Ecommerce_Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api
{
    public class ExceptionLoggerService
    {
        private readonly EcommerceDailyPickContext _context;

        public ExceptionLoggerService(EcommerceDailyPickContext context)
        {
            _context = context;
        }

        public void LogException(Exception ex,string userId)
        {
            var exceptionLog = new ExceptionLog
            {

                Timestamp = DateTime.UtcNow,
                UserId = userId,
                ExceptionMessage = ex.Message ?? "No message available",
                StackTrace = ex.StackTrace ?? "No stack trace available",
                ErrorCode = ex.GetHashCode(), // GetHashCode() should not return null
                Severity = ex.Source ?? "No source available",
                Data = ex.Data?.ToString() ?? "No data available", // Check for null before ToString()
                HelpLink = ex.HelpLink ?? "No help link available",
                InnerException = ex.InnerException?.ToString() ?? "No inner exception available", // Check for null before ToString()
                Source = ex.Source ?? "No source available",
                TargetSite = ex.TargetSite?.ToString() ?? "No target site available" // Check for null before ToString()


                // Add more fields as needed
            };

            _context.ExceptionLogs.Add(exceptionLog);
            _context.SaveChanges();

        }
    }
}
