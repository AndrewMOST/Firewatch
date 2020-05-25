using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.IO;

namespace Kursach
{
    public static class Sender
    {
        // Сервер, на котором запускается обработка нейросетью.
        const string uri = "http://127.0.0.1:5000/";

        /// <summary>
        /// Попытка подключения к серверу.
        /// </summary>
        /// <returns>Строка состояния подключения</returns>
        public static string TryToConnect()
        {
            // Создание GET-запроса по адресу сервера.
            WebRequest request = WebRequest.Create($"{uri}index");
            request.Method = "GET";

            // Попытка получить ответ от сервера.
            // Возврат соответствующего результата.
            try
            {
                WebResponse response = request.GetResponse();

                Stream dataStream;
                string responsefromserver;

                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responsefromserver = reader.ReadToEnd();
                }

                if(responsefromserver == "000")
                {
                    return "connected";
                }
                if(responsefromserver == "200")
                {
                    return "error";
                }
                return "unable";
            }
            catch (Exception)
            {
                return "unable";
            }
        }

        /// <summary>
        /// Отправка запроса к серверу на обработку изображений.
        /// </summary>
        public static List<string> PredictRequest(List<string> paths, bool ifmulticlass)
        {
            // Создание строки из путей к файлам.

            /*
             * Тут стоит пояснить, что можно было использовать какой-нибудь широкодоступный формат, типа JSON,
             * но используемые запросы содержат только информацию одного типа, которую легко запаковать.
             * Тем более, подлый С# все равно все кодирует в один большой массив байт,
             * с раскодированием которого на сервере мне возиться не хочется. So here:
             * строка из имен файлов.
             * 
             * В качестве разделителя взят символ *, так как его не может быть в именах файлов.
             */

            string datastr = string.Join("*", paths);

            // Кодирование строки в массив байтов.
            byte[] data = Encoding.UTF8.GetBytes(datastr);

            // Создание запроса.
            WebRequest request;

            // Определение модели нейросети, к которой подается запрос.
            if (ifmulticlass)
            {
                request = WebRequest.Create(uri + "predictmanymulti");
            }
            else
            {
                request = WebRequest.Create(uri + "predictmanybinary");
            }

            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            List<string> predictions = new List<string>();

            // Расшифровка ответа.
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                predictions = responseFromServer.Split('*').ToList();
            }

            return predictions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static List<string> Replace(List<string> paths)
        {
            // Форматирование строк путей к файлам для избежания проблем с кодировкой.
            for (int i = 0; i < paths.Count; i++)
            {
                paths[i] = paths[i].Replace('\\', '/');
            }

            return paths;
        }

        /// <summary>
        /// Запрос на загрузку весов.
        /// </summary>
        public static bool WeightsRequest(string weightspath)
        {
            // Создание массива байтов для отправки.
            byte[] data = Encoding.UTF8.GetBytes(weightspath);

            // Создание POST-запроса
            WebRequest request;

            // Путь к запросу. Сначала для бинарной модели.
            request = WebRequest.Create(uri + "uploadweightsbinary");

            // Флаг успешности запроса.
            bool gotresponse = true;

            // Попытка загрузки весов.

            /*
             * Сначала происходит попытка загрузить веса в бинарную модель.
             * Если по какой-то причине сервер выдал ошибку (несоответствие размеров тензоров, отсутствие необходимых файлов),
             * запрос посылается для многоклассовой модели.
             * Если опять что-то не получается, то не судьба - больше не пытаемся.
             */

            try
            {
                request.Method = "POST";
                request.ContentLength = data.Length;
                request.ContentType = "application/json";
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                gotresponse = true;
            }
            catch
            {
                // Изменение флага успешности.
                gotresponse = false;
            }

            // Попытка загрузить веса для другой модели, если первая попытка безуспешна.
            if (!gotresponse)
            {
                // Все то же самое, но на другую страницу.
                request = WebRequest.Create(uri + "uploadweightsmulti");

                try
                {
                    request.Method = "POST";
                    request.ContentLength = data.Length;
                    request.ContentType = "application/json";
                    request.ContentType = "application/json";
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(data, 0, data.Length);
                    dataStream.Close();

                    WebResponse response = request.GetResponse();
                    gotresponse = true;
                }
                catch
                {
                    gotresponse = false;
                }
            }

            return gotresponse;
        }
    }
}
