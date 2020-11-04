using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp33333
{
    class SockedManager
    {
        Socket client;
        public string msg { get; set; }
        public SockedManager()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect("127.0.0.1", 8000);
            //AsyncServerwork();
            GetMessage();

        }
            
        void GetMessage()
        {
           
                
                byte[] data ;
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
            
            // закрываем сокет
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        async void AsyncServerwork()
        {
            await Task.Run(() => GetMessage());
        }

    }
}
