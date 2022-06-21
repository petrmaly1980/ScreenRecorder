using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Captura;

namespace ScreenRecorder
{
    public partial class Form1 : Form
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();
        private void UpdateTime()
        {
            labelTime.Text = GetTimeString(stopwatch.Elapsed);
        }
        private string GetTimeString(TimeSpan elapsed)
        {
            string result = string.Empty;
            result = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            elapsed.Hours,
            elapsed.Minutes,
            elapsed.Seconds,
            elapsed.Milliseconds);
            
            return result;
        }
        public Form1()
        {
            InitializeComponent();
            UpdateTime();
            buttonStop.Enabled = false;
            buttonStart.BackColor = Color.Green;
            buttonStop.BackColor = Color.White;
        }
        public Recorder rec;
        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Text = "Recording !";
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            buttonStart.BackColor = Color.Red;
            buttonStop.BackColor = Color.Green;
            string now = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'.'mm'.'ss");
            int fr = int.Parse(comboBoxFrameRate.Text);
            int quality = int.Parse(comboBoxQuality.Text);
            //Recorder recorder = new Recorder(new RecorderParams("out.avi", 10, SharpAvi.KnownFourCCs.Codecs.MotionJpeg, 70));
            rec = new Recorder(new RecorderParams("record_"+now+".avi", fr, SharpAvi.KnownFourCCs.Codecs.MotionJpeg, quality));
            //rec = recorder;
            stopwatch.Start();
            timer.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStart.Text = "Start Recording";
            buttonStart.BackColor = Color.Green;
            buttonStop.BackColor= Color.White;
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            stopwatch.Stop();
            rec.Dispose();
            timer.Enabled = false;
            stopwatch.Reset();
            UpdateTime();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }
    }
}
