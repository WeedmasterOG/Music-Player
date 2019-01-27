using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CodeShare
{
    class DownloadFile
    {
        public static async Task<string> Get(string url)
        {
            string FilePath;

            FilePath = Path.GetTempPath() + GenRandString.Gen(16) + ".mp3";

            try
            {
                // Create an instance of WebClient
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;

                    Console.WriteLine("Downloading file..");

                    // Start the download and copy the file to c:\temp
                    await client.DownloadFileTaskAsync(new Uri(url), Path.Combine(Path.GetTempPath(), FilePath));
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error downloading file..");
                Console.WriteLine(ex);

                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }

                return null;
            }

            return FilePath;
        }

        private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double percentage = Math.Round(double.Parse(e.BytesReceived.ToString()) /
                double.Parse(e.TotalBytesToReceive.ToString()) * 100);

            Console.WriteLine($"Percentage: {percentage}%");
        }
    }
}
