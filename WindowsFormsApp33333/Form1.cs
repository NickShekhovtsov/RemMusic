using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Forms;
using System.Media;
using WMPLib;
using DAudio;
using System.Net.Sockets;
using WindowsFormsApp33333;
using System.Threading;

namespace WindowsFormsApp33333
{
    public partial class Form1 : Form
    {   
        
        private AudioPlayer Player;
        SockedManager client = new SockedManager();
        public AudioList audioList = new AudioList();
        private bool isaudioListChanged { get; set;}
        
        
        public Form1()
        {
           
            InitializeComponent();
            Player = new AudioPlayer();
            Player.PlayingStatusChanged += (s, e) => button1.Text = e == Status.Playing ? "Pause" : "Play";
            Player.AudioSelected += (s, e) =>
             {
                 trackBar1.Maximum = (int)e.Duration;
                 label1.Text = e.Name;
                 label4.Text = e.Duration.ToString(@"mm\:ss");
                 listBox1.SelectedItem = e.Name;
             };
            Player.ProgressChanged += (s, e) =>
              {
                  trackBar1.Value = (int)e;
                  label2.Text = ((AudioPlayer)s).PositionTime.ToString(@"mm\:ss");
              };

            InputServerWorkAsync();




        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (((Button)sender).Text == "Play")
            {

                Player.Play();
            }
            else if (((Button)sender).Text == "Pause")
                Player.Pause();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog=new OpenFileDialog() { Multiselect=true,Filter="Audio Files|*.mp3;"})
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Player.LoadAudio(dialog.FileNames);

                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(Player.Playlist);
                    // below must be changed. Promlem is we shoud have list before add information to it. This makes too large list before
                    for (int i = 0; i < Player.Playlist.Count(); i++)
                    {
                        audioList.audioinfolist.Add(new InfoAbSong());
                    }
                    for (int i = 0; i < Player.Playlist.Count(); i++)
                    {

                        audioList.audioinfolist[i] = new InfoAbSong(Player.playlist[i]);

                    }

                    byte[] outputdata;
                    outputdata = new byte[1024];
                    string outputjson = JsonSerializer.Serialize<AudioList>(audioList);
                    outputdata = Encoding.Unicode.GetBytes(outputjson);
                    client.client.Send(outputdata);
                }




            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItem == null)
                return;
            Player.SelectAudio(((ListBox)sender).SelectedIndex);
        }

        private void trackBar1_Scroll(object sender, EventArgs e) => Player.Position = ((TrackBar)sender).Value;

        private void trackBar2_Scroll(object sender, EventArgs e) => Player.Volume = ((TrackBar)sender).Value;

        private void button3_Click(object sender, EventArgs e)
        {
            

        }
        void InputServerWork()
        {

            while (true)
                if (client.client.Available > 0)
                {
                    byte[] data;
                    // получаем ответ
                    data = new byte[256]; // буфер для ответа
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байт

                    do
                    {
                        bytes = client.client.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (client.client.Available > 0);

                    client.msg = builder.ToString();

                    if (client.msg == "play")
                    {
                        button1_Click(button1, EventArgs.Empty);

                        //Player.Play();
                    }

                    if (client.msg == "stop")
                    {
                        button1_Click(button1, EventArgs.Empty);

                        //Player.Pause();
                    }

                }
                else Thread.Sleep(100);
        }

        async void InputServerWorkAsync()
        {
            await Task.Run(()=>InputServerWork());
        }

        
    }
}
