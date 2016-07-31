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
    internal sealed partial class Lista : Form
    {
        internal static Dictionary<string, KorozottSzemely> WP;
        internal static Dictionary<string, KorozesJarmu> WC;
        internal static Dictionary<string, int> WPC;
        internal static Dictionary<string, int> WCC;
        private KorozottSzemely childForm;
        private KorozesJarmu childForm2;
        internal static ListaTorolt childForm3;
        internal static Lista ListaInstance;
        internal static bool IsUpdating = false;
        internal static int VK = 0;
        internal static int PK = 0;

        internal Lista()
        {
            Closing += Form1_FormClosing;
            WP = new Dictionary<string, KorozottSzemely>();
            WC = new Dictionary<string, KorozesJarmu>();
            WPC = new Dictionary<string, int>();
            WCC = new Dictionary<string, int>();
            InitializeComponent();
            button1.ForeColor = Color.Red;
            button2.ForeColor = Color.Red;
            button5.ForeColor = Color.Green;
            ListaInstance = this;
            RunUpdate();
        }

        internal void RunUpdate()
        {
            Vehicles.Items.Clear();
            Persons.Items.Clear();
            string s2 = Communicator.SendMessage(((int)Communicator.Codes.GetAllVehicles) + "=");
            string[] split2 = s2.Split(Convert.ToChar("="));
            string[] vehicles = split2[1].Split(Convert.ToChar(Communicator.Separator));
            WC.Clear();
            WCC.Clear();
            foreach (var x in vehicles)
            {
                if (string.IsNullOrEmpty(x))
                {
                    continue;
                }
                var y = x.Split(Convert.ToChar("$"));
                childForm2 = new KorozesJarmu();
                string[] vehicles2 = y[1].Split(Convert.ToChar("<"));
                childForm2.TextB1.Text = vehicles2[0];
                childForm2.TextB2.Text = vehicles2[1];
                childForm2.TextB3.Text = vehicles2[2];
                childForm2.TextB4.Text = vehicles2[3];
                childForm2.TextB5.Text = vehicles2[4];
                childForm2.TextB6.Text = vehicles2[5];
                childForm2.TextB7.Text = vehicles2[6];
                childForm2.TextB8.Text = vehicles2[7];
                int iss = this.Vehicles.Items.Add(vehicles2[0] + " (" + vehicles2[1] + " | " + vehicles2[2] + " )");
                WC.Add(iss + vehicles2[0] + " (" + vehicles2[1] + " | " + vehicles2[2] + " )", childForm2);
                WCC.Add(iss + vehicles2[0] + " (" + vehicles2[1] + " | " + vehicles2[2] + " )", int.Parse(y[0]));
            }
            VK = WC.Keys.Count;

            string s = Communicator.SendMessage(((int)Communicator.Codes.GetAllPersons) + "=");
            string[] split1 = s.Split(Convert.ToChar("="));
            string[] persons = split1[1].Split(Convert.ToChar(Communicator.Separator));
            WP.Clear();
            WPC.Clear();
            foreach (var x in persons)
            {
                if (string.IsNullOrEmpty(x))
                {
                    continue;
                }
                childForm = new KorozottSzemely();
                var y = x.Split(Convert.ToChar("$"));
                string[] persons2 = y[1].Split(Convert.ToChar("<"));
                childForm.TextB1.Text = persons2[5];
                childForm.TextB2.Text = persons2[4];
                childForm.TextB3.Text = persons2[3];
                childForm.TextB4.Text = persons2[2];
                childForm.TextB5.Text = persons2[0];
                childForm.TextB6.Text = persons2[1];
                childForm.TextB7.Text = persons2[6];
                int iss = this.Persons.Items.Add(persons2[0] + " (" + persons2[1] + " )");
                WP.Add(iss + persons2[0] + " (" + persons2[1] + " )", childForm);
                WPC.Add(iss + persons2[0] + " (" + persons2[1] + " )", int.Parse(y[0]));
            }
            PK = WP.Keys.Count;
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Panel.childForm = null;
            Panel.childForm2 = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsUpdating)
            {
                MessageBox.Show("Lista frissítés futott, próbáld meg ez után.", "SeeWanted");
                return;
            }
            var selected = Persons.SelectedIndex;
            if (selected >= 0)
            {
                var getuserdata = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + Login.User);
                string[] getuserdataspl = getuserdata.Split(Convert.ToChar("="));

                if (getuserdataspl[1].Contains("null"))
                {
                    MessageBox.Show("A felhasználód nem létezik!", "SeeWanted");
                    return;
                }

                Persons.SelectedIndex = selected;
                int sindex = Persons.SelectedIndex;
                string key = Persons.GetItemText(Persons.SelectedItem);
                string key2 = sindex + key;
                var num = WPC[key2];
                DialogResult dialogResult = MessageBox.Show(key, "Biztosan törölni akarod a körözést?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string msg =
                        Communicator.SendMessage((int) Communicator.Codes.DeletePerson + "=" + num +
                                                 Communicator.Separator + Login.User);
                    if ((Communicator.Codes) int.Parse(msg.Split(Convert.ToChar("="))[0]) ==
                        Communicator.Codes.DeleteFail)
                    {
                        MessageBox.Show("Sikertelen törlés! (Lehet már előtted törölték)", "SeeWanted");
                        return;
                    }
                    try
                    {
                        Persons.Items.RemoveAt(selected);
                    }
                    catch
                    {
                        
                    }
                    RunUpdate();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsUpdating)
            {
                MessageBox.Show("Lista frissítés futott, próbáld meg ez után.", "SeeWanted");
                return;
            }
            var selected = Vehicles.SelectedIndex;
            if (selected >= 0)
            {
                var getuserdata = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + Login.User);
                string[] getuserdataspl = getuserdata.Split(Convert.ToChar("="));

                if (getuserdataspl[1].Contains("null"))
                {
                    MessageBox.Show("A felhasználód nem létezik!", "SeeWanted");
                    return;
                }

                Vehicles.SelectedIndex = selected;
                int sindex = Vehicles.SelectedIndex;
                string key = Vehicles.GetItemText(Vehicles.SelectedItem);
                string key2 = sindex + key;
                var num = WCC[key2];
                DialogResult dialogResult = MessageBox.Show(key, "Biztosan törölni akarod a körözést?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string msg = Communicator.SendMessage((int)Communicator.Codes.DeleteVehicle + "=" + num + Communicator.Separator + Login.User);
                    if ((Communicator.Codes)int.Parse(msg.Split(Convert.ToChar("="))[0]) == Communicator.Codes.DeleteFail)
                    {
                        MessageBox.Show("Sikertelen törlés! (Lehet már előtted törölték)", "SeeWanted");
                        return;
                    }
                    try
                    {
                        Vehicles.Items.RemoveAt(Vehicles.SelectedIndex);
                    }
                    catch
                    {
                        
                    }
                    RunUpdate();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Vehicles.SelectedIndex >= 0)
            {
                KorozesJarmu kj = WC[Vehicles.SelectedIndex + Vehicles.GetItemText(Vehicles.Items[Vehicles.SelectedIndex])];
                kj.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Persons.SelectedIndex >= 0)
            {
                KorozottSzemely kj = WP[Persons.SelectedIndex + Persons.GetItemText(Persons.Items[Persons.SelectedIndex])];
                kj.Show();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (childForm3 == null)
            {
                childForm3 = new ListaTorolt();
                childForm3.Show();
            }
        }
    }
}
