using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Helpers;
using Guna.UI2.WinForms;
using LiveCharts;
using LiveCharts.Wpf;
using Mp3Oynatici;
using NAudio.Utils;
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
        private Image imageMicrophone;
        private Image imageMp3Player;

        private string lastPath;
        private float[] audioData;
        private float lastMaxAmplitude = 0;
        private int voice;
        private bool isMouseDown;
        private bool isStopped = false;
        private bool isPaused = false;
        private bool isPlayer = true;
        private bool isRemove = false;

        private IWavePlayer waveOut;
        public WaveStream audioFile;
        private WaveInEvent waveIn;
        private MemoryStream memoryStream;
        private WaveFileWriter writer;
        private Timer timer;
        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog sfd = new SaveFileDialog();
        decibelForm decibelForm = new decibelForm();

        /****************************************** Başlıca İşlemler ******************************************/
        public Form1()
        {
            InitializeComponent();
            InitializeButtonImages();

            timer = new Timer(); // yeni timer oluşturuluyor
            timer.Interval = 10; // timer her 10 milisaniyede bir tetikleniyor
            timer.Tick += timer_Tick; // timer_Tick metodu timer'ın tetiklenmesiyle ilişkilendiriliyor

            this.Load += Form1_Load; // Form1_Load metodu formun yüklenmesiyle ilişkilendiriliyor
        }
        private void Form1_Load(object sender, EventArgs e) //form1 ilk yüklendiğinde olacak işlemler
        {

            lblGecenSure.Text = "00:00:00";  // ses dosyasının anlık süre label'ına değer atanıyor
            lblToplamSure.Text = "00:00:00"; // ses dosyasının toplam süre label'ına değer atanıyor
            Volume.Value = 50; // ses trackbar'ının başlangıç değeri yarıya çekiliyor
            btnMicrophone.Image = imageMicrophone;
            btnMicPlay.Image = imagePlay;

            btnMicPlay.Visible = false;
            btnMicStop.Visible = false;
            btnBluetooth.Visible = false;
            lblBluetooth.Visible = false;

            lastPath = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName + "\\Kaydedilenler\\";

            timer.Stop();
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e) // timer'ın tetiklenme olayı
        {
            if (audioFile != null && audioFile.CurrentTime.TotalMilliseconds <= audioFile.TotalTime.TotalMilliseconds) // import edilen dosya varsa ve anlık zaman toplam zamandan küçük veya eşitse
            {
                lblGecenSure.Text = audioFile.CurrentTime.ToString(@"hh\:mm\:ss");  // anlık süre label'ının değeri ses dosyasının anlık süresi olarak atanıyor
            }
            btnVolume.Invalidate(); // ses buton'u güncelleniyor (ses düzeyine göre ikon değişikliği için)
            pnlGraphic.Invalidate(); // grafik panel'i güncelleniyor
            Volume.Invalidate(); // ses trackbar'ı güncelleniyor
        }
        private void InitializeButtonImages() // değişkenlere ikonların atanması
        {
            imagePlay = Mp3Oynatici.Properties.Resources.icons8_play_25;
            imagePause = Mp3Oynatici.Properties.Resources.icons8_pause_25;
            imageFull = Mp3Oynatici.Properties.Resources.icons8_audio_25;
            imageHalf = Mp3Oynatici.Properties.Resources.icons8_voice_25;
            imageLow = Mp3Oynatici.Properties.Resources.icons8_low_volume_25;
            imageMute = Mp3Oynatici.Properties.Resources.icons8_sound_speaker_25;
            imageMicrophone = Mp3Oynatici.Properties.Resources.icons8_microphone_30;
            imageMp3Player = Mp3Oynatici.Properties.Resources.icons8_sound_30;
        }
        /****************************************** Butonlar ******************************************/
        private async void btnOpen_Click(object sender, EventArgs e) // import butonuna tıklanma olayı
        {
            ofd.Filter = "Audio files (*.wav;*.mp3;*.aac)|*.wav;*.mp3;*.aac"; // import edilen dosyası seçme ekranında görünecek dosya formatları
            if (ofd.ShowDialog() == DialogResult.OK) // dosya seçildiğinde
            {
                if (waveOut != null) // Zaten import edilen dosya varsa durdur ve bellekten sil
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
                if (audioFile != null) // Zaten import edilen dosya varsa durdur ve bellekten sil
                {
                    audioFile.Dispose();
                    audioFile = null;
                }

                string filePath = ofd.FileName;

                if (filePath.EndsWith(".aac")) // dosya aac uzantılıysa wav'a çevirme
                {
                    instaConvert(filePath);
                }
                audioFile = new AudioFileReader(filePath); // seçilen dosyayı import etme

                waveOut = new WaveOutEvent(); 
                waveOut.Init(audioFile);
                btnPlay.Image = imagePlay;
                isRemove = false;


                lblToplamSure.Text = audioFile.TotalTime.ToString(@"hh\:mm\:ss"); // toplam süre label'ına dosyanın toplam süresini yazdırma

                waveOut.PlaybackStopped += OnPlayBackStopped; // OnPlayBackStopped metodunu dosyanın bitmesiyle ilişkilendirme
                await Task.Run(() => loadAudioData()); // ses dosyasını işleyecek olan metodu arkaplanla çalıştırma

                pnlGraphic.Invalidate(); // dosya eklendiğinde grafik panel'ini tek seferli güncelleme

                {   // dosya ismi label'ına dosyanın ismini yazdırma
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string uzanti = "";
                    int sonNokta = ofd.FileName.LastIndexOf('.');
                    if (sonNokta > 0)
                        uzanti = ofd.FileName.Substring(sonNokta);
                    if (fileName.Length > 15)
                        lblFileName.Text = fileName.Substring(0, 15) + ".." + uzanti;
                    else
                        lblFileName.Text = fileName + uzanti;
                }
            }
        }
        private void btnPlay_Click(object sender, EventArgs e) // oynatma butonuna tıklama olayı
        { // butona tıklanınca dosyayı durdurup devam ettirme ve ikon değiştirme işlemi
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
        private void btnBackward_Click(object sender, EventArgs e) // geri alma butonuna tıklama olayı
        { // butona basıldığında oynatılan dosyanın 5 saniye geri alınmasını sağlama işlemi
            if (audioFile != null)
            {
                TimeSpan newTime = audioFile.CurrentTime.Subtract(TimeSpan.FromSeconds(5)); // yeni zamana anlık zamandan 5 saniye önceki zamanın atanması

                if (newTime < TimeSpan.Zero)
                    newTime = TimeSpan.Zero;

                audioFile.CurrentTime = newTime;

                lblGecenSure.Text = audioFile.CurrentTime.ToString(@"hh\:mm\:ss"); 
                if (isStopped)
                {
                    waveOut?.Play();
                    isStopped = false;
                    btnPlay.Image = imagePause;
                }
            }
        }
        private void btnForward_Click(object sender, EventArgs e) // ileri alma butonuna tıklama olayı
        { // butona basıldığında oynatılan dosyanın 5 saniye ileri alınmasını sağlama işlemi
            if (audioFile != null)
            {
                audioFile.CurrentTime = audioFile.CurrentTime.Add(TimeSpan.FromSeconds(5)); // anlık zamanı 5 saniye ileri alma işlemi

                if (audioFile.CurrentTime.TotalMilliseconds > audioFile.TotalTime.TotalMilliseconds)
                    audioFile.CurrentTime = audioFile.TotalTime;

                lblGecenSure.Text = audioFile.CurrentTime.ToString(@"hh\:mm\:ss");
                if (isStopped)
                {
                    waveOut?.Play();
                    isStopped = false;
                    btnPlay.Image = imagePause;
                }
            }
        }
        private void btnRestart_Click(object sender, EventArgs e) // yeniden başlatma butonuna tıklanma olayı
        { // butona tıklandığında çalınan dosyanın başa alınması
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
        private void btnExport_Click(object sender, EventArgs e) // export butonuna tıklanma olayı
        { 
            guna2ContextMenuStrip1.Show(btnExport, new Point(btnExport.Width, 0)); // butona tıklandığında context menünün açılma işlemi
        }
        private async void toWav_Click(object sender, EventArgs e) // context menüdeki toWav seçeneğine tıklanma işlemi
        { // import edilen dosyanın wav formatına çevrilme işlemi
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
        private async void toMp3_Click(object sender, EventArgs e) // context menüdeki toMp3 seçeneğine tıklanma işlemi
        { // import edilen dosyanın mp3 formatına çevrilme işlemi
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
        private async void toAac_Click(object sender, EventArgs e) // context menüdeki toAac seçeneğine tıklanma işlemi
        { // import edilen dosyanın aac formatına çevrilme işlemi
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
        private async void toOgg_Click(object sender, EventArgs e) // context menüdeki toOgg seçeneğine tıklanma işlemi
        { // import edilen dosyanın ogg formatına çevrilme işlemi
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
        private async void audioToolStrip_Click(object sender, EventArgs e) // context menüdeki desibel değiştirme seçeneğine tıklama olayı
        { // desibel formun açılması ve formda seçilen değere göre import edilen ses dosyasının desibelinin değiştirilme işlemi
            if (audioData != null)
            {
                if (decibelForm.ShowDialog() == DialogResult.OK)
                {
                    double newVolume = decibelForm.newDecibel;

                    string input = ofd.FileName;
                    string fileName = Path.GetFileNameWithoutExtension(input);
                    string extension = Path.GetExtension(input);

                    sfd.Filter = ofd.Filter;
                    sfd.DefaultExt = ofd.DefaultExt;
                    sfd.FileName = $"Changed_{fileName}{extension}";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string output = sfd.FileName;
                        await AdjustVolumeAsync(input, output, newVolume); // desibel değiştirme metodunun çağrıldığı yer
                    }
                }
                else
                    MessageBox.Show("The operation failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Lütfen bir ses dosyası seçiniz!", "Dosya bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private async void backgroundToolStrip_Click(object sender, EventArgs e)
        {
            if (audioFile != null)
            {
                sfd.Filter = ofd.Filter;
                sfd.DefaultExt = ofd.DefaultExt;
                sfd.FileName = "RemovedBGround_" + Path.GetFileName(ofd.FileName);
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string output = sfd.FileName;
                    await RemoveBack(ofd.FileName, output);
                }
            }
            else
                MessageBox.Show("Lütfen bir ses dosyası seçiniz!", "Dosya bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnVolume_Click(object sender, EventArgs e) // ses butonuna tıklanma olayı
        { // butona tıklandığında sesin kapanma ve açılma işlemi
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
            }
        }
        private void btnMicrophone_Click(object sender, EventArgs e)
        {
            if (isPlayer && btnMicrophone.Image == imageMicrophone)
            {
                btnMicrophone.Image = imageMp3Player;
                lblChangeMicrophone.Text = "Mp3Player";
                goesMicrophone();
            }
            else if (!isPlayer && btnMicrophone.Image == imageMp3Player)
            {
                StopRecording();
                btnMicrophone.Image = imageMicrophone;
                lblChangeMicrophone.Text = "Microphone";
                goesPlayer();
            }
        }
        private async void btnBackground_Click(object sender, EventArgs e)
        {
            if (audioFile != null && !isRemove)
            {
                string newOutput = "tempFile.wav";

                await RemoveBack(ofd.FileName, newOutput);

                waveOut.Stop();
                waveOut.Dispose();
                audioFile.Dispose();

                audioFile = new AudioFileReader(newOutput);
                waveOut = new WaveOutEvent();
                waveOut.Init(audioFile);
                isRemove = true;
                MessageBox.Show("Background noise is removed!", "Succesfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (isRemove)
                MessageBox.Show("Background already removed!", "Second Remove", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("Lütfen bir ses dosyası seçiniz!", "Dosya bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnMicPlay_Click(object sender, EventArgs e)
        {
            if (!isMicOk())
                MessageBox.Show("Microphone is not found!", "Can't start recording", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (btnMicPlay.Image == imagePlay)
            {
                btnMicPlay.Image = imagePause;
                StartRecording();
            }
            else if (btnMicPlay.Image == imagePause)
            {
                btnMicPlay.Image = imagePlay;
                PauseRecording();
            }
        }
        private void btnMicStop_Click(object sender, EventArgs e)
        {
            if (memoryStream != null)
            {
                StopRecording();

                sfd.Filter = "WAV files (*.wav)|*.wav";
                string tempFile = "MicDosyasi.wav";
                sfd.InitialDirectory = lastPath;
                string uniqueFilename = GenerateUniqueFileName(Path.Combine(sfd.InitialDirectory, tempFile));
                sfd.FileName = Path.GetFileName(uniqueFilename);

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    lastPath = Path.GetDirectoryName(sfd.FileName);
                    string outputPath = sfd.FileName;
                    SaveRecording(outputPath);
                }


                //pnlGraphic.BackColor = Color.FromArgb(192, 255, 255);
            }
            else
                MessageBox.Show("Please start recording!", "No Recording", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /****************************************** TrackBarlar ******************************************/
        private void Volume_Scroll(object sender, ScrollEventArgs e) // ses trackbar'ını kaydırma olayı
        {
            if (waveOut != null)
            {
                waveOut.Volume = Volume.Value / 100f; // trackbar'ın değerine göre oynatılan dosyanın sesini ayarlama işlemi
            }
        }

        /****************************************** Paneller ******************************************/
        private void pnlGraphic_Paint(object sender, PaintEventArgs e) // grafik panelinin görselinin değişme olayı
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black); // Grafiği çizecek kalemi oluşturma

            int width = pnlGraphic.Width;
            int height = pnlGraphic.Height;
            int midHeight = height / 2;
            string fileName = ofd.FileName;

            if (btnMicrophone.Image == imageMicrophone)
            {
                if (audioData != null && audioData.Length > 0)
                {
                    int step = Math.Max(1, audioData.Length / width);
                    for (int i = 0; i < width; i++) // import edilen ses dosyasının ses şiddetinin grafiğini çıkartma işlemi
                    {
                        int index = i * step;
                        if (index < audioData.Length)
                        {
                            float value = audioData[index];
                            float y = midHeight + (float)(value * midHeight);
                            g.DrawLine(pen, i, midHeight, i, y); // ses dosyasının her bir verisine göre ses şiddeti grafiğini çıkartma işlemi
                        }
                    }
                    if (audioFile != null)
                    {
                        pen = new Pen(Color.Red, 3);
                        int playPositionX = (int)((audioFile.CurrentTime.TotalMilliseconds / audioFile.TotalTime.TotalMilliseconds) * pnlGraphic.Width);
                        if (playPositionX > pnlGraphic.Width)
                            playPositionX = pnlGraphic.Width;
                        if (isMouseDown)
                            g.DrawLine(pen, playPositionX, 0, playPositionX, pnlGraphic.Height); // fare ile grafiğin üzerinden ses dosyasının yönetiminin takibi için görsel oluşturma işlemi
                        else if (isStopped)
                        {
                            g.DrawLine(pen, pnlGraphic.Width - 1, 0, pnlGraphic.Width - 1, pnlGraphic.Height); // ses dosyası oynatması bittiğinde sona gelindiğini gösteren görseli oluşturma işlemi
                        }
                        else
                            g.DrawLine(pen, playPositionX, 0, playPositionX, pnlGraphic.Height); // grafik üzerinde ses dosyasının anlık takibinin yapılabileceği görseli oluşturma işlemi
                    }
                }
            }
            else if (btnMicrophone.Image == imageMp3Player)
            {
                // Ses şiddetini hesaplayın (0 ile 1 arasında bir değer)
                float amplitude = CalculateCurrentAmplitude();

                // Grafiğin orta noktasını belirleyin
                int centerX = pnlGraphic.Width / 2;
                int centerY = pnlGraphic.Height / 2;

                // Ses şiddetine göre çizginin yüksekliğini hesaplayın
                int barHeight = (int)(amplitude * pnlGraphic.Height * 5);

                // Çizimin hassasiyetini artırmak için her bir adımı belirleyin
                int step = pnlGraphic.Width / 300;

                for (int x = 0; x < pnlGraphic.Width; x += step)
                {
                // Y ekseninde merkezi simetrik olarak yukarı ve aşağı çizgiler çizin
                e.Graphics.DrawLine(pen, x, centerY - barHeight / 2, x, centerY + barHeight / 2);
                }
            }

        }
        private void btnVolume_Paint(object sender, PaintEventArgs e) // ses butonunun görselinin değişme olayı
        { // ses butonunun ses düzeyine göre ikonunun değişme işlemi
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
        private void pnlGraphic_MouseClick(object sender, MouseEventArgs e) // panele fare ile tıklama olayı
        { // ses dosyasının oynatma konumunun panel üzerinde tıklanan yere getirilme işlemi
            if (audioFile != null && btnMicrophone.Image == imageMicrophone)
            {
                float percentage = (float)e.X / pnlGraphic.Width;
                double newTime = percentage * audioFile.TotalTime.TotalMilliseconds;
                audioFile.CurrentTime = TimeSpan.FromMilliseconds(newTime);
            }
        }

        private void pnlGraphic_MouseEnter(object sender, EventArgs e) // fare ile panel üzerine gelme olayı
        {
            if (audioFile != null && btnMicrophone.Image == imageMicrophone)
            {
                pnlGraphic.Cursor = Cursors.Hand; // fare ile panelin üzerine gelindiğinde farenin görselinin "el" şekline dönüşmesi işlemi
            }
            else
                pnlGraphic.Cursor = Cursors.Default;
        }
        private void pnlGraphic_MouseMove(object sender, MouseEventArgs e) // fare ile panelin üzerinde tıklanıp kaydırma olayı
        {
            if (isMouseDown && audioFile != null && btnMicrophone.Image == imageMicrophone)
            {
                float percentage = ((float)e.X * (float)audioFile.TotalTime.TotalMilliseconds) / pnlGraphic.Width;
                if (percentage < 0)
                    percentage = 0;
                audioFile.CurrentTime = TimeSpan.FromMilliseconds(percentage); // fare ile panel üzerinde kaydırma anında anlık olarak dosyanın oynatma takibinin yapılma işlemi
            }
        }
        private void pnlGraphic_MouseDown(object sender, MouseEventArgs e) // fare ile panele basılı tutma olayı
        {
            if (audioFile != null && btnMicrophone.Image == imageMicrophone)
            {
                isMouseDown = true;
                waveOut?.Stop(); // fare ile panelin üzerine basılı tutulduğu anda çalınan ses dosyasının durdurulma işlemi
            }
        }
        private void pnlGraphic_MouseUp(object sender, MouseEventArgs e) // panel üzerinde basılı tutulan farenin basılı tutmanın bırakılma olayı
        {
            if (audioFile != null && btnMicrophone.Image == imageMicrophone)
            {
                isMouseDown = false;
                waveOut?.Play(); // farenin basılı tutulurken durdurulan ses kaydının basılı tutma bittiğinde kaldığı yerden oynatılmaya devam etme işlemi
                isStopped = false;
                if (btnPlay.Image == imagePlay)
                    btnPlay.Image = imagePause;
            }
        }
        /****************************************** Ekstra Metotlar ******************************************/
        public void loadAudioData() // import edilen ses dosyasının verisinin işlendiği metot
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
                if (a == bufferSize)
                    break; 
            }

            audioData = audioDataList.ToArray();
            audioFile.Position = 0;
        }
        private void OnPlayBackStopped(object sender, StoppedEventArgs e) // oynatılan ses dosyasının bitme olayı
        { // oynatılan dosya bittiğinde oynatma butonu ikonunun değişmesi, anlık zaman label'ının toplam süre label'ıyla eşitlenmesi ve dosyanın durduğunu belirtecek bir değişken ataması yapılması işlemi
            btnPlay.Image = imagePlay;
            lblGecenSure.Text = lblToplamSure.Text;
            isStopped = true;

        }
        private async void instaConvert(string fileName) // import edilen ses dosyasının wav formatına çevrildiği metot
        {
            string outputfilename = System.IO.Path.ChangeExtension(fileName, "wav"); ;
            await convert(fileName, outputfilename);
        }
        private async Task convert(string input, string output) // import edilen ses dosyasının istenilen formata çevrilip export edildiği metot
        {
            await FFMpegArguments
                .FromFileInput(input)
                .OutputToFile(output, overwrite: true)
                .ProcessAsynchronously();
        }
        public async Task AdjustVolumeAsync(string inputFilePath, string outputFilePath, double volumeLevel) // import edilen ses dosyasının desibelinin ayarlanıp export edildiği asenkron çalışan metot
        {
            string newVolume = volumeLevel.ToString(CultureInfo.InvariantCulture);

            await FFMpegArguments
                .FromFileInput(inputFilePath)
                .OutputToFile(outputFilePath, overwrite: true, options => options
                    .WithCustomArgument($"-filter:a \"volume={newVolume}\""))
                .ProcessAsynchronously();
        }


        private void goesMicrophone()
        {
            isPlayer = false;
            btnBackward.Visible = false;
            btnRestart.Visible = false;
            btnPlay.Visible = false;
            btnForward.Visible = false;
            btnVolume.Visible = false;
            btnOpen.Visible = false;
            btnExport.Visible = false;
            btnBackground.Visible = false;
            Volume.Visible = false;
            lblGecenSure.Visible = false;
            lblToplamSure.Visible = false;
            lblImport.Visible = false;
            lblExport.Visible = false;
            lblBackground1.Visible = false;
            lblBackground2.Visible = false;
            lblFileName.Visible = false;

            btnMicPlay.Visible = true;
            btnMicStop.Visible = true;
            btnBluetooth.Visible = true;
            lblBluetooth.Visible = true;

        }
        private void goesPlayer()
        {
            isPlayer = true;
            btnBackward.Visible = true;
            btnRestart.Visible = true;
            btnPlay.Visible = true;
            btnForward.Visible = true;
            btnVolume.Visible = true;
            btnOpen.Visible = true;
            btnExport.Visible = true;
            btnBackground.Visible = true;
            Volume.Visible = true;
            lblGecenSure.Visible = true;
            lblToplamSure.Visible = true;
            lblImport.Visible = true;
            lblExport.Visible = true;
            lblBackground1.Visible = true;
            lblBackground2.Visible = true;
            lblFileName.Visible = true;

            btnMicPlay.Visible = false;
            btnMicStop.Visible = false;
            btnBluetooth.Visible = false;
            lblBluetooth.Visible = false;
        }
        public void StartRecording()
        {
            if (waveIn == null && isMicOk())
            {
                MessageBox.Show("Recording has started", "Recording", MessageBoxButtons.OK, MessageBoxIcon.Information);
                waveIn = new WaveInEvent();
                waveIn.WaveFormat = new WaveFormat(44100, 1);
                waveIn.DataAvailable += OnDataAvailable;
                waveIn.RecordingStopped += OnRecordingStopped;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                }

                if (writer == null)
                {
                    writer = new WaveFileWriter(new IgnoreDisposeStream(memoryStream), waveIn.WaveFormat);
                }
                waveIn.StartRecording();
                isPaused = false;
            }
            else if (waveIn != null)
            {
                MessageBox.Show("Recording continues", "Recording", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResumeRecording();
            }
        }
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (!isPaused && writer != null)
            {
                // Anlık olarak alınan ses verilerinin şiddetini hesaplayın
                float max = 0;
                for (int index = 0; index < e.BytesRecorded; index += 2)
                {
                    short sample = BitConverter.ToInt16(e.Buffer, index);
                    float sample32 = sample / 32768f;
                    if (sample32 < 0) sample32 = -sample32;
                    if (sample32 > max) max = sample32;
                }

                // Hesaplanan maksimum amplitüdü global değişkene kaydedin
                lastMaxAmplitude = max;

                // Sonucu bir grafik üzerinde gösterin
                this.BeginInvoke(new Action(() =>
                {
                    // Panel üzerinde grafiği çizmek için yeniden çizim çağırın
                    pnlGraphic.Invalidate();
                }));
            }
        }
        private void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            writer?.Dispose();
            writer = null;
            waveIn?.Dispose();
            waveIn = null;
        }

        public void PauseRecording()
        {
            isPaused = true;
        }

        public void ResumeRecording()
        {
            isPaused = false;
        }

        public void StopRecording()
        {
            waveIn?.StopRecording();
            writer?.Dispose();
            writer = null;
            waveIn?.Dispose();
            waveIn = null;
            if (btnMicPlay.Image == imagePause)
                btnMicPlay.Image = imagePlay;
        }

        public void SaveRecording(string outputPath)
        {
            File.WriteAllBytes(outputPath, memoryStream.ToArray());
            memoryStream.Dispose();
            memoryStream = null;
        }

        private string GenerateUniqueFileName(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            int counter = 1;
            string newFilePath = filePath;

            while (File.Exists(newFilePath))
            {
                string newFileName = $"{fileNameWithoutExtension} ({counter})";
                newFilePath = Path.Combine(directory, newFileName + extension);
                counter++;
            }

            return newFilePath;
        }
        public async Task RemoveBack(string inputfilepath, string outputfilepath)
        {
            if (audioFile != null)
            {
                await FFMpegArguments
                .FromFileInput(inputfilepath)
                .OutputToFile(outputfilepath, overwrite: true, options => options
                    .WithCustomArgument("-af afftdn"))
                .ProcessAsynchronously();
            }
        }
        public bool isMicOk()
        {
            var waveInDevices = WaveInEvent.DeviceCount;
            return waveInDevices > 0;
        }
        private float CalculateCurrentAmplitude()
        {
            // Global değişkende saklanan son hesaplanan maksimum amplitüdü döndürün
            return lastMaxAmplitude;
        }
    }
}
