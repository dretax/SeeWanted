using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace SeeWanted
{
    internal sealed partial class Login : Form
    {
        private Panel childForm;
        internal static string User = "";
        internal static string Pw = "";
        internal static string Faction = "";

        internal Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Egyik mező sem lehet üres!", "SeeWanted");
                return;
            }
            string Hash = Program.SHA256Code(textBox2.Text);
            string data =
                Communicator.SendMessage((int) Communicator.Codes.Login + "=" + textBox1.Text.ToLower() +
                                         Communicator.Separator + Hash);
            string[] split = data.Split(Convert.ToChar("="));
            int intp = -1;
            int.TryParse(split[0], out intp);
            if (intp == -1)
            {
                return;
            }
            //MessageBox.Show(intp.ToString());
            Communicator.Codes code = (Communicator.Codes) intp;
            if (code == Communicator.Codes.IsLeader)
            {
                Program.Leader = true;
                childForm = new Panel(this);
                childForm.Show();
                Hide();
                User = textBox1.Text.ToLower();
                Pw = textBox2.Text;
                var getcurrentfaction = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + User);
                string[] factionsplit = getcurrentfaction.Split(Convert.ToChar("="));
                string[] fsplit = factionsplit[1].Split(Convert.ToChar(Communicator.Separator));
                string faction = fsplit[2];
                Faction = faction;
                string version = Communicator.SendMessage((int)Communicator.Codes.Version + "=Requesting");
                if (version.Split(Convert.ToChar("="))[1] != Program.Version)
                {
                    MessageBox.Show("Régebbi verziót használsz mint a szerver! A gomb lenyomása után letöltheted.", "SeeWanted");
                    System.Diagnostics.Process.Start("https://www.dropbox.com/s/8qbw54glkqx31f7/SeeWanted.exe?dl=0");
                    Close();
                }
            }
            else if (code == Communicator.Codes.NotLeader)
            {
                Program.Leader = false;
                childForm = new Panel(this);
                childForm.Show();
                Hide();
                User = textBox1.Text.ToLower();
                Pw = textBox2.Text;
                var getcurrentfaction = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + User);
                string[] factionsplit = getcurrentfaction.Split(Convert.ToChar("="));
                string[] fsplit = factionsplit[1].Split(Convert.ToChar(Communicator.Separator));
                string faction = fsplit[2];
                Faction = faction;
                string version = Communicator.SendMessage((int)Communicator.Codes.Version + "=Requesting");
                if (version.Split(Convert.ToChar("="))[1] != Program.Version)
                {
                    MessageBox.Show("Régebbi verziót használsz mint a szerver! A gomb lenyomása után letöltheted.", "SeeWanted");
                    System.Diagnostics.Process.Start("https://www.dropbox.com/s/8qbw54glkqx31f7/SeeWanted.exe?dl=0");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Hibás Adatok!", "SeeWanted");
            }
        }
    }
}
