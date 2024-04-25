using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Net.WebRequestMethods;


namespace FileDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string PATH = @"c:\\gc-downloads\\{0}";
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                PATH = @"/tmp/gc-downloads/{0}";
                Console.WriteLine("Running on a mac...");
            }

            var urls = new List<string>()
            {
                "https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669495/1650982-YEPK5YVPJDFR.mp4",
                "https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669496/1650986-KVP3XX75MAAP.mp4",
                "https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669512/1651027-N0EQL84N66P7.mp4",
                "https://realplay-app.s3.amazonaws.com/videos/2024/13/da302e33-9787-41d2-95a9-e9db8c124194/671101/1655332-54HMSFQBSVVW.mp4",
                "https://realplay-app.s3.amazonaws.com/videos/2024/13/da302e33-9787-41d2-95a9-e9db8c124194/671144/1655409-QSVT7OOX93AM.mp4",
                "https://realplay-app.s3.amazonaws.com/videos/2024/13/da302e33-9787-41d2-95a9-e9db8c124194/671183/1655502-1SZIK6WOQUAQ.mp4",

            };

            var downloadTask = new List<Task>();
            foreach (var url in urls)
            {
                downloadTask.Add(DownloadFile(url, string.Format(PATH + ".mp4", Guid.NewGuid())));
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
