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

        static void Main(string[] args)
        {
            ILoggingMethod logger = new ConsoleLogger("SongHavenLog");
            using (SongHavenEntities db = new SongHavenEntities())
            {

                SongPlayer songPlayer = new SongPlayer(db, logger);
                songPlayer.StartPlayer();


            }
        }
    }


}
