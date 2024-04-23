using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
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
                "https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669508/1651014-D52LFAP3XX76.mp4",    
            };

            var downloadTask = new List<Task>();
            foreach (var url in urls)
            {
                downloadTask.Add(DownloadFile(url, $"c:\\gc-downloads\\{Guid.NewGuid()}.mp4"));
            }

            Console.WriteLine($"Downloading { downloadTask.Count } files");
            Task.WaitAll(downloadTask.ToArray());
            Console.WriteLine("Download completed!");

        }

        static async Task DownloadFile(string url, string filePath)
        {
            try
            {
                var myClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
                myClient.DefaultRequestHeaders.Add("Referer", "https://app.realplay.us/");
                
                using (var response = await myClient.GetAsync(url))
                using (var stream = await response.Content.ReadAsStreamAsync())
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
