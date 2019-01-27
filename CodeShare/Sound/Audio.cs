using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShare
{
    class Audio
    {
        private static WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();

        public static void Play(string path, int volume, int startposition, int delay)
        {
            if (path != null)
            {
                // Incase audio was playing before
                Stop();

                player.controls.currentPosition = startposition;

                if (delay == 0)
                {
                    player.URL = path;
                }
                else
                {
                    Task.Factory.StartNew(() => {
                        Thread.Sleep(TimeSpan.FromSeconds(delay));
                        player.URL = path;
                    });
                }

                Volume.Set(volume);
            }
        }

        public static void Stop()
        {
            if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                player.controls.stop();
            }
        }
    }
}
