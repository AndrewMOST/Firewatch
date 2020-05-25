using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kursach
{
    public static class Settings
    {
        static Settings()
        {
            IfMulticlass = false;
            IfSingle = false;
            SaveLogs = false;
        }

        public static bool IfSingle { get; set; }

        public static bool SaveLogs { get; set; }

        public static bool IfMulticlass { get; set; }

        /// <summary>
        /// Загрузка настроек.
        /// </summary>
        /// <param name="path">Путь к файлу настроек</param>
        /// <returns>Успешность загрузки</returns>
        public static bool LoadSettings(string path)
        {
            // Массив строк настроек.
            string[] settings = new string[0];

            // Попытка чтения настроек из файла.
            try
            {
                settings = File.ReadAllLines(path);
            }
            catch (Exception e) when (e is IOException || e is System.Security.SecurityException || e is UnauthorizedAccessException)
            {
                return false;
            }

            // Флаг успеха обработки.
            bool parsesuccess = true;

            // Парс файла в словарь.
            Dictionary<string, string> settingsdict = new Dictionary<string, string>();

            for (int i = 0; i < settings.Length; i++)
            {
                try
                {
                    settingsdict.Add(settings[i].Split('=')[0], settings[i].Split('=')[1]);
                }
                catch(ArgumentOutOfRangeException)
                {
                    parsesuccess = false;
                }

            }

            // Возвращение информации о безуспещшной загрузке.
            if (!parsesuccess)
            {
                return false;
            }
            else
            {
                try
                {
                    // Интерпретация прочтенных настроек.
                    if (settingsdict["IfMulticlass"] == "True")
                    {
                        IfMulticlass = true;
                    }

                    if (settingsdict["IfSingle"] == "True")
                    {
                        IfSingle = true;
                    }

                    if (settingsdict["SaveLogs"] == "True")
                    {
                        SaveLogs = true;
                    }

                    return true;
                }
                catch (KeyNotFoundException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Сохранение настроек.
        /// </summary>
        /// <param name="path">Путь к файлу настроеке</param>
        /// <returns>Успешность сохранения</returns>
        public static bool SaveSettings(string path)
        {
            // Создание и заполнение массива строк настроек.
            string[] settings = new string[3];

            settings[0] = "IfMulticlass=" + IfMulticlass;
            settings[1] = "IfSingle=" + IfSingle;
            settings[2] = "SaveLogs=" + SaveLogs;

            // Попытка записи настроек в файл.
            try
            {
                File.WriteAllLines(path, settings);
                return true;
            }
            catch (Exception e) when (e is IOException || e is System.Security.SecurityException || e is UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}
