using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShare
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CheckFiles();

            string PrevId = "";
            bool first = true;

            DataFormatter Dformatter = new DataFormatter();
            DataFormatter.FormattedPasteBinData FPData = new DataFormatter.FormattedPasteBinData();
            DataFormatter.FormattedSettingsData FSData = new DataFormatter.FormattedSettingsData();

            // Load settings data
            FSData = Dformatter.DeserializeSettings(File.ReadAllText("Settings.json"));

            Console.WriteLine("Listening for commands...");

            while (true)
            {
                TryAgain:

                try
                {
                    FPData = Dformatter.DeserializePasteBin(
                        await PastebinRequest.DownloadString(FSData.PastebinURL));
                } catch(Exception ex)
                {
                    Console.WriteLine("Network connection down...");
                    Console.WriteLine("");
                    Console.WriteLine($"exception: {ex}");

                    await Task.Delay(TimeSpan.FromSeconds(5));
                    goto TryAgain;
                }

                if (PrevId != FPData.Id && first == false)
                {
                    await Commands.Execute(FPData);
                }

                first = false;

                PrevId = FPData.Id;

                // Debug
                //Console.WriteLine("looped");

                await Task.Delay(TimeSpan.FromSeconds(FSData.CheckingInterval));
            }
        }

        private static void CheckFiles()
        {
            if (!File.Exists("Newtonsoft.Json.dll") ||
                !File.Exists("WMPLib.dll") ||
                !File.Exists("Settings.json") ||
                !Directory.Exists("LocalAudio"))
            {
                Console.WriteLine("Error: Missing files/folders");

                // Thread Sleep because we are going to exit
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
    }
}
