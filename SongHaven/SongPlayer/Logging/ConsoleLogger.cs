using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlayer.Logging
{
    class ConsoleLogger : ILoggingMethod
    {

        #region Properties

        /// <summary>
        /// The logger name
        /// </summary>
        private readonly string m_LoggerName;
        /// <summary>
        /// The m_ logging level
        /// </summary>
        private int m_LoggingLevel = 0;

        #endregion

        #region Methods


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class.
        /// </summary>
        /// <param name="loggerName">Name of the logger.</param>
        /// <exception cref="System.ArgumentNullException">Parameter loggerName cannot be null or empty.</exception>
        public ConsoleLogger(string loggerName)
        {
            if (string.IsNullOrEmpty(loggerName)) throw new ArgumentNullException("Parameter loggerName cannot be null or empty.");
            m_LoggerName = loggerName;
            m_LoggingLevel = 2;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class.
        /// </summary>
        /// <param name="loggerName">Name of the logger.</param>
        /// <param name="loggingLevel">The logging level.</param>
        /// <exception cref="System.ArgumentNullException">Parameter loggerName cannot be null or empty.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter loggingLevel only takes integer values 0 through 3 as valid inputs.</exception>
        public ConsoleLogger(string loggerName, int loggingLevel)
        {
            if (string.IsNullOrEmpty(loggerName)) throw new ArgumentNullException("Parameter loggerName cannot be null or empty.");
            if (loggingLevel < 0 || loggingLevel > 3) throw new ArgumentOutOfRangeException("Parameter loggingLevel only takes integer values 0 through 3 as valid inputs.");

            m_LoggerName = loggerName;
            m_LoggingLevel = loggingLevel;
        }

        #endregion

        #region Implementation of ILoggingMethod

        /// <summary>
        /// Gets the type of the log.
        /// </summary>
        /// <returns></returns>
        public string GetLogType()
        {
            return "console";
        }

        /// <summary>
        /// Sets the logging level.
        /// </summary>
        /// <param name="loggingLevel">The logging level.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter loggingLevel only takes integer values 0 through 3 as valid inputs.</exception>
        public void SetLoggingLevel(int loggingLevel)
        {
            if (loggingLevel < 0 || loggingLevel > 3) throw new ArgumentOutOfRangeException("Parameter loggingLevel only takes integer values 0 through 3 as valid inputs.");
            m_LoggingLevel = loggingLevel;
        }




        /// <summary>
        /// Logs the error message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public void LogError(string msg)
        {
            if (m_LoggingLevel == 3)
            {
                Console.WriteLine(GetTimeStamp() + msg);
            }
        }

        public void LogInfo(string msg)
        {
            if (m_LoggingLevel >= 2)
            {
                Console.WriteLine(GetTimeStamp() + msg);
            }
        }

        public void LogWarn(string msg)
        {
            if (m_LoggingLevel >= 1)
            {
                Console.WriteLine(GetTimeStamp() + msg);
            }
        }

        public void LogDebug(string msg)
       {
            if (m_LoggingLevel >= 0)
            {
                Console.WriteLine(GetTimeStamp() + msg);
            }
        }

        #endregion

        #region Implementation of IDisposeable

        public void Dispose()
        {
            //do nothing as we don't need to worry about closing a file stream when writing to the console.
        }

        #endregion

        #endregion

        #region Private Methods

        private string GetTimeStamp()
        {
            string timestamp = "";
            timestamp += "[";
            timestamp += m_LoggerName;
            timestamp += " ::- ";
            timestamp += DateTime.Now.ToShortDateString();
            timestamp += "] ";
            return timestamp;
        }

        #endregion
    }
}
