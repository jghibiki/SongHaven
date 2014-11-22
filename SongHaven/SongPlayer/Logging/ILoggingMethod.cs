using System;

namespace SongPlayer.Logging
{
    public interface ILoggingMethod : IDisposable
    {


        /// <summary>
        /// Gets the type of the log.
        /// </summary>
        /// <returns></returns>
        string GetLogType();

        
        /// <summary>
        /// Sets the logging level.
        /// </summary>
        /// <param name="loggingLevel">The logging level. 0 - debug, 1 - warn, 2 - info, 3- error</param>
        void SetLoggingLevel(int loggingLevel);



        /// <summary>
        /// Logs the error message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void LogError(string msg);

       
        /// <summary>
        /// Logs the information message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void LogInfo(string msg);

        
        /// <summary>
        /// Logs the warn message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void LogWarn(string msg);


        /// <summary>
        /// Logs the debug message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void LogDebug(string msg);


    }
}
