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
                "https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669517/1651036-C1UEIFOXO4HN.mp4",
            };

            int i = 1;
            foreach (var url in urls)
            {
                var headers = new WebHeaderCollection
            {
                // {"Sec-Ch-Ua", "\"Chromium\";v=\"112\", \"Google Chrome\";v=\"112\", \"Not:A-Brand\";v=\"99\""},
                // {"Dnt", "1"},
                // {"Accept-Encoding", "gzip, deflate"},
                // {"Sec-Ch-Ua-Mobile", "?0"},
                // {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36"},
                // {"Sec-Ch-Ua-Platform", "\"Windows\""},
                // {"Accept", "*/*"},
                // {"Sec-Fetch-Site", "cross-site"},
                // {"Sec-Fetch-Mode", "no-cors"},
                // {"Sec-Fetch-Dest", "video"},
                 {"Referer", "https://app.realplay.us/"},
                // {"Accept-Language", "en-US,en;q=0.9"},
                // {"Range", "bytes=0-100000000"},
                // {"If-Range", "\"078e6c0a41bf4fbab098dcfa08cb14f2\""}
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
