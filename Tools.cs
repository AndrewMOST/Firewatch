using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kursach
{
    public static class Tools
    {

        // Разрешенные форматы изображений.
        public static string[] AllowedFormats { get
            {
                return new string[] { "png", "jpg", "jpeg", "PNG", "JPG", "JPEG" };
            }
        }

        /// <summary>
        /// Удаление неподдерживаемых файлов.
        /// </summary>
        /// <param name="filenames">Список путей к файлам</param>
        /// <returns>Все ли файлы подлежат обработке</returns>
        public static bool RemoveUnsupported(ref List<string> filenames)
        {
            // Список файлов на удаление.
            List<string> toremove = new List<string>();

            // Флаг возможности обработки всех файлов.
            bool isallowed = true;

            // Определение возможности обработки каждого файла.
            foreach (var filename in filenames)
            {
                // Флаг отдельного файла.
                bool oneallowed = false;

                // Проверка соответствия файла поддерживаемым форматам.
                foreach (var format in AllowedFormats)
                {
                    if (filename.EndsWith(format))
                    {
                        oneallowed = true;
                        break;
                    }
                }

                // Если файл не поддерживается, отрежение этого в переменной для всех файлов.
                if (!oneallowed)
                {
                    isallowed = false;
                    // Добавление файла в список на удаление.
                    toremove.Add(filename);
                }
            }

            // Удаление неподдерживаемых файлов.
            if (!isallowed)
            {
                // Удаление неподдерживаемых файлов.
                foreach (var filename in toremove)
                {
                    filenames.Remove(filename);
                }
            }

            return isallowed;
        }

        /// <summary>
        /// Сохранение журнала классификации.
        /// </summary>
        /// <param name="imagepredictions">Словарь путей к файлам с соответствующими предсказаниями</param>
        /// <returns>Успешно ли сохранение</returns>
        public static bool SaveLogs(Dictionary<string, string> imagepredictions)
        {

            // Создание массива строк для записи в журнал.
            string[] linestowrite = new string[imagepredictions.Count];
            int counter = 0;

            // Заполнение массива-журнала.
            foreach (var key in imagepredictions.Keys)
            {
                linestowrite[counter] = key + "\t:\t" + imagepredictions[key];
                counter++;
            }

            // Создание имени журнала.
            string path = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" +
                DateTime.Now.Day + "--" + DateTime.Now.Hour + "-" +
                DateTime.Now.Minute + "-" + DateTime.Now.Second + ".txt";

            // Попытка записи журнала в файл.
            try
            {
                File.WriteAllLines(path, linestowrite);
                return true;
            }
            catch (Exception e) when (e is IOException || e is System.Security.SecurityException || e is UnauthorizedAccessException)
            {
                return false;
            }
        }

        /// <summary>
        /// Сохранение изображений, классифицированных, как дым (ИККД)
        /// </summary>
        /// <param name="savedir">Директория сохранения</param>
        /// <param name="imagepredictions">Словарь путей к файлам с соответствующими предсказаниями</param>
        /// <returns>Успешность сохранения</returns>
        public static bool SaveImages(string savedir, Dictionary<string, string> imagepredictions)
        {

            // Флаг успешности сохранения.
            bool savessuccessful = true;

            // Сохранение ИККД.
            foreach (var key in imagepredictions.Keys)
            {
                if (imagepredictions[key] == "Wildfire")
                {
                    try
                    {
                        // Сохранение названия файла.
                        string filename = key.Split('/').Last();

                        // Копирование файла в выбранную папку.
                        File.Copy(key, savedir + "/" + filename);
                    }
                    catch (Exception e) when (e is IOException || e is System.Security.SecurityException || e is UnauthorizedAccessException)
                    {
                        savessuccessful = false;
                    }
                }
            }

            // Возвращение успешности сохранения.
            if (!savessuccessful)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Получить класс изображения по пути к нему.
        /// </summary>
        /// <param name="loadedimage">Путь к данному изображению</param>
        /// <param name="imagepredictions">Словарь путей с предсказаниями</param>
        /// <returns>Класс изображения</returns>
        public static string GetClass(string loadedimage, Dictionary<string, string> imagepredictions)
        {
            // Если этот файл был классифицирован, изображние будет подписано данным классом.
            if (imagepredictions.ContainsKey(loadedimage.Replace('\\', '/')))
            {
                return imagepredictions[loadedimage.Replace('\\', '/')];
            }
            else
            {
                // Иначе подпись "Неизвестно".
                return "Unknown";
            }
        }

        /// <summary>
        /// Запуск сервера
        /// </summary>
        /// <param name="server">Объект-процесс, отвечающий за сервер</param>
        /// <param name="pathtoserver">Путь к скрипту сервера</param>
        public static void StartServer(out System.Diagnostics.Process server, string pathtoserver)
        {

            // Создание процесса, отвечающего за сервер.
            server = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "python.exe",
                    Arguments = "\"" + pathtoserver + "\"",

                    // Аргументы для запуска в "тихом режиме".
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            // Запуск сервера.
            server.Start();
        }

        /// <summary>
        /// Получение файлов из директории, удаление неподдерживаемых, подготовка к отправке на сервер.
        /// </summary>
        /// <param name="filenames">Список путей к файлам</param>
        /// <param name="directory">Директория для обработки</param>
        /// <returns>Статус операции</returns>
        public static string GetAndRemove(out List<string> filenames, string directory)
        {
            filenames = new List<string>();

            // Попытка чтения файлов из директории.
            try
            {
                filenames = Directory.GetFiles(directory).ToList<string>();
            }
            catch (Exception e) when (e is IOException || e is System.Security.SecurityException || e is UnauthorizedAccessException)
            {
                return "unreachable";
            }

            // Форматирование путей.
            filenames = Sender.Replace(filenames);


            // Удаление неподдерживаемых файлов.
            if (RemoveUnsupported(ref filenames))
            {
                return "correct";
            }
            else
            {
                return "incorrect";
            }
        }
    }
}
