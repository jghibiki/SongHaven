using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSCore;
using NAudio.Midi;
using NAudio.Wave;
using SongHaven;



namespace SongPlayer
{
    class Program
    {
        private static bool isPlaying = false;
        private static bool skipSong = false;
        private static IWavePlayer wavePlayer;
        private static TimeSpan totalPlayTime;
        private static Stopwatch stopWatch;
        private static float volume = 0.75f;
        private static bool isMuted = false;
        private static AudioFileReader audioFileReader;
        private static int waitConter = 0;

        static void Main(string[] args)
        {
            using (SongHavenEntities db = new SongHavenEntities())
            {
                InsertTestData(db);
                

                while (true)
                {
                    Request nextRequest = db.Requests.SingleOrDefault(x => x.dt_created_date == db.Requests.Max(y => y.dt_created_date));

                    if (nextRequest != null)
                    {

                        playSong(nextRequest);
                        
                        
                        while ((stopWatch.Elapsed < totalPlayTime) && (isPlaying) && skipSong == false)
                        {
                            Thread.Sleep(1000);

                            Resources.RemoteCommand command  = GetCommand();
                            if (command != 0)
                            {
                                switch (command)
                                {
                                    case Resources.RemoteCommand.DO_NOTHING:
                                        ; // do nothing
                                        break;
                                    case Resources.RemoteCommand.PLAY_PAUSE:
                                        if (wavePlayer.PlaybackState == PlaybackState.Playing)
                                        {
                                            Console.WriteLine("Pausing...");
                                            stopWatch.Stop();
                                            wavePlayer.Pause();
                                            Console.WriteLine("Paused");
                                            WaitForContinue();
                                        }
                                        break;
                                    case Resources.RemoteCommand.NEXT_SONG:
                                        skipSong = true;
                                        Console.WriteLine("Skiping song...");
                                        break;
                                    case Resources.RemoteCommand.VOLUME_DOWN:
                                        if (!isMuted)
                                        {
                                            volume -= 0.1f;
                                            audioFileReader.Volume = volume;
                                            Console.WriteLine("Volume decreased to " + volume.ToString());
                                        }
                                        else
                                        {
                                            Console.WriteLine("Volume is muted, volume not decreased.");
                                        }
                                        break;
                                    case Resources.RemoteCommand.VOLUME_UP:
                                        if (!isMuted)
                                        {
                                            volume += 0.1f;
                                            audioFileReader.Volume = volume;
                                            Console.WriteLine("Volume increased to " + volume.ToString());
                                        }
                                        else
                                        {
                                            Console.WriteLine("Volume is muted, volume not increased.");
                                        }
                                        break;
                                    case Resources.RemoteCommand.VOLUME_MUTE:
                                        if (!isMuted)
                                        {
                                            isMuted = true;
                                            audioFileReader.Volume = 0f;
                                        }
                                        else
                                        {
                                            isMuted = false;
                                            audioFileReader.Volume = volume;
                                        }
                                        break;
                                    case Resources.RemoteCommand.RESTART:
                                        System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);
                                        Environment.Exit(0);
                                        break;

                                }
                            }

                        }
                        Console.WriteLine("Song finished Finished!");
                        


                        db.Requests.Remove(nextRequest);
                        db.SaveChanges();
                    }
                    else
                    {
                        Resources.RemoteCommand command  = GetCommand();

                        switch (command)
                        {
                            case Resources.RemoteCommand.DO_NOTHING:
                                if (waitConter > 3)
                                {
                                    Console.WriteLine("Waiting..."); 
                                }
                                break;
                            case Resources.RemoteCommand.VOLUME_DOWN:
                                if (!isMuted)
                                {
                                    volume -= 0.1f;
                                    audioFileReader.Volume = volume;
                                    Console.WriteLine("Volume decreased to " + volume.ToString());
                                }
                                else
                                {
                                    Console.WriteLine("Volume is muted, volume not decreased.");
                                }
                                break;
                            case Resources.RemoteCommand.VOLUME_UP:
                                if (!isMuted)
                                {
                                    volume += 0.1f;
                                    audioFileReader.Volume = volume;
                                    Console.WriteLine("Volume increased to " + volume.ToString());
                                }
                                else
                                {
                                    Console.WriteLine("Volume is muted, volume not increased.");
                                }
                                break;
                            case Resources.RemoteCommand.VOLUME_MUTE:
                                if (!isMuted)
                                {
                                    isMuted = true;
                                    audioFileReader.Volume = 0f;
                                    Console.WriteLine("Volume Muted.");
                                }
                                else
                                {
                                    isMuted = false;
                                    audioFileReader.Volume = volume;
                                    Console.WriteLine("Volume Unmuted.");
                                }
                                break;
                            case Resources.RemoteCommand.RESTART:
                                System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);
                                Environment.Exit(0);
                                break;

                        }
                        Thread.Sleep(100);
                        
                    }


                }
            }
        }

        private static void playSong(Request nextRequest)
        {
            Console.WriteLine("Playing song: " + nextRequest.Song.nvc_title);
            string audioFile = string.Format(@"C:\ProgramData\SongHaven\music\{0}.{1}", nextRequest.Song.guid_id, nextRequest.Song.nvc_file_type);

            wavePlayer = new WaveOut();
            audioFileReader = new AudioFileReader(audioFile);
            audioFileReader.Volume = !isMuted ? volume : 0f;

            wavePlayer.Init(audioFileReader);
            totalPlayTime = audioFileReader.TotalTime;
            stopWatch = new Stopwatch();
            stopWatch.Start();
            wavePlayer.Play();
            isPlaying = true;
            Console.WriteLine("Playing...");
        }

        private static void WaitForContinue()
        {
            while (wavePlayer.PlaybackState == PlaybackState.Paused && skipSong == false) //keep waiting for a new command until we get a command that either starts 
            {
                Resources.RemoteCommand command = GetCommand();

                switch (command)
                {
                    case Resources.RemoteCommand.DO_NOTHING:
                        Console.WriteLine("Waiting for command...");
                        break;
                    case Resources.RemoteCommand.PLAY_PAUSE:
                        wavePlayer.Play();
                        stopWatch.Start();
                        Console.WriteLine("Resuming Playback...");
                        break;
                    case Resources.RemoteCommand.NEXT_SONG:
                        skipSong = true;
                        Console.WriteLine("Skiping song...");
                        break;
                    case Resources.RemoteCommand.VOLUME_DOWN:
                        if (!isMuted)
                        {
                            volume -= 0.1f;
                            audioFileReader.Volume = volume;
                            Console.WriteLine("Volume decreased to " + volume.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Volume is muted, volume not decreased.");
                        }
                        break;
                    case Resources.RemoteCommand.VOLUME_UP:
                        if (!isMuted)
                        {
                            volume += 0.1f;
                            audioFileReader.Volume = volume;
                            Console.WriteLine("Volume increased to " + volume.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Volume is muted, volume not increased.");
                        }
                        break;
                    case Resources.RemoteCommand.VOLUME_MUTE:
                        if (!isMuted)
                        {
                            isMuted = true;
                            audioFileReader.Volume = 0f;
                        }
                        else
                        {
                            isMuted = false;
                            audioFileReader.Volume = volume;
                        }
                        break;
                    case Resources.RemoteCommand.RESTART:
                        System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                        break;
                }
                command = Resources.RemoteCommand.DO_NOTHING;

            }
            
        }


        

        public static Resources.RemoteCommand GetCommand()
        {
            
            Resources.RemoteCommand command;


            using (SongHavenEntities db = new SongHavenEntities())
            {
                Command retrievedCommand = db.Commands.FirstOrDefault(x => x.int_id != null);
                if (retrievedCommand != null)
                {
                    command = (Resources.RemoteCommand) retrievedCommand.int_command;

                    IList<Command> commandList = db.Commands.Where(x => x.int_id != null).ToList();
                    foreach (Command commandToRemove in commandList)
                    {
                        db.Commands.Remove(commandToRemove);
                    }
                    db.SaveChanges();
                }
                else
                {
                    command = Resources.RemoteCommand.DO_NOTHING;
                }
            }

            return command;
            
        }


        private static void InsertTestData(SongHavenEntities db)
        {

            try
            {
                db.Requests.Remove(db.Requests.Find(new Guid("645C6274-4EB7-438A-AF27-491E5FFA58F7")));
                db.SaveChanges();
            }
            catch { Console.WriteLine("Failed to remove Requests");}
            try
            {
                db.Songs.Remove(db.Songs.Find(new Guid("0F3C3012-1960-4214-A576-C3A83CC61514")));
                db.SaveChanges();
            }
            catch { Console.WriteLine("Failed to remove Songs"); }
            try
            {


                db.Users.Remove(db.Users.Find(new Guid("9EA72CDD-9DF8-4874-8D1F-BDE787AC29F0")));
                db.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Failed to remove Users");
            }

            Console.WriteLine("Setting up test env...");
            Song previousTestSong = db.Songs.Find(new Guid("0F3C3012-1960-4214-A576-C3A83CC61514"));
            if (previousTestSong != null)
            {
                Console.WriteLine("Removing previous test song...");
                db.Songs.Remove(previousTestSong);
            }
            
            
            Song testSong = new Song()
            {
                nvc_title = "testSong",
                nvc_album = "testAlbum",
                nvc_artist = "testArtist",
                dt_created_date = DateTime.UtcNow,
                dt_last_played_date = null,
                guid_id = new Guid("0F3C3012-1960-4214-A576-C3A83CC61514"),
                int_number_of_plays = 0,
                nvc_file_type = "wav"
            };

            Console.WriteLine("Adding test song...");
            testSong = db.Songs.Add(testSong);
            

            User testUser = new User()
            {
                guid_id = new Guid("9EA72CDD-9DF8-4874-8D1F-BDE787AC29F0"),
                nvc_username = "test",
                nvc_password = "123",
                nvc_email = "test@test.com",
                nvc_first_name = "test",
                nvc_last_name = "tester",
                int_account_strikes = 0,
                dt_created_date = DateTime.UtcNow,
                dt_date_banned = null
            };

            Console.WriteLine("Adding test user...");
            testUser = db.Users.Add(testUser);

            Request testRequest = new Request()
            {
                Song = testSong,
                User = testUser,
                guid_id = new Guid("645C6274-4EB7-438A-AF27-491E5FFA58F7"),
                dt_created_date = DateTime.UtcNow,
            };


            Console.WriteLine("Adding test request...");
            testRequest = db.Requests.Add(testRequest);


            Console.WriteLine("Saving DB");
            db.SaveChanges();

            Console.WriteLine("Finished setting up test env...");
        }



    }


}
