using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeeWanted
{
    internal partial class NyilvantartasArch : Form
    {
        internal static NyilvantartasLapArch childForm;
        internal Dictionary<string, string> NameData = new Dictionary<string, string>();

        internal NyilvantartasArch()
        {
            Closing += Form1_FormClosing;
            InitializeComponent();
            RunUpdate();
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Nyilvantartas.childForm2 = null;
        }

        internal void RunUpdate()
        {
            NameData.Clear();
            Szemelyek.Items.Clear();
            var getuserdata = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + Login.User);
            string[] getuserdataspl = getuserdata.Split(Convert.ToChar("="));

            if (getuserdataspl[1].Contains("null"))
            {
                MessageBox.Show("A felhasználód nem létezik!", "SeeWanted");
                Panel.PanelForm.Close();
                return;
            }
            string list = Communicator.SendMessage((int)Communicator.Codes.GetBookedAPD + "=" + Login.User);
            string[] split = list.Split(Convert.ToChar("="));
            var ndata = split[1].Split(Convert.ToChar("<"));
            foreach (var x in ndata)
            {
                if (string.IsNullOrEmpty(x))
                {
                    continue;
                }
                string userdata = Communicator.SendMessage((int)Communicator.Codes.GetBookedAPDData + "=" +
                x.ToLower());
                userdata = userdata.Split(Convert.ToChar("="))[1];
                var spl = userdata.Split(Convert.ToChar("$"));
                Szemelyek.Items.Add(x + " (Azonosító: " + spl[0] + ")");
                NameData[(x + " (Azonosító: " + spl[0] + ")").ToLower()] = x;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var getuserdata = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + Login.User);
            string[] getuserdataspl = getuserdata.Split(Convert.ToChar("="));

            if (getuserdataspl[1].Contains("null"))
            {
                MessageBox.Show("A felhasználód nem létezik!", "SeeWanted");
                Panel.PanelForm.Close();
                return;
            }
            var selected = Szemelyek.SelectedIndex;
            var selected2 = Szemelyek.SelectedItem;
            if (selected >= 0)
            {
                var oname = Szemelyek.GetItemText(selected2);
                var name = NameData[oname.ToLower()];
                string user = Communicator.SendMessage((int)Communicator.Codes.GetBookedAPDData + "=" +
                name.ToLower());
                string[] split = user.Split(Convert.ToChar("="));
                if (split[1].Contains("null"))
                {
                    return;
                }
                string userdata = split[1];

                childForm = new NyilvantartasLapArch(name, userdata);
                childForm.Show();
            }
        }
    }
}
