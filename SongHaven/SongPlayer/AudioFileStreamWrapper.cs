using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using SongPlayer.Logging;


namespace SongPlayer
{
    class AudioFileStreamWrapper
    {
        #region Public Properties

        /// <summary>
        /// The song length
        /// </summary>
        public TimeSpan SongLength;
        /// <summary>
        /// The how much of the song has been played.
        /// </summary>
        public Stopwatch SongElapsedTime;
        /// <summary>
        /// The playback state
        /// </summary>
        public PlaybackStates PlaybackState;

        /// <summary>
        /// Available playback states
        /// </summary>
        public enum PlaybackStates
        {
            PAUSED = 0,
            PLAYING = 1,
        }

        #endregion  

        #region Private Properties

        private IWavePlayer m_WaveOut;
        private AudioFileReader m_AudioFileReader;
        private bool m_StartedSong;
        private ILoggingMethod m_Logger;
        

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileStreamWrapper"/> class.
        /// </summary>
        /// <param name="audioFile">The audio file.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="System.ArgumentNullException">
        /// audioFile
        /// or
        /// logger
        /// </exception>
        public AudioFileStreamWrapper(string audioFile, ILoggingMethod logger)
        {
            if (string.IsNullOrEmpty(audioFile)) throw new ArgumentNullException("audioFile");
            if(logger == null) throw new ArgumentNullException("logger");

            m_Logger = logger;
            Initialize(audioFile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileStreamWrapper"/> class.
        /// </summary>
        /// <param name="audioFile">The audio file.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="volume">The volume.</param>
        /// <exception cref="System.ArgumentNullException">
        /// audioFile
        /// or
        /// logger
        /// </exception>
        public AudioFileStreamWrapper(string audioFile, ILoggingMethod logger, float volume)
        {
            if(string.IsNullOrEmpty(audioFile)) throw new ArgumentNullException("audioFile");
            if(logger == null) throw new ArgumentNullException("logger");
          
            Initialize(audioFile);
            m_AudioFileReader.Volume = volume;
        }


        #endregion

        #region Methods

        /// <summary>
        /// Plays this instance.
        /// </summary>
        public void Play()
        {
            m_Logger.LogInfo("Playing song.");
            if (m_StartedSong == false)
            {
                m_Logger.LogDebug("Attempting to play song for the first time.");
                try
                {
                    SongElapsedTime.Start();
                    m_WaveOut.Play();
                    PlaybackState = PlaybackStates.PLAYING;
                }
                catch(Exception e)
                {
                    m_Logger.LogError("Failed to start song.");
                    throw;
                }
            }
            else if (PlaybackState == PlaybackStates.PAUSED)
            {
                m_Logger.LogDebug("Attempting to resume song.");
                try
                {
                    SongElapsedTime.Start();
                    m_WaveOut.Play();
                    PlaybackState = PlaybackStates.PLAYING;
                }
                catch (Exception)
                {
                    m_Logger.LogError("Failed to resume song.");
                    throw;
                }
            }
            else
            {
                m_Logger.LogDebug("Could not start song as it is already playing.");
            }

            m_Logger.LogInfo("Playback state is now:" + PlaybackState.ToString());
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            m_Logger.LogInfo("Pausing song.");
            if (PlaybackState == PlaybackStates.PLAYING)
            {
                m_Logger.LogDebug("Attempting to pause song.");

                try
                {
                    m_WaveOut.Pause();
                    SongElapsedTime.Stop();
                    PlaybackState = PlaybackStates.PAUSED;
                }
                catch (Exception)
                {
                    m_Logger.LogError("Failed to pause song.");
                    throw;
                }
            }
            m_Logger.LogInfo("Playback state is now:" + PlaybackState.ToString());
        }

        /// <summary>
        /// Gets the volume.
        /// </summary>
        /// <returns></returns>
        public float GetVolume()
        {
            return m_AudioFileReader.Volume;
        }

        /// <summary>
        /// Sets the volume.
        /// </summary>
        /// <param name="volume">The volume.</param>
        public void SetVolume(float volume)
        {
            m_AudioFileReader.Volume = volume;
        }

        #endregion

        #region Private Methods

        private void Initialize(string audioFile)
        {
            m_Logger.LogDebug("Beginning to initialize song output interface for song:" + audioFile);
            m_AudioFileReader = new AudioFileReader(audioFile);
            m_WaveOut = new WaveOut();
            m_WaveOut.Init(m_AudioFileReader);
            SongLength = m_AudioFileReader.TotalTime;
            SongElapsedTime = new Stopwatch();
            m_Logger.LogDebug("Sucessfully initialized song output interface");
        }

        #endregion
    }
}
