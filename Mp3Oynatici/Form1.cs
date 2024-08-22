using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Helpers;
using Guna.UI2.WinForms;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mp3Player
{
    /*
            private void panel2_Paint(object sender, PaintEventArgs e)
            {

                if(audioFile != null)
                {
                Graphics g = e.Graphics;
                    Pen pen = new Pen(Color.Red,3);
                int playPositionX = (int)((audioFile.CurrentTime.TotalMilliseconds / audioFile.TotalTime.TotalMilliseconds) * panel2.Width);
                g.DrawLine(pen, playPositionX, 0, playPositionX, panel2.Height);
                }
            }
            *///panel2


    public partial class Form1 : Form
    {
        private Image imagePlay;
        private Image imagePause;
        private Image imageMute;
        private Image imageLow;
        private Image imageHalf;
        private Image imageFull;

        private IWavePlayer waveOut;
        public WaveStream audioFile;
        private float[] audioData;
        private Timer timer;
        private bool isStopped = false;
        private int voice;

        private bool isMouseDown;

        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog sfd = new SaveFileDialog();

        /****************************************** Başlıca İşlemler ******************************************/
        public Form1()
        {
            InitializeComponent();
            InitializeButtonImages();
            InitializeGunaPanel();

            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += timer_Tick;

            this.Load += Form1_Load;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            gecenSure.Text = "00:00:00";
            toplamSure.Text = "00:00:00";
            Volume.Value = 50;

            timer.Stop();
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (audioFile != null && audioFile.CurrentTime.TotalMilliseconds <= audioFile.TotalTime.TotalMilliseconds)
            {
                gecenSure.Text = audioFile.CurrentTime.ToString(@"hh\:mm\:ss");
            }
            btnVolume.Invalidate();
            graphic.Invalidate();
            Volume.Invalidate();
        }
        private void InitializeButtonImages()
        {
            imagePlay = Mp3Oynatici.Properties.Resources.icons8_play_25;
            imagePause = Mp3Oynatici.Properties.Resources.icons8_pause_25;
            imageFull = Mp3Oynatici.Properties.Resources.icons8_audio_25;
            imageHalf = Mp3Oynatici.Properties.Resources.icons8_voice_25;
            imageLow = Mp3Oynatici.Properties.Resources.icons8_low_volume_25;
            imageMute = Mp3Oynatici.Properties.Resources.icons8_sound_speaker_25;
        }
        private void InitializeGunaPanel()
        {
            graphic.MouseClick += graphic_MouseClick;
            graphic.Paint += guna2Panel1_Paint;
            graphic.MouseEnter += guna2Panel1_MouseEnter;
        }
        /****************************************** Butonlar ******************************************/
        private async void btnOpen_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Audio files (*.wav;*.mp3;*.aac)|*.wav;*.mp3;*.aac";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (waveOut != null)
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
                if (audioFile != null)
                {
                    audioFile.Dispose();
                    audioFile = null;
                }

                string filePath = ofd.FileName;

                if (filePath.EndsWith(".aac"))
                {
                    instaConvert(filePath);
                }
                audioFile = new AudioFileReader(filePath);

                waveOut = new WaveOutEvent();
                waveOut.Init(audioFile);


                toplamSure.Text = audioFile.TotalTime.ToString(@"hh\:mm\:ss");

                waveOut.PlaybackStopped += OnPlayBackStopped;
                await Task.Run(() => loadAudioData());
                btnPlay.Image = imagePlay;

                graphic.Invalidate();
                Volume.Value = 50;

                {//dosya ismi yazdırma
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string uzanti = "";
                    int sonNokta = ofd.FileName.LastIndexOf('.');
                    if (sonNokta > 0)
                        uzanti = ofd.FileName.Substring(sonNokta);
                    if (fileName.Length > 15)
                        FileName.Text = fileName.Substring(0, 15) + ".." + uzanti;
                    else
                        FileName.Text = fileName + uzanti;
                }
            }
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (btnPlay.Image == imagePlay)
            {
                btnPlay.Image = imagePause;
                waveOut?.Play();
                isStopped = false;
            }
            else if (btnPlay.Image == imagePause)
            {
                btnPlay.Image = imagePlay;
                waveOut?.Pause();
            }
        }
        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (audioFile != null)
            {
                TimeSpan newTime = audioFile.CurrentTime.Subtract(TimeSpan.FromSeconds(5));

                if (newTime < TimeSpan.Zero)
                    newTime = TimeSpan.Zero;

                audioFile.CurrentTime = newTime;

                gecenSure.Text = audioFile.CurrentTime.ToString(@"hh\:mm\:ss");
                if (isStopped)
                {
                    waveOut?.Play();
                    isStopped = false;
                }
            }
        }
        private void btnForward_Click(object sender, EventArgs e)
        {
            if (audioFile != null)
            {
                audioFile.CurrentTime = audioFile.CurrentTime.Add(TimeSpan.FromSeconds(5));

                if (audioFile.CurrentTime.TotalMilliseconds > audioFile.TotalTime.TotalMilliseconds)
                    audioFile.CurrentTime = audioFile.TotalTime;

                gecenSure.Text = audioFile.CurrentTime.ToString(@"hh\:mm\:ss");
                if (isStopped)
                {
                    waveOut?.Play();
                    isStopped = false;
                }
            }
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (audioFile != null)
            {
                waveOut?.Pause();
                audioFile.Position = 0;
                if (btnPlay.Image == imagePause)
                {
                    waveOut?.Play();
                    isStopped = false;
                }
            }
        }
        private void btnVolume_Paint(object sender, PaintEventArgs e)
        {
            int value = Volume.Value;
            if (value == 0)
            {
                btnVolume.Image = imageMute;
            }
            else if (value > 0 && value <= 33)
            {
                btnVolume.Image = imageLow;
            }
            else if (value > 33 && value <= 67)
            {
                btnVolume.Image = imageHalf;
            }
            else if (value > 67 && value <= 100)
            {
                btnVolume.Image = imageFull;
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            guna2ContextMenuStrip1.Show(btnExport, new Point(btnExport.Width, 0));
        }
        private async void toWav_Click(object sender, EventArgs e)
        {
            string fileName = ofd.FileName;
            if (fileName.EndsWith(".wav"))
            {
                MessageBox.Show("Seçilen dosya zaten wav formatındadır", "Dosya Türü Çevirme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (audioFile != null)
            {
                sfd.Filter = "WAV files (*.wav) | *.wav";
                sfd.DefaultExt = "wav";
                sfd.FileName = System.IO.Path.ChangeExtension(fileName, "wav");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string outputfilename = sfd.FileName;
                    await convert(fileName, outputfilename);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir ses dosyası oynatınız!", "Dosya bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void toMp3_Click(object sender, EventArgs e)
        {
            string fileName = ofd.FileName;
            if (fileName.EndsWith(".mp3"))
            {
                MessageBox.Show("Seçilen dosya zaten mp3 formatındadır", "Dosya Türü Çevirme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (audioFile != null)
            {
                sfd.Filter = "MP3 files (*.mp3) | *.mp3";
                sfd.DefaultExt = "mp3";
                sfd.FileName = System.IO.Path.ChangeExtension(fileName, "mp3");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string outputfilename = sfd.FileName;
                    await convert(fileName, outputfilename);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir ses dosyası oynatınız!", "Dosya bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void toAac_Click(object sender, EventArgs e)
        {
            string fileName = ofd.FileName;
            if (fileName.EndsWith(".aac"))
            {
                MessageBox.Show("Seçilen dosya zaten aac formatındadır", "Dosya Türü Çevirme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (audioFile != null)
            {
                sfd.Filter = "AAC files (*.aac) | *.aac";
                sfd.DefaultExt = "aac";
                sfd.FileName = System.IO.Path.ChangeExtension(fileName, "aac");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string outputfilename = sfd.FileName;
                    await convert(fileName, outputfilename);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir ses dosyası oynatınız!", "Dosya bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void toOgg_Click(object sender, EventArgs e)
        {
            string fileName = ofd.FileName;
            if (fileName.EndsWith(".ogg"))
            {
                MessageBox.Show("Seçilen dosya zaten ogg formatındadır", "Dosya Türü Çevirme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (audioFile != null)
            {
                sfd.Filter = "OGG files (*.ogg) | *.ogg";
                sfd.DefaultExt = "ogg";
                sfd.FileName = System.IO.Path.ChangeExtension(fileName, "ogg");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string outputfilename = sfd.FileName;
                    await convert(fileName, outputfilename);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir ses dosyası oynatınız!", "Dosya bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void audioToolStrip_Click(object sender, EventArgs e)
        {
            if (audioData != null)
            {
                //if (desibelForm.ShowDialog() == DialogResult.OK)
                //{
                //    double newVolume = desibelForm.volume;

                //    string input = ofd.FileName;
                //    string directory = Path.GetDirectoryName(input);
                //    string fileName = Path.GetFileNameWithoutExtension(input);
                //    string extension = Path.GetExtension(input);

                //    sfd.Filter = ofd.Filter;
                //    sfd.DefaultExt = ofd.Filter;
                //    sfd.FileName = Path.Combine(directory, $"Changed_{fileName}{extension}");
                //    if (sfd.ShowDialog() == DialogResult.OK)
                //    {
                //        string output = sfd.FileName;
                //        await AdjustVolumeAsync(input, output, newVolume);

                //    }
                //}
            }
            else
                MessageBox.Show("Lütfen bir ses dosyası seçiniz!", "Dosya bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnVolume_Click(object sender, EventArgs e)
        {
            if (Volume.Value > 0)
            {
                voice = Volume.Value;
                Volume.Value = 0;
                if (audioFile != null)
                    waveOut.Volume = Volume.Value / 100f;
                btnVolume.Image = imageMute;
            }
            else if (Volume.Value == 0)
            {
                Volume.Value = voice;
                if (audioFile != null)
                    waveOut.Volume = Volume.Value / 100f;
                if (Volume.Value > 0 && Volume.Value <= 33)
                    btnVolume.Image = imageLow;
                else if (Volume.Value > 33 && Volume.Value <= 67)
                    btnVolume.Image = imageHalf;
                else if (Volume.Value > 67 && Volume.Value <= 100)
                    btnVolume.Image = imageFull;
            }
        }

        /****************************************** TrackBarlar ******************************************/
        private void Volume_Scroll(object sender, ScrollEventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Volume = Volume.Value / 100f;
            }
        }

        /****************************************** Paneller ******************************************/
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            int width = graphic.Width;
            int height = graphic.Height;
            int midHeight = height / 2;
            string fileName = ofd.FileName;
            if (audioData != null && audioData.Length > 0)
            {
                int step = Math.Max(1, audioData.Length / width);
                for (int i = 0; i < width; i++)
                {
                    int index = i * step;
                    if (index < audioData.Length)
                    {
                        float value = audioData[index];
                        float y = midHeight + (float)(value * midHeight);
                        g.DrawLine(pen, i, midHeight, i, y);
                    }
                }
                if (audioFile != null)
                {
                    pen = new Pen(Color.Red, 3);
                    int playPositionX = (int)((audioFile.CurrentTime.TotalMilliseconds / audioFile.TotalTime.TotalMilliseconds) * graphic.Width);
                    if (playPositionX > graphic.Width)
                        playPositionX = graphic.Width;
                    if (isMouseDown)
                        g.DrawLine(pen, playPositionX, 0, playPositionX, graphic.Height);
                    else if (isStopped)
                    {
                        g.DrawLine(pen, graphic.Width - 1, 0, graphic.Width - 1, graphic.Height);
                    }
                    else
                        g.DrawLine(pen, playPositionX, 0, playPositionX, graphic.Height);
                }
            }
        }
        private void graphic_MouseClick(object sender, MouseEventArgs e)
        {
            if (audioFile != null)
            {
                float percentage = (float)e.X / graphic.Width;
                double newTime = percentage * audioFile.TotalTime.TotalMilliseconds;
                audioFile.CurrentTime = TimeSpan.FromMilliseconds(newTime);
            }
        }
        private void guna2Panel1_MouseEnter(object sender, EventArgs e)
        {
            if (audioFile != null)
            {
                graphic.Cursor = Cursors.Hand;
            }
        }
        private void guna2Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && audioFile != null)
            {
                float percentage = ((float)e.X * (float)audioFile.TotalTime.TotalMilliseconds) / graphic.Width;
                if (percentage < 0)
                    percentage = 0;
                audioFile.CurrentTime = TimeSpan.FromMilliseconds(percentage);
            }
        }
        private void guna2Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (audioFile != null)
            {
                isMouseDown = true;
                waveOut?.Stop();
            }
        }
        private void guna2Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (audioFile != null)
            {
                isMouseDown = false;
                waveOut?.Play();
                isStopped = false;
                if (btnPlay.Image == imagePlay)
                    btnPlay.Image = imagePause;
            }
        }
        /****************************************** Ekstra Metotlar ******************************************/
        public void loadAudioData()
        {
            int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            List<float> audioDataList = new List<float>();
            int a = 0;
            int samplesRead;

            while ((samplesRead = audioFile.Read(buffer, 0, buffer.Length)) > 0)
            {
                float[] floatbuffer = new float[samplesRead / 4];
                Buffer.BlockCopy(buffer, 0, floatbuffer, 0, samplesRead);

                for (int i = 0; i < floatbuffer.Length; i++)
                {
                    float value = floatbuffer[i];

                    audioDataList.Add(value);
                }

                a++;
                if (a == bufferSize) { break; }
            }

            audioData = audioDataList.ToArray();
            audioFile.Position = 0;
        }
        private void OnPlayBackStopped(object sender, StoppedEventArgs e)
        {
            btnPlay.Image = imagePlay;
            gecenSure.Text = toplamSure.Text;
            isStopped = true;

        }
        private async void instaConvert(string fileName)
        {
            string outputfilename = System.IO.Path.ChangeExtension(fileName, "wav"); ;
            await convert(fileName, outputfilename);
        }
        private async Task convert(string input, string output)
        {
            await FFMpegArguments
                .FromFileInput(input)
                .OutputToFile(output, overwrite: true)
                .ProcessAsynchronously();
        }
        public async Task AdjustVolumeAsync(string inputFilePath, string outputFilePath, double volumeLevel)
        {
            string newVolume = volumeLevel.ToString(CultureInfo.InvariantCulture);

            await FFMpegArguments
                .FromFileInput(inputFilePath)
                .OutputToFile(outputFilePath, overwrite: true, options => options
                    .WithCustomArgument($"-filter:a \"volume={newVolume}\""))
                .ProcessAsynchronously();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Volume.Width = this.Width / 5;
        }
    }
}
