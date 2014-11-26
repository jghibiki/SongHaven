using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using SongHaven;
using SongPlayer.Logging;

namespace SongPlayer.SongPlayer
{
    public class SongPlayer : ISongPlayer
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
        private AudioFileStreamWrapper m_AudioFileStreamWrapper;

        #endregion

        #region Constructor
        public SongPlayer(SongHavenEntities db, ILoggingMethod logger)
        {
            if(db == null) throw new ArgumentNullException("db");
            if(logger == null) throw new ArgumentNullException("logger");
            m_database = db;
            m_logger = logger;
        }
        #endregion

        #region Methods

        #region Public Methods

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

                        //play the next song
                        PlaySong(nextRequest);


                        bool breakOut = false;
                        while (m_AudioFileStreamWrapper.SongElapsedTime.Elapsed < m_AudioFileStreamWrapper.SongLength)
                        {
                            Thread.Sleep(100);
                            Resources.RemoteCommand command = GetCommand();

                            switch (command)
                            {
                                case Resources.RemoteCommand.DO_NOTHING:
                                    break;
                                case Resources.RemoteCommand.NEXT_SONG:
                                    breakOut = true;
                                    break;
                                case Resources.RemoteCommand.PLAY_PAUSE:
                                    switch (m_AudioFileStreamWrapper.PlaybackState)
                                    {
                                        case AudioFileStreamWrapper.PlaybackStates.PLAYING:
                                            m_AudioFileStreamWrapper.Pause();
                                            break;
                                        case AudioFileStreamWrapper.PlaybackStates.PAUSED:
                                            m_AudioFileStreamWrapper.Play();
                                            break;
                                    }
                                    break;
                                case Resources.RemoteCommand.VOLUME_UP:
                                    m_logger.LogDebug("Volume Up");
                                    m_AudioFileStreamWrapper.SetVolume(m_AudioFileStreamWrapper.GetVolume() + 0.1f);
                                    break;
                                case Resources.RemoteCommand.VOLUME_DOWN:
                                    m_logger.LogDebug("Volume Down");
                                    m_AudioFileStreamWrapper.SetVolume(m_AudioFileStreamWrapper.GetVolume() - 0.1f);
                                    break;
                                case Resources.RemoteCommand.RESTART:
                                    Restart();
                                    break;
                            }

                            if (breakOut) break; // breaks out of loop if the song is done.
                        }
                        if (breakOut) m_logger.LogInfo("Skipped Song.");
                        else if (!breakOut) m_logger.LogInfo("Finished playing song.");

                        RemoveRequest(nextRequest);

                    }
                    else
                    {
                        Resources.RemoteCommand command = GetCommand();
                        if (command == Resources.RemoteCommand.RESTART)
                        {
                            Restart();
                        }
                        Thread.Sleep(100);
                    }




                }

            }
        }

        #endregion

        #region Private Methods

        private void Restart()
        {
            System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);
            Environment.Exit(0);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Removes the request.
        /// </summary>
        /// <param name="currentRequest">The current request.</param>
        protected void RemoveRequest(Request currentRequest)
        {
            m_logger.LogDebug("Removing request.");
            m_database.Requests.Remove(currentRequest);
            m_database.SaveChanges();
            m_logger.LogDebug("Request removed successully.");
        }

        /// <summary>
        /// Plays the song.
        /// </summary>
        /// <param name="currentRequest">The current request.</param>
        /// <exception cref="System.ArgumentNullException">currentRequest</exception>
        protected void PlaySong(Request currentRequest)
        {
            Song currentSong = GetSong(currentRequest.fk_song);
            m_logger.LogInfo("Trying to play song: " + currentSong.nvc_title + " by: " + currentSong.nvc_artist);

            string audioFile = string.Format(SongPathTemplate, currentRequest.Song.guid_id,
                currentRequest.Song.nvc_file_type);

            m_logger.LogDebug("Creating audio file stream");
            m_AudioFileStreamWrapper = new AudioFileStreamWrapper(audioFile, m_logger);

            m_logger.LogDebug("Playing song.");
            m_AudioFileStreamWrapper.Play();

        }

        protected Song GetSong(Guid songIdGuid)
        {
            Song song = m_database.Songs.FirstOrDefault(x => x.guid_id == songIdGuid);
            if (song == null)
            {
                m_logger.LogWarn("Failed to retrieve song from database");
            }
            return song;
        }

        /// <summary>
        /// Gets the requests.
        /// </summary>
        /// <returns></returns>
        protected Request GetRequests()
        {
            return m_database.Requests.SingleOrDefault(x => x.dt_created_date == m_database.Requests.Max(y => y.dt_created_date));
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <returns></returns>
        protected Resources.RemoteCommand GetCommand()
        {
            Resources.RemoteCommand command;

            Command retrievedCommand = m_database.Commands.FirstOrDefault(x => true);
            if (retrievedCommand != null)
            {
                command = (Resources.RemoteCommand)retrievedCommand.int_command;
                m_database.Commands.Remove(retrievedCommand);
                m_database.SaveChanges();
            }
            else
            {
                command = Resources.RemoteCommand.DO_NOTHING;
            }

            
            return command;
        }

        /// <summary>
        /// Clears the command queue.
        /// </summary>
        protected void ClearCommandQueue()
        {
            IList<Command> commandList = m_database.Commands.Where(x => true).ToList();
            foreach (Command command in commandList)
            {
                m_database.Commands.Remove(command);
            }
            m_database.SaveChanges();
        }


        #endregion

        #endregion

    }
}
