using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace b_player
{
    public partial class Form1 : Form
    {
        String[] names, pm;
        bool turn = false;
        int ch = 0, len = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                axWindowsMediaPlayer1.Size = new System.Drawing.Size(1365, 696);
                trackBar1.Size = new System.Drawing.Size(200, 45);
                trackBar1.Location = new Point(965, 7);
                bunifuImageButton5.Location = new Point(930,3);
                progressBar1.Location = new Point(0, 720);
                progressBar1.Size = new System.Drawing.Size(1364, 5);
                movies.Size = new System.Drawing.Size(120, 690);
                button1.Location = new Point(106, 350);
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                axWindowsMediaPlayer1.Size = new System.Drawing.Size(778, 360);
                trackBar1.Size = new System.Drawing.Size(104, 45);
                trackBar1.Location = new Point(476, 7);
                progressBar1.Location = new Point(0, 385);
                progressBar1.Size = new System.Drawing.Size(778, 5);
                movies.Size = new System.Drawing.Size(120, 354);
                button1.Location = new Point(106, 172);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void load_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.ValidateNames = true;
            o.Filter = "mp4|*.mp4|mp3|*.mp3|wmv|*.wmv|flv|*.flv|mpeg|*.mpeg|wma|*.wma";
            if (o.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                movies.Items.Clear();
                pm = o.FileNames;
                names = o.SafeFileNames;
                for (int i = 0; i < names.Length; i++)
                {
                    movies.Items.Add(names[i]);
                }
                len = pm.Length;
                axWindowsMediaPlayer1.URL = pm[0];
                axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
                movies.SelectedIndex = 0;
                label1.Text = setname(names[0]);
            }
        }

        private void movies_SelectedIndexChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = pm[movies.SelectedIndex];
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (!turn)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                turn = true;
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                turn = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "<")
            {
                button1.Text = ">";
             if (this.WindowState == FormWindowState.Normal)
                button1.Location = new Point(0, 172);
             else
                 button1.Location = new Point(0, 350);
                movies.Width = 14;
            }
            else
            {
                button1.Text = "<";
                if (this.WindowState == FormWindowState.Normal)
                    button1.Location = new Point(106, 172);
                else
                    button1.Location = new Point(106, 350);
                movies.Width = 120;
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (movies.Visible)
            {
                movies.Visible = false;
                button1.Visible = false;
            }
            else
            {
                movies.Visible = true;
                button1.Visible = true;
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (len > 1)
            {
                if ((ch + 1) <= (names.Length - 1))
                {
                    ch++;
                    movies.SelectedIndex = ch;
                    axWindowsMediaPlayer1.URL = pm[movies.SelectedIndex];
                    label1.Text = setname(names[movies.SelectedIndex]);
                }
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            if (len > 1)
            {
                if ((ch) > (0))
                {
                    ch--;
                    movies.SelectedIndex = ch;
                    axWindowsMediaPlayer1.URL = pm[movies.SelectedIndex];
                    label1.Text = setname(names[movies.SelectedIndex]);
                }
            }
        }

        String setname(String str)
        {
            String s = "";
            char[] ch = str.ToCharArray();
            if (ch.Length > 35)
            {
                for (int i = 0; i < 35; i++)
                    s += ch[i];
                s += "...";
            }
            else
                for (int i = 0; i < ch.Length; i++)
                    s += ch[i];
            return s;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
            label2.Text = trackBar1.Value.ToString() + "%";
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            if (trackBar1.Value != 0)
            {
                trackBar1.Value = 0;
                axWindowsMediaPlayer1.settings.volume = 0;
                label2.Text = "0%";
            }
            else
            {
                trackBar1.Value = 100;
                axWindowsMediaPlayer1.settings.volume = 100;
                label2.Text = "100%";
            }
        }

        private void axWindowsMediaPlayer1_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if(axWindowsMediaPlayer1.playState==WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar1.Maximum = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                timer1.Start();
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                timer1.Stop();
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                timer1.Stop();
                progressBar1.Value = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            label4.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString.ToString();            
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            }
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void bottom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.fastForward();

        }

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.fastReverse();
        }
    }
}
