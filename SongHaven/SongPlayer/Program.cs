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
using SongPlayer.Logging;


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
            ILoggingMethod logger = new ConsoleLogger("SongHavenLog");
            using (SongHavenEntities db = new SongHavenEntities())
            {
                InsertTestData(db);

                SongPlayer songPlayer = new SongPlayer(db, logger);
                songPlayer.StartPlayer();


            }
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
