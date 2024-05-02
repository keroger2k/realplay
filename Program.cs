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
            string PATH = @"c:\\gc-downloads\\";
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                PATH = @"/tmp/gc-downloads/";
                Console.WriteLine("Running on a mac...");
            }

            var urls = new List<string>()
            {

"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669469/1650922-8AQ90S8B1US9.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669470/1650924-UPLHR3RO1E0F.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669471/1650927-LP9AUAQ6M6LK.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669472/1650929-KQ5NBOMMY49F.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669473/1650931-NE1AVOQECRS1.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669481/1650944-6SMC3V3C1X9I.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669481/1650946-1GQBJC62S2WG.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669482/1650948-L5OGFZEUX8GA.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669483/1650951-EXCR846SOE9D.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669478/1650938-6AMTOI30RH7M.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669493/1650977-O0E02IH8V3DO.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669494/1650980-3ELBKYGS4K66.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669510/1651021-MCBIFE20QB22.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/8edc0e14-c11d-4560-b438-7799288149af/669511/1651025-YJ6BYZAZROGQ.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/da302e33-9787-41d2-95a9-e9db8c124194/671078/1655271-WSQFUI1KKCN0.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/da302e33-9787-41d2-95a9-e9db8c124194/671083/1655285-RBZOW6T0QGEY.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/da302e33-9787-41d2-95a9-e9db8c124194/671109/1655351-ID5UGMMN45OU.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/da302e33-9787-41d2-95a9-e9db8c124194/671158/1655446-MCII8EGIW5TQ.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/13/da302e33-9787-41d2-95a9-e9db8c124194/671198/1655542-5MG1JEY3WS4T.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/15/fc40aec2-5d68-477f-885d-d7fc5aeea617/671908/1657304-6DYF69IEXMLM.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/15/fc40aec2-5d68-477f-885d-d7fc5aeea617/671910/1657312-Q1UWP3PB87HS.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/15/fc40aec2-5d68-477f-885d-d7fc5aeea617/671932/1657368-IFFJPTHMALB6.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/15/fc40aec2-5d68-477f-885d-d7fc5aeea617/671933/1657377-XINZCEV62V42.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/15/fc40aec2-5d68-477f-885d-d7fc5aeea617/671936/1657382-NMOZ7HIKLE80.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/15/fc40aec2-5d68-477f-885d-d7fc5aeea617/671953/1657422-THUA7A0Q140D.mp4",
"https://realplay-app.s3.amazonaws.com/videos/2024/15/fc40aec2-5d68-477f-885d-d7fc5aeea617/671987/1657514-KTO837TPJ025.mp4",
            };

            Console.WriteLine($"Downloading { urls.Count } files");
            foreach (var url in urls)
            {
                DownloadFile(url, PATH).Wait();
            }
            Console.WriteLine($"Downloads complete!");
        }

        static async Task DownloadFile(string url, string path)
        {
            try
            {
                var myClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
                myClient.DefaultRequestHeaders.Add("Referer", "https://app.realplay.us/");
                var segments = url.Split('/');
                var filePath = segments.ElementAt(segments.Count() - 1);
                
                using (var response = await myClient.GetAsync(url))
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = System.IO.File.Create(path + filePath))
                {
                    stream.CopyTo(fileStream);
                }
                Console.WriteLine($"Success: { url }");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while downloading the file " + url + ": " + ex.Message);
            }
        }
    }
}
