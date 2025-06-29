using System;
using System.Windows.Forms;
using MonoPingPong.Core;

namespace Core
{
    /// <summary>
    /// Application entry point for the MonoPingPong game.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            using var game = new PingPong();
            game.Run();
        }
    }
}