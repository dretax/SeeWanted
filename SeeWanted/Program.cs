using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SeeWanted
{
    internal static class Program
    {
        internal static bool Leader = false;
        internal static Bitmap pd = SeeWanted.Properties.Resources.pd;
        internal static Bitmap nav = SeeWanted.Properties.Resources.nav;
        internal static Bitmap nni = SeeWanted.Properties.Resources.nni;
        internal static Bitmap adm = SeeWanted.Properties.Resources.adm;
        internal static ImageList ImgList;
        internal const string Version = "1.2";


        [STAThread]
        internal static void Main()
        {
            ImgList = new ImageList();
            ImgList.Images.Add("pd", pd);
            ImgList.Images.Add("nav", nav);
            ImgList.Images.Add("nni", nni);
            ImgList.Images.Add("adm", adm);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        internal static string SHA256Code(string value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return string.Join("", hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }

        internal static bool CheckForChars(string s)
        {
            return !s.Contains("~") && !s.Contains("<") && !s.Contains("=") && !s.Contains("-");
        }
    }
}
