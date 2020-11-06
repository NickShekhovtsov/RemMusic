using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace WindowsFormsApp33333
{
    class SockedManager
    {
        public Socket client { get; set; }
        public string msg { get; set; }
        public SockedManager()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect("127.0.0.1", 8000);
            //AsyncServerwork();
            //GetMessage();

        }
            
        void GetMessage()
        {
           
                
                byte[] data;
                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = client.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (client.Available > 0);
                msg = builder.ToString();

            {
                if (msg == "play")
                {
                    WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                    wplayer.URL = @"H:\Music\Top50 Tracks Deep House Ver.20 (2020)\50.  Jay Aliyev  -  Move On.mp3";
                    wplayer.controls.play();
                    wplayer.settings.volume = 55;
                }



                if (msg == "play2")
                {
                    WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                    wplayer.URL = @"H:\Music\Top50 Tracks Deep House Ver.20 (2020)\32.  Jack Flex  -  Deep Inside Me (Ocean Drive Mix).mp3";
                    wplayer.controls.play();
                    wplayer.settings.volume = 50;
                }
            }



            // закрываем сокет
            //client.Shutdown(SocketShutdown.Both);
            //client.Close();
        }

        public async void AsyncServerwork()
        {   while(true)
            await Task.Run(() => GetMessage());
        }

    }
}
