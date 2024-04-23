using System;
using System.IO;
using System.Net;
using static System.Net.WebRequestMethods;

namespace FileDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var urls = new List<string>()
            {
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669491/1650973-MYO0CPG770NK.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669508/1651014-D52LFAP3XX76.mp4"
            };

            int i = 1;
            foreach (var url in urls)
            {
                var headers = new WebHeaderCollection
            {
                 {"Referer", "https://app.realplay.us/"},
            };
                var filePath = $"c:\\gc-downloads\\{i++}.mp4";
                DownloadFile(url, headers, filePath);
                Console.WriteLine("Download completed!");
            }
        }

        static void DownloadFile(string url, WebHeaderCollection headers, string filePath)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Headers = headers;

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var fileStream = System.IO.File.Create(filePath))
                {
                    stream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while downloading the file: " + ex.Message);
            }
        }
    }
}
