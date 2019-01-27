using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeShare
{
    class Commands
    {
        public static async Task Execute(DataFormatter.FormattedPasteBinData data)
        {
            switch(data.Command.ToLower())
            {
                case "play":
                    string RemoveFilePath = await DownloadFile.Get(data.AudioSource);
                    Audio.Play(RemoveFilePath, data.Volume, data.StartPosition, data.Delay);
                    Console.WriteLine($"Playing: {data.AudioSource}");

                    if (RemoveFilePath != null)
                    {
                        if (File.Exists(RemoveFilePath))
                        {
                            // Cleanup
                            // This will delete the file when the program is closed
                            File.Delete(RemoveFilePath);
                        }
                    }
                    break;

                case "playlocal":
                    string path = @"LocalAudio\" + data.AudioSource + ".mp3";

                    if (File.Exists(path))
                    {

                        Audio.Play(path, data.Volume, data.StartPosition, data.Delay);
                        Console.WriteLine($"Playing(local): {data.AudioSource}");
                    } else
                    {
                        Console.WriteLine("ERROR: local file not found");
                    }
                    break;

                case "stop":
                    Audio.Stop();
                    Console.WriteLine("Stopping..");
                    break;
            }
        }
    }
}
