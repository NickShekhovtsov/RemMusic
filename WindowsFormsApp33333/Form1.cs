using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using WMPLib;
using DAudio;

namespace WindowsFormsApp33333
{
    public partial class Form1 : Form
    {
        private AudioPlayer Player;
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
                Player.Play();
            else if (((Button)sender).Text == "Pause")
                Player.Pause();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog=new OpenFileDialog() { Multiselect=true,Filter="Audio Files|*.mp3;"})
            {
                if (dialog.ShowDialog()== DialogResult.OK)
                {
                    Player.LoadAudio(dialog.FileName);
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(Player.Playlist);
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
            listBox1.Items.Clear();
            Player.Clear_playlist();
        }
    }
}
