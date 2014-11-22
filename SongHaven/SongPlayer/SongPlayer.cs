using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CSCore.XAudio2;
using SongHaven;
using SongPlayer.Logging;

namespace SongPlayer
{
    public class SongPlayer
    {
        #region Public Static Properties

        /// <summary>
        /// The song path template
        /// </summary>
        public static string SongPathTemplate = @"C:\ProgramData\SongHaven\music\{0}.{1}";


        /// <summary>
        /// The default log path template
        /// </summary>
        public static string DefaultLogPathTemplate = @"C:\ProgramData\SongHaven\logs\{0}.{1}";

        #endregion

        #region Private Static Properties

        private static bool m_isInitialized = false;    

        #endregion


        #region Properties

        private SongHavenEntities m_database;
        private ILoggingMethod m_logger;

        #endregion

        #region Constructor
        public SongPlayer(SongHavenEntities db, ILoggingMethod logger)
        {
            if(db == null) throw new ArgumentNullException("db");
            if(logger == null) throw new ArgumentNullException("logger");
            m_database = db;
        }
        #endregion

        #region Methods

        public void StartPlayer()
        {
            //only attempt to start a the player if on hasn't been started yet.
            if (!m_isInitialized)
            {
                m_isInitialized = true;

                //enter main loop
                while (true)
                {
                    
                    //check for any requests in the queue
                    Request nextRequest = GetRequests();

                    //if nextRequest is not null, then there was a song for us to play
                    if (nextRequest != null)
                    {
                        
                    }




                }

            }
        }

        /// <summary>
        /// Gets the requests.
        /// </summary>
        /// <returns>The next Request or null.</returns>
        private Request GetRequests()
        {
            return m_database.Requests.SingleOrDefault(x => x.dt_created_date == m_database.Requests.Max(y => y.dt_created_date));
        }

        private void PlaySong(Request currentRequest)
        {
            if(currentRequest == null) throw new ArgumentNullException("currentRequest");


        }

        #endregion

    }
}
