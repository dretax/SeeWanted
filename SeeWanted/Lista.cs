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
            var selectedv = Vehicles.SelectedItem;
            var selectedp = Persons.SelectedItem;
            Vehicles.Items.Clear();
            Persons.Items.Clear();
            string vehkeys = Communicator.SendMessage(((int)Communicator.Codes.GetAllVehKeys) + "=");
            string[] vehspit = vehkeys.Split(Convert.ToChar("="));
            string[] vkeys = vehspit[1].Split(Convert.ToChar("$"));
            WC.Clear();
            WCC.Clear();
            foreach (var yy in vkeys)
            {

                string veh = Communicator.SendMessage(((int)Communicator.Codes.GetVehicleData) + "=" + yy);
                string[] veh2 = veh.Split(Convert.ToChar("="));
                if (veh2[1].Contains("null"))
                {
                    continue;
                }
                try
                {
                    childForm2 = new KorozesJarmu();
                    string[] vehicles2 = veh2[1].Split(Convert.ToChar("<"));
                    int iss =
                        this.Vehicles.Items.Add(vehicles2[0] + " (" + vehicles2[1] + " | " + vehicles2[2] + " )");
                    WC.Add(iss + vehicles2[0] + " (" + vehicles2[1] + " | " + vehicles2[2] + " )", childForm2);
                    WCC.Add(iss + vehicles2[0] + " (" + vehicles2[1] + " | " + vehicles2[2] + " )", int.Parse(yy));
                    childForm2.TextB1.Text = vehicles2[0];
                    childForm2.TextB2.Text = vehicles2[1];
                    childForm2.TextB3.Text = vehicles2[2];
                    childForm2.TextB4.Text = vehicles2[3];
                    childForm2.TextB5.Text = vehicles2[4];
                    childForm2.TextB6.Text = vehicles2[5];
                    childForm2.TextB7.Text = vehicles2[6];
                    childForm2.TextB8.Text = vehicles2[7];
                }
                catch
                {

                }

            }
            VK = WC.Keys.Count;

            string perkeys = Communicator.SendMessage(((int)Communicator.Codes.GetAllPersonKeys) + "=");
            string[] perspit = perkeys.Split(Convert.ToChar("="));
            string[] pkeys = perspit[1].Split(Convert.ToChar("$"));

            WP.Clear();
            WPC.Clear();

            foreach (var yy in pkeys)
            {
                string s = Communicator.SendMessage(((int) Communicator.Codes.GetPersonData) + "=" + yy);
                string[] split1 = s.Split(Convert.ToChar("="));
                try
                {
                    childForm = new KorozottSzemely();
                    string[] persons2 = split1[1].Split(Convert.ToChar("<"));
                    int iss = this.Persons.Items.Add(persons2[0] + " ( " + persons2[1] + " )");
                    WP.Add(iss + persons2[0] + " ( " + persons2[1] + " )", childForm);
                    WPC.Add(iss + persons2[0] + " ( " + persons2[1] + " )", int.Parse(yy));
                    childForm.TextB1.Text = persons2[5];
                    childForm.TextB2.Text = persons2[4];
                    childForm.TextB3.Text = persons2[3];
                    childForm.TextB4.Text = persons2[2];
                    childForm.TextB5.Text = persons2[0];
                    childForm.TextB6.Text = persons2[1];
                    childForm.TextB7.Text = persons2[6];
                }
                catch
                {

                }

            }
            PK = WP.Keys.Count;
            Vehicles.SelectedItems.Clear();
            Persons.SelectedItems.Clear();
            Vehicles.SelectedItem = selectedv;
            Persons.SelectedItem = selectedp;
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Panel.childForm = null;
            Panel.childForm2 = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || textBox1.Text.Length < 3)
            {
                MessageBox.Show("Add meg a törlés okát!", "SeeWanted");
                return;
            }
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
                    Panel.PanelForm.Close();
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
                                                 Communicator.Separator + Login.User + Communicator.Separator + textBox1.Text);
                    if ((Communicator.Codes) int.Parse(msg.Split(Convert.ToChar("="))[0]) ==
                        Communicator.Codes.DeleteFail)
                    {
                        MessageBox.Show("Sikertelen törlés! (Lehet már előtted törölték)", "SeeWanted");
                        return;
                    }
                    textBox1.Text = "";
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
            if (string.IsNullOrEmpty(textBox2.Text) || textBox2.Text.Length < 3)
            {
                MessageBox.Show("Add meg a törlés okát!", "SeeWanted");
                return;
            }
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
                    Panel.PanelForm.Close();
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
                    string msg = Communicator.SendMessage((int)Communicator.Codes.DeleteVehicle + "=" + num + Communicator.Separator + Login.User + Communicator.Separator + textBox2.Text);
                    if ((Communicator.Codes)int.Parse(msg.Split(Convert.ToChar("="))[0]) == Communicator.Codes.DeleteFail)
                    {
                        MessageBox.Show("Sikertelen törlés! (Lehet már előtted törölték)", "SeeWanted");
                        return;
                    }
                    textBox2.Text = "";
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
