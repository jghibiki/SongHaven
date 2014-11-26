using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlayer
{
    public class Resources
    {
        public enum RemoteCommand
        {
            DO_NOTHING = 0,
            PLAY_PAUSE = 1,
            NEXT_SONG = 2,
            VOLUME_UP = 3,
            VOLUME_DOWN = 4,
            RESTART = 5
        }
    }
}
