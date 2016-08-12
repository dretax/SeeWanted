using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeeWanted
{
    internal partial class Nyilvantartas : MetroFramework.Forms.MetroForm
    {
        internal static Nyilvantartas NyilvantartasInst;
        internal static NyilvantartasLap childForm;
        internal static NyilvantartasArch childForm2;
        internal Dictionary<string, string> NameData = new Dictionary<string, string>(); 

        internal Nyilvantartas()
        {
            Closing += Form1_FormClosing;
            InitializeComponent();
            button2.ForeColor = Color.Red;
            button4.ForeColor = Color.Green;
            NyilvantartasInst = this;
            RunUpdate();
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Panel.childForm5 = null;
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
            string list = Communicator.SendMessage((int)Communicator.Codes.GetBookedPersons + "=" + Login.User);
            string[] split = list.Split(Convert.ToChar("="));
            var ndata = split[1].Split(Convert.ToChar("<"));
            foreach (var x in ndata)
            {
                if (string.IsNullOrEmpty(x))
                {
                    continue;
                }
                var nsplit = x.Split(Convert.ToChar("$"));
                string userdata = Communicator.SendMessage((int)Communicator.Codes.GetBookedPD + "=" +
                nsplit[0].ToLower());
                userdata = userdata.Split(Convert.ToChar("="))[1];
                Szemelyek.Items.Add(nsplit[0] + " (Azonosító: " + userdata + ")");
                NameData[(nsplit[0] + " (Azonosító: " + userdata + ")").ToLower()] = nsplit[0];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Program.CheckForChars(textBox4.Text))
            {
                MessageBox.Show("Ne használj ~<=$ karaktereket a jelentésben!", "SeeWanted");
                return;
            }

            if (string.IsNullOrEmpty(textBox4.Text) || textBox4.Text.Length < 3)
            {
                MessageBox.Show("Add meg a törlés okát!", "SeeWanted");
                return;
            }
            var selected = Szemelyek.SelectedIndex;
            var selected2 = Szemelyek.SelectedItem;
            if (selected >= 0)
            {
                var getuserdata = Communicator.SendMessage((int) Communicator.Codes.GetUserData + "=" + Login.User);
                string[] getuserdataspl = getuserdata.Split(Convert.ToChar("="));

                if (getuserdataspl[1].Contains("null"))
                {
                    MessageBox.Show("A felhasználód nem létezik!", "SeeWanted");
                    Panel.PanelForm.Close();
                    return;
                }
                var oname = Szemelyek.GetItemText(selected2);
                DialogResult dialogResult = MessageBox.Show(oname, "Biztosan törölni akarod a körözést?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var name = NameData[oname.ToLower()];
                    string deluser = Communicator.SendMessage((int) Communicator.Codes.DeleteBookedP + "=" + name
                                                              + Communicator.Separator + Login.User +
                                                              Communicator.Separator + Login.Faction + Communicator.Separator + textBox4.Text);
                    string[] split = deluser.Split(Convert.ToChar("="));
                    if ((Communicator.Codes) int.Parse(split[0]) == Communicator.Codes.DeleteFail)
                    {
                        MessageBox.Show("Sikertelen törlés!", "SeeWanted");
                        return;
                    }
                    RunUpdate();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Program.CheckForChars(textBox1.Text) || !Program.CheckForChars(textBox2.Text) ||
                !Program.CheckForChars(textBox3.Text))
            {
                MessageBox.Show("Ne használj ~<=$ karaktereket a jelentésben!", "SeeWanted");
                return;
            }

            var getuserdata = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + Login.User);
            string[] getuserdataspl = getuserdata.Split(Convert.ToChar("="));

            if (getuserdataspl[1].Contains("null"))
            {
                MessageBox.Show("A felhasználód nem létezik!", "SeeWanted");
                Panel.PanelForm.Close();
                return;
            }

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text)
                || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Egyik mező sem lehet üres!", "SeeWanted");
                return;
            }

            if (!textBox1.Text.Contains("_") || textBox1.Text.Contains(" "))
            {
                MessageBox.Show("A névben nem lehet szóköz, és alsóvonallal kell elválasztani!", "SeeWanted");
                return;
            }

            string adduser = Communicator.SendMessage((int) Communicator.Codes.AddBookedP + "=" +
                textBox1.Text.ToLower() + Communicator.Separator + textBox2.Text + Communicator.Separator + DateTime.Now + ": " + textBox3.Text + ";");
            string[] split = adduser.Split(Convert.ToChar("="));
            if ((Communicator.Codes) int.Parse(split[0]) == Communicator.Codes.BookedExists)
            {
                MessageBox.Show("Ez a név már létezik!", "SeeWanted");
                return;
            }
            RunUpdate();
            MessageBox.Show("Hozzáadva!", "SeeWanted");
        }

        private void button3_Click(object sender, EventArgs e)
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
                string user = Communicator.SendMessage((int)Communicator.Codes.GetBookedPD + "=" +
                name.ToLower());
                string[] split = user.Split(Convert.ToChar("="));
                if (split[1].Contains("null"))
                {
                    return;
                }

                string userdata = split[1];

                childForm = new NyilvantartasLap(name, userdata);
                childForm.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (childForm2 == null)
            {
                childForm2 = new NyilvantartasArch();
                childForm2.Show();
            }
        }
    }
}
