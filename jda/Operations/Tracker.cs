using Microsoft.Extensions.Logging;

namespace jda.Operations
{
    public static class Tracker
    {
        private static bool IsLogEnabled(ILogger logger, string message) => logger != null && !string.IsNullOrEmpty(message);

        public static void LogError(ILogger logger, string message) 
        {
            if (IsLogEnabled(logger, message)) logger.LogError(message);
        }

        public static void LogInformation(ILogger logger, string message) 
        {
            if (IsLogEnabled(logger, message)) logger.LogInformation(message);
        }
    }
}
