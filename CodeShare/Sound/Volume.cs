using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CodeShare
{
    class Volume
    {
        public static void Set(int vol)
        {
            for(int i = 0; i < 51; i++)
            {
                VolDown();
            }

            for (int f = 0; f < vol/2; f++)
            {
                VolUp();
            }
        }

        private static readonly IntPtr Handle = Process.GetCurrentProcess().MainWindowHandle;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);

        private static void Mute()
        {
            SendMessageW(Handle, 0x80000, Handle,
                (IntPtr)0x80000);
        }

        private static void VolDown()
        {
            SendMessageW(Handle, 0x319, Handle,
                (IntPtr)0x90000);
        }

        private static void VolUp()
        {
            SendMessageW(Handle, 0x319, Handle,
                (IntPtr)0xA0000);
        }
    }
}
