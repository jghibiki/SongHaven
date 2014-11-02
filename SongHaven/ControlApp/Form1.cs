using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SongHaven;
using SongPlayer;


namespace ControlApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string commandFile = @"C:\ProgramData\SongHaven\commands\commands.txt";

        private void button1_Click(object sender, EventArgs e)
        {
            AddCommand(Resources.RemoteCommand.PLAY_PAUSE);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddCommand(Resources.RemoteCommand.NEXT_SONG);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddCommand(Resources.RemoteCommand.VOLUME_UP);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddCommand(Resources.RemoteCommand.VOLUME_DOWN);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddCommand(Resources.RemoteCommand.VOLUME_MUTE);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddCommand(Resources.RemoteCommand.RESTART);
        }

        private void AddCommand(Resources.RemoteCommand commandId)
        {
            using (var db = new SongHavenEntities())
            {
                IList<Command> commandList = db.Commands.Where(x => x.int_id != null).ToList();
                foreach(Command command in commandList)
                {
                    db.Commands.Remove(command);
                }

                Command newCommand = new Command()
                {
                    int_command = (int) commandId
                };

                db.Commands.Add(newCommand);
                db.SaveChanges();
            }
        }
    }
}
