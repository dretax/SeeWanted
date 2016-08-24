using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SeeWantedServer
{
    internal sealed class Logger
    {
        internal static System.IO.StreamWriter file;

        internal static void Log(string Message)
        {
            if (!File.Exists(Program.Pathh + "\\Log.log"))
            {
                File.Create(Program.Pathh + "\\Log.log").Dispose();
            }
            try
            {
                File.SetAttributes(Program.Pathh + "\\Log.log", FileAttributes.Normal);
                file = new System.IO.StreamWriter(Program.Pathh + "\\Log.log", true);
                file.WriteLine(DateTime.Now + " [Console] " + Message);
                file.Close();
                Console.WriteLine(DateTime.Now + " [Console] " + Message);
            }
            catch
            {
                
            }
        }
    }
}
