using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.CompilerServices;

namespace Kursach
{
    public partial class Firewatch : Form
    {
        // Путь к настройкам.
        const string settingspath = "settings.ini";
        // Путь к файлу скрипта сервера.
        const string pathtoserver = "../../Server/server.py";
        // Количество попыток подключения.
        const int maxattempt = 50;

        // Счетчик текущего изображения.
        static int currentimage = 0;
        // Пути к принятым на обработку файлам.
        static List<string> filenames = new List<string>();
        // Процесс, отвечающий за сервер.
        static System.Diagnostics.Process server;
        // Список предсказаний нейросети.
        static List<string> predictions = new List<string>();
        // Словарь для хранения путей к изображениям с соответствующими предсказаниями нейросети.
        static Dictionary<string, string> imagepredictions = new Dictionary<string, string>();
        // Попытка подключения к серверу.
        static int attempt = 0;

        public Firewatch()
        {
            try
            {
                Tools.StartServer(out server, pathtoserver);

                Icon = new Icon("../../Pics/Logo.ico");
            }
            catch (Win32Exception)
            {
                // Обработка ошибок при запуске сервера.
                MessageBox.Show("Unable to start the server!", "Error", MessageBoxButtons.OK);
                Close();
            }
            catch (IOException)
            {
                // Обработка ошибки при загрузке иконки.
                Icon = default;
            }

            InitializeComponent();

            // Запуск таймера для подключения к серверу.
            Thread.Sleep(50);
            ConnectTimer.Start();

            // Просто текст сообщения.
            PleaseWait.Text = ".." + Environment.NewLine + PleaseWait.Text + Environment.NewLine + "..";
        }

        /// <summary>
        /// Выбор изображений для обработки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectImages_Click(object sender, EventArgs e)
        {
            // Проверка на режим загрузки: индивидуальные файлы или папки.
            if (Settings.IfSingle)
            {
                MessageBox.Show("Select an image for analysis from a dialog box.",
                    "File Selection", MessageBoxButtons.OK);

                // Вызов метода для выбора файлов.
                if (ChooseFile())
                {
                    // Форматирование файлов для отправки.
                    filenames = Sender.Replace(filenames);

                    // Удаление неподдерживаемых файлов.
                    if(!Tools.RemoveUnsupported(ref filenames))
                    {
                        MessageBox.Show("Some of the selected files cannot be processed. They will be ignored.",
                            "Format Error",
                            MessageBoxButtons.OK);
                    }

                    // Запрос подтверждения обработки.
                    if (MessageBox.Show(
                        $"Would you like to proceed to classifying? {filenames.Count} images selected.",
                        "",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Запрос к серверу на обработку изображений.
                        SendPred();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a folder for analysis from a dialog box.",
                    "Folder Selection", MessageBoxButtons.OK);

                string directory;

                // Вызов метода для выбора папки.
                if (ChooseFolder(out directory))
                {
                    // Получение файлов, удаление неподдерживаемых.
                    string result = Tools.GetAndRemove(out filenames, directory);

                    if(result == "unreachable")
                    {
                        // Показ сообщения об ошибке.
                        MessageBox.Show("Unable to reach to directory!", "Error", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if(result == "incorrect")
                        {
                            MessageBox.Show("Some of the selected files cannot be processed. They will be ignored.",
                                "Format Error",
                                MessageBoxButtons.OK);
                        }

                        // Запрос подтверждения на обработку изображений.
                        if (MessageBox.Show(
                            $"Would you like to proceed to classifying? {filenames.Count} images selected.",
                            "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Запрос к серверу на ообработку изображений.
                            SendPred();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Выбор папки для анализа из диалогового окна.
        /// </summary>
        /// <returns>Выбрана ли какая-либо папка</returns>
        bool ChooseFolder(out string directory)
        {
            directory = string.Empty;

            // Открытие диалогового окна.
            folderBrowserDialog1.Description = default;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Присвоение пути к папке соответствующей переменной.
                directory = folderBrowserDialog1.SelectedPath;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Выбор файлов для обработки.
        /// </summary>
        /// <returns></returns>
        bool ChooseFile()
        {
            // Разрешение выбора нескольких файлов, так как этот диалог используется еще в одном месте.
            openFileDialog1.Multiselect = true;

            // Открытие диалогового окна.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Очистка переменной путей к файлам.
                filenames = new List<string>();

                // Заполнение путей.
                foreach (var file in openFileDialog1.FileNames)
                {
                    filenames.Add(file);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Обработка закрытия приложения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Закрытие сервера.
                server.Kill();
            }
            catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException)
            {
                // Вывод сообщения об ошибке.
                MessageBox.Show("Unable to shut down the server!", "Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Скрытие и показ элементов в состоянии, когда приложение подключено к серверу.
        /// </summary>
        void Connected()
        {
            PleaseWait.Hide();
            SelectImages.Show();
            IfMulticlass.Show();
            IfSingle.Show();
            Frame1.Show();
            UploadWeights.Show();
            Frame2.Show();
            SettingsLabel.Show();
            SaveLogs.Show();
            SaveSettings.Show();

            // Попытка загрузки настроек.
            if (!Settings.LoadSettings(settingspath))
            {
                MessageBox.Show("Error while loading settings", "Error", MessageBoxButtons.OK);
            }

            // Обновление чекбоксов.
            IfMulticlass.Checked = Settings.IfMulticlass;
            IfSingle.Checked = Settings.IfSingle;
            SaveLogs.Checked = Settings.SaveLogs;

            // Загрузка изображений к кнопкам.
            try
            {
                Next.BackgroundImage = new Bitmap("../../Pics/ArrowRight.png");
                Prev.BackgroundImage = new Bitmap("../../Pics/ArrowLeft.png");
            }
            catch
            {
                MessageBox.Show("Some files have not been loaded properly.", "Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Тик таймера подключения к серверу. Новая попытка или программа сдается и умирает (но не совсем)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Проверка на превышение планки максимальной попытки подключения.
            if (attempt >= maxattempt)
            {
                // Остановка таймера.
                ConnectTimer.Stop();
                
                // Вывод сообщения об ошибке.
                MessageBox.Show("Unable to connect to server! The app will be closed.");

                // Закрытие приложения.
                Close();
            }
            else
            {
                // Повторная попытка подключения.
                attempt++;
                string response = Sender.TryToConnect();

                // При успехе остановка таймера, вызов соответствующего делегата.
                if (response == "connected")
                {
                    ConnectTimer.Stop();
                    Connected();
                }
                else
                {
                    if(response == "error")
                    {
                        ConnectTimer.Stop();
                        MessageBox.Show("Error while loading a neural network model! The app will be closed.", "Error", MessageBoxButtons.OK);
                        Close();
                    }
                }
            }

            // Изменение строки сообщения, потому что мне так хочется.
            if (PleaseWait.Text.Contains("......................................"))
            {
                PleaseWait.Text = PleaseWait.Text.Substring(36, PleaseWait.Text.Length - 72);
            }
            else
            {
                PleaseWait.Text = "...." + PleaseWait.Text + "....";
            }
        }
        
        

        /// <summary>
        /// Метод для отправки запроса на загрузку весов к серверу.
        /// </summary>
        void SendPred()
        {
            // Обновление интерфейса.
            HideAll();
            WaitForPred.Text = "Please wait: the images are being processed";
            WaitForPred.Show();
            Refresh();

            // Флаг получения ответа.
            bool gotit = true;

            // Попытка отправки запроса и получения ответа.
            try
            {
                predictions = Sender.PredictRequest(filenames, Settings.IfMulticlass);
            }
            catch (Exception)
            {
                // Вывод сообщения об ошибке.
                MessageBox.Show("Error while classifying images", "Error", MessageBoxButtons.OK);

                // Возвращение на главный экран.
                WaitForPred.Hide();
                Connected();
                gotit = false;
            }

            if (gotit)
            {
                // Активация делегата полученных предсказаний.
                AnalyzePredictions();
            }
        }


        /// <summary>
        /// Обработка полученных предсказаний.
        /// </summary>
        void AnalyzePredictions()
        {
            // Очищение словаря путей с предсказаниями.
            imagepredictions = new Dictionary<string, string>();

            // Заполнение словаря путями и предсказаниями.
            for (int i = 0; i < Math.Min(filenames.Count, predictions.Count); i++)
            {
                imagepredictions.Add(filenames[i], predictions[i]);
            }

            // Проверка на необходимость сохранять журналы классификации.
            if (Settings.SaveLogs)
            {
                if (!Tools.SaveLogs(imagepredictions))
                {
                    MessageBox.Show("Error while saving logs!", "Error", MessageBoxButtons.OK);
                }
            }

            // Количество файлов, классифицированных, как дым.
            int wildfires = 0;

            // Подсчет количества изображений, классифицированных, как дым.
            foreach (var pred in predictions)
            {
                if (pred == "Wildfire")
                {
                    wildfires++;
                }
            }

            // Вывод сообщения о количестве изображений, классифицированных, как дым. Отныне сокращаю ИККД.
            WaitForPred.Text = $"{wildfires} images have been classified as wildfires.";

            // Показ сообщения.
            WaitForPred.Show();

            // Показ кнопки просмотра изображений.
            ViewImages.Show();

            // Показ кнопки "Назад"
            BackBut.Show();

            if (wildfires > 0)
            {
                // Проверка необходимости сохранения ИККД в отдельную папку.
                if (MessageBox.Show("Would you like to save images classified as wildfires to separate folder?",
                    "Choose", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string savedir;
                    // Выбор папки для сохранения.
                    if (ChooseSaveFolder(out savedir))
                    {
                        if (!Tools.SaveImages(savedir, imagepredictions))
                        {
                            // Вывод сообщения об ошибке сохранения.
                            MessageBox.Show("Some images could not be saved!", "Error", MessageBoxButtons.OK);
                        }
                        else
                        {
                            // Вывод сообщения об успешности сохранения.
                            MessageBox.Show("Images have been saved to selected folder.", "Success", MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Нажатие кнопки"Назад".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBut_Click(object sender, EventArgs e)
        {
            // Скрытие и показ соответствующих элементов.
            WaitForPred.Hide();
            BackBut.Hide();
            ViewImages.Hide();
            LoadImage.Hide();
            Pics.Hide();
            PredClass.Hide();
            Next.Hide();
            Prev.Hide();
            currentimage = 0;
            // BackColor = Color.FromArgb(33, 34, 38);
            Connected();
        }

        /// <summary>
        /// Загрузка весов для нейросети.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UploadWeights_Click(object sender, EventArgs e)
        {
            // Показ сообщения о выборе весов.
            MessageBox.Show(
                "Select weights for the neural network to use from the dialog. Weights will be automatically assigned to the suitable network.",
                "Selection",
                MessageBoxButtons.OK);

            string path;

            // Выбор весов.
            if (ChooseWeights(out path))
            {
                // Проверка на соответствие типу файла.
                if (path.EndsWith(".index"))
                {
                    // Запрос одобрения на загрузку весов.
                    if (MessageBox.Show("Proceed with uploading weights?", "Choose", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Отправка запроса на загрузку весов к серверу.
                        SendWeights(path);

                        // Возвращение на главный экран.
                        Connected();
                        WaitForPred.Hide();
                    }
                }
                else
                {
                    // Вывод сообщния о несоответствии типа файла.
                    MessageBox.Show("Pleаse choose a file containing weights.", "Error", MessageBoxButtons.OK);
                }
            }
        }

        /// <summary>
        /// Метод для отправки запроса на обработку изображений к серверу.
        /// </summary>
        void SendWeights(string path)
        {
            // Обновление интерфейса.
            HideAll();
            WaitForPred.Text = "Please wait: the weights are being uploaded...";
            WaitForPred.Show();
            Refresh();

            // Вывод сообщения об успехе или провале загрузки весов.
            if (Sender.WeightsRequest(path))
            {
                MessageBox.Show("Weights have been successfully uploaded", "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error while uploading weights!", "Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Выбор весов.
        /// </summary>
        /// <returns>Выбран ли файл.</returns>
        bool ChooseWeights(out string weightspath)
        {
            weightspath = string.Empty;

            // Показ соответствующего окна, выбор файла.
            if (weightUpload.ShowDialog() == DialogResult.OK)
            {
                weightspath = weightUpload.FileName;

                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Скрытие всех элементов управления (ну, всех, которые нужно скрывать).
        /// </summary>
        void HideAll()
        {
            SelectImages.Hide();
            IfMulticlass.Hide();
            IfSingle.Hide();
            Frame1.Hide();
            Frame2.Hide();
            SettingsLabel.Hide();
            UploadWeights.Hide();
            SaveLogs.Hide();
            SaveSettings.Hide();
        }

        /// <summary>
        /// Сохранение настроек.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveSettings_Click(object sender, EventArgs e)
        {
            if (!Settings.SaveSettings(settingspath))
            {
                // Вывод сообщения об ошибке.
                MessageBox.Show("Error while saving settings", "Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Выбор папки для сохранения изображений.
        /// </summary>
        /// <returns>Выбрана ли папка.</returns>
        bool ChooseSaveFolder(out string savedir)
        {
            savedir = string.Empty;

            // Ну тут все очевидно, ну правда.
            folderBrowserDialog1.Description = "Select a folder to save images.";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                savedir = folderBrowserDialog1.SelectedPath;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Просмотр изображений.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewImages_Click(object sender, EventArgs e)
        {
            // Скрытие и показ соответствующих элементов интерфейса.
            ViewImages.Hide();
            WaitForPred.Hide();

            Pics.Show();
            PredClass.Show();
            Prev.Show();
            Next.Show();
            LoadImage.Show();

            // Показ начального изображения.
            if (imagepredictions.Count > 0)
            {
                ShowImage(currentimage);
            }
            else
            {
                // Обработка пустого словаря предсказаний.
                PredClass.Text = "Error";
                Pics.Image = Pics.ErrorImage;
            }
        }

        /// <summary>
        /// Следующее изображение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_Click(object sender, EventArgs e)
        {
            // Проверка на наличие следующего.
            if (currentimage + 1 < imagepredictions.Keys.Count)
            {
                currentimage++;

                ShowImage(currentimage);
            }
        }

        /// <summary>
        /// Предыдущее изображение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Prev_Click(object sender, EventArgs e)
        {
            // Проверка на выход за пределы словаря.
            if (currentimage - 1 < imagepredictions.Keys.Count && currentimage - 1 >= 0)
            {
                currentimage--;

                ShowImage(currentimage);
            }
        }

        /// <summary>
        /// Выбор изображения для просмотра.
        /// </summary>
        /// <returns></returns>
        bool ChooseImage(out string loadedimage)
        {
            loadedimage = string.Empty;

            // Вот и понадобилось изменение мультиселекта, потому что тут он запрещен.
            openFileDialog1.Multiselect = false;

            // Выбор файла в диалоговом окне.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Проверка на соответствие разрешенным форматам.
                if (Tools.AllowedFormats.Contains(openFileDialog1.FileName.Split('.').Last()))
                {
                    loadedimage = openFileDialog1.FileName;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Загрузка изображения для просмотра.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadImage_Click(object sender, EventArgs e)
        {
            // Просто сообщение пользователю.
            MessageBox.Show("Choose an image to view", "", MessageBoxButtons.OK);

            string loadedimage;

            // Выбор файла.
            if (ChooseImage(out loadedimage))
            {
                // Попытка загрузки из файла.
                try
                {
                    Pics.Image = Image.FromFile(loadedimage);

                    PredClass.Text = Tools.GetClass(loadedimage, imagepredictions);
                }
                catch (Exception ex) when (ex is IOException || ex is System.Security.SecurityException || ex is UnauthorizedAccessException)
                {
                    Pics.Image = Pics.ErrorImage;
                        PredClass.Text = "Error";
                }
            }
        }

        // Обновление класса настроек при изменении статуса соответствующих чекбоксов.
        private void IfSingle_CheckedChanged(object sender, EventArgs e)
        {
            Settings.IfSingle = IfSingle.Checked;
        }
        private void IfMulticlass_CheckedChanged(object sender, EventArgs e)
        {
            Settings.IfMulticlass = IfMulticlass.Checked;
        }
        private void SaveLogs_CheckedChanged(object sender, EventArgs e)
        {
            Settings.SaveLogs = SaveLogs.Checked;
        }

        void ShowImage(int index)
        {
            // Попытка чтения из файла.
            try
            {
                Pics.Image = Image.FromFile(imagepredictions.Keys.ToList()[index]);
                PredClass.Text = imagepredictions[imagepredictions.Keys.ToList()[index]];
            }
            catch (Exception e) when (e is IOException || e is System.Security.SecurityException || e is UnauthorizedAccessException)
            {
                Pics.Image = Pics.ErrorImage;
                PredClass.Text = "Error";   
            }
        }
    }
}
