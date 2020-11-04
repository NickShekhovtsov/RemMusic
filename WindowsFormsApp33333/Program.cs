using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WMPLib;

namespace WindowsFormsApp33333
{
    
    static class Program
    {    
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        
        static void Main()
        {


            SockedManager client = new SockedManager();

            //switch (client.msg)
            //{
            if (client.msg == "play")
            {
                WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = @"H:\Music\Top50 Tracks Deep House Ver.20 (2020)\50.  Jay Aliyev  -  Move On.mp3";
                wplayer.controls.play();
                wplayer.settings.volume = 55;
            }



            if (client.msg == "play2")
            {
                WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = @"H:\Music\Top50 Tracks Deep House Ver.20 (2020)\32.  Jack Flex  -  Deep Inside Me (Ocean Drive Mix).mp3";
                wplayer.controls.play();
                wplayer.settings.volume = 50;
            }
            //break;

            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
