using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Helpers;
using Guna.UI2.WinForms;
using Mp3Oynatici;
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

            gecenSure.Text = "00:00:00";  // ses dosyasının anlık süre label'ına değer atanıyor
            toplamSure.Text = "00:00:00"; // ses dosyasının toplam süre label'ına değer atanıyor
            Volume.Value = 50; // ses trackbar'ının başlangıç değeri yarıya çekiliyor

            timer.Stop();
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e) // timer'ın tetiklenme olayı
        {
            if (audioFile != null && audioFile.CurrentTime.TotalMilliseconds <= audioFile.TotalTime.TotalMilliseconds) // import edilen dosya varsa ve anlık zaman toplam zamandan küçük veya eşitse
            {
                gecenSure.Text = audioFile.CurrentTime.ToString(@"hh\:mm\:ss");  // anlık süre label'ının değeri ses dosyasının anlık süresi olarak atanıyor
            }
            btnVolume.Invalidate(); // ses buton'u güncelleniyor (ses düzeyine göre ikon değişikliği için)
            graphic.Invalidate(); // grafik panel'i güncelleniyor
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
        }
        /****************************************** Butonlar ******************************************/
        private async void btnOpen_Click(object sender, EventArgs e) // import butonuna tıklanma olayı
        {
            ofd.Filter = "Audio files (*.wav;*.mp3;*.aac)|*.wav;*.mp3;*.aac"; // import edilen dosyası seçme ekranında görünecek dosya formatları
            if (ofd.ShowDialog() == DialogResult.OK) // dosya seçildiğinde
            {
                if (waveOut != null) // Zaten import edilen dosya varsa durdur
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
                if (audioFile != null) // Zaten import edilen dosya varsa durdur
                {
                    audioFile.Dispose();
                    audioFile = null;
                }

                string filePath = ofd.FileName;

                if (filePath.EndsWith(".aac")) // dosya aac uzantılıysa wav'a çevirme
                {
                    instaConvert(filePath);
                }
                audioFile = new AudioFileReader(filePath); // seçilen dosyayı oynatma

                waveOut = new WaveOutEvent(); 
                waveOut.Init(audioFile);
                btnPlay.Image = imagePlay;


                toplamSure.Text = audioFile.TotalTime.ToString(@"hh\:mm\:ss"); // toplam süre label'ına dosyanın toplam süresini yazdırma

                waveOut.PlaybackStopped += OnPlayBackStopped; // OnPlayBackStopped metodunu dosyanın bitmesiyle ilişkilendirme
                await Task.Run(() => loadAudioData()); // ses dosyasını işleyecek olan metodu arkaplanla çalıştırma

                graphic.Invalidate(); // dosya eklendiğinde grafik panel'ini tek seferli güncelleme

                {   // dosya ismi label'ına dosyanın ismini yazdırma
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

                gecenSure.Text = audioFile.CurrentTime.ToString(@"hh\:mm\:ss"); 
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

                gecenSure.Text = audioFile.CurrentTime.ToString(@"hh\:mm\:ss");
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
                    string directory = Path.GetDirectoryName(input);
                    string fileName = Path.GetFileNameWithoutExtension(input);
                    string extension = Path.GetExtension(input);

                    sfd.Filter = ofd.Filter;
                    sfd.DefaultExt = ofd.Filter;
                    sfd.FileName = Path.Combine(directory, $"Changed_{fileName}{extension}");
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

        /****************************************** TrackBarlar ******************************************/
        private void Volume_Scroll(object sender, ScrollEventArgs e) // ses trackbar'ını kaydırma olayı
        {
            if (waveOut != null)
            {
                waveOut.Volume = Volume.Value / 100f; // trackbar'ın değerine göre oynatılan dosyanın sesini ayarlama işlemi
            }
        }

        /****************************************** Paneller ******************************************/
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) // grafik panelinin görselinin değişme olayı
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black); // Grafiği çizecek kalemi oluşturma

            int width = graphic.Width;
            int height = graphic.Height;
            int midHeight = height / 2;
            string fileName = ofd.FileName;
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
                    int playPositionX = (int)((audioFile.CurrentTime.TotalMilliseconds / audioFile.TotalTime.TotalMilliseconds) * graphic.Width);
                    if (playPositionX > graphic.Width)
                        playPositionX = graphic.Width;
                    if (isMouseDown)
                        g.DrawLine(pen, playPositionX, 0, playPositionX, graphic.Height); // fare ile grafiğin üzerinden ses dosyasının yönetiminin takibi için görsel oluşturma işlemi
                    else if (isStopped)
                    {
                        g.DrawLine(pen, graphic.Width - 1, 0, graphic.Width - 1, graphic.Height); // ses dosyası oynatması bittiğinde sona gelindiğini gösteren görseli oluşturma işlemi
                    }
                    else
                        g.DrawLine(pen, playPositionX, 0, playPositionX, graphic.Height); // grafik üzerinde ses dosyasının anlık takibinin yapılabileceği görseli oluşturma işlemi
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
        private void graphic_MouseClick(object sender, MouseEventArgs e) // panele fare ile tıklama olayı
        { // ses dosyasının oynatma konumunun panel üzerinde tıklanan yere getirilme işlemi
            if (audioFile != null)
            {
                float percentage = (float)e.X / graphic.Width;
                double newTime = percentage * audioFile.TotalTime.TotalMilliseconds;
                audioFile.CurrentTime = TimeSpan.FromMilliseconds(newTime);
            }
        }
        private void guna2Panel1_MouseEnter(object sender, EventArgs e) // fare ile panel üzerine gelme olayı
        {
            if (audioFile != null)
            {
                graphic.Cursor = Cursors.Hand; // fare ile panelin üzerine gelindiğinde farenin görselinin "el" şekline dönüşmesi işlemi
            }
        }
        private void guna2Panel1_MouseMove(object sender, MouseEventArgs e) // fare ile panelin üzerinde tıklanıp kaydırma olayı
        {
            if (isMouseDown && audioFile != null)
            {
                float percentage = ((float)e.X * (float)audioFile.TotalTime.TotalMilliseconds) / graphic.Width;
                if (percentage < 0)
                    percentage = 0;
                audioFile.CurrentTime = TimeSpan.FromMilliseconds(percentage); // fare ile panel üzerinde kaydırma anında anlık olarak dosyanın oynatma takibinin yapılma işlemi
            }
        }
        private void guna2Panel1_MouseDown(object sender, MouseEventArgs e) // fare ile panele basılı tutma olayı
        { 
            if (audioFile != null)
            {
                isMouseDown = true;
                waveOut?.Stop(); // fare ile panelin üzerine basılı tutulduğu anda çalınan ses dosyasının durdurulma işlemi
            }
        }
        private void guna2Panel1_MouseUp(object sender, MouseEventArgs e) // panel üzerinde basılı tutulan farenin basılı tutmanın bırakılma olayı
        {
            if (audioFile != null)
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
            gecenSure.Text = toplamSure.Text;
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

        private void Form1_Resize(object sender, EventArgs e) // From1'in boyutunun kullanıcı tarafından değiştirilme olayı
        {
            Volume.Width = this.Width / 5; // ses trackbarı'nın genişliği, bulunduğu formun genişliğinin 5'te 1'i olarak ayarlanması işlemi
        }

    }
}
