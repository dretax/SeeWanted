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
    internal sealed partial class ListaTorolt : MetroFramework.Forms.MetroForm
    {
        internal static Dictionary<string, KorozottSzemelyTorolt> WP;
        internal static Dictionary<string, KorozesJarmuTorolt> WC;
        internal static Dictionary<string, int> WPC;
        internal static Dictionary<string, int> WCC;
        private KorozottSzemelyTorolt childForm;
        private KorozesJarmuTorolt childForm2;
        internal static ListaTorolt ListaInstance;
        internal static bool IsUpdating = false;
        internal static int VK = 0;
        internal static int PK = 0;

        internal ListaTorolt()
        {
            Closing += Form1_FormClosing;
            WP = new Dictionary<string, KorozottSzemelyTorolt>();
            WC = new Dictionary<string, KorozesJarmuTorolt>();
            WPC = new Dictionary<string, int>();
            WCC = new Dictionary<string, int>();
            InitializeComponent();
            ListaInstance = this;
            RunUpdate();
        }

        internal void RunUpdate()
        {
            var selectedv = Vehicles.SelectedItem;
            var selectedp = Persons.SelectedItem;
            Vehicles.Items.Clear();
            Persons.Items.Clear();
            //string s2 = Communicator.SendMessage(((int)Communicator.Codes.GetAllArchivedV) + "=");
            string keys = Communicator.SendMessage((int) Communicator.Codes.GetAllArchivedVKeys + "=");
            string[] split2 = keys.Split(Convert.ToChar("="))[1].Split(Convert.ToChar("$"));
            //string[] split2 = s2.Split(Convert.ToChar("="));
            //string[] vehicles = split2[1].Split(Convert.ToChar(Communicator.Separator));
            WC.Clear();
            WCC.Clear();
            foreach (var x in split2)
            {
                if (string.IsNullOrEmpty(x))
                {
                    continue;
                }
                string keydata = Communicator.SendMessage((int)Communicator.Codes.GetArchivedVKey + "=" + x);
                string[] vehicles2 = keydata.Split(Convert.ToChar("="))[1].Split(Convert.ToChar("<"));
                //MessageBox.Show(string.Join(" ", vehicles2));
                //var y = x.Split(Convert.ToChar("$"));
                childForm2 = new KorozesJarmuTorolt();
                childForm2.TextB1.Text = vehicles2[0];
                childForm2.TextB2.Text = vehicles2[1];
                childForm2.TextB3.Text = vehicles2[2];
                childForm2.TextB4.Text = vehicles2[3];
                childForm2.TextB5.Text = vehicles2[4];
                childForm2.TextB6.Text = vehicles2[5];
                childForm2.TextB7.Text = vehicles2[6];
                childForm2.TextB8.Text = vehicles2[7];
                childForm2.TextB9.Text = vehicles2[8];
                childForm2.TextB10.Text = vehicles2.Length == 10 ? vehicles2[9] : "Nincs";
                int iss = this.Vehicles.Items.Add(vehicles2[0] + " (" + vehicles2[1] + " | " + vehicles2[2] + " )");
                WC.Add(iss + vehicles2[0] + " (" + vehicles2[1] + " | " + vehicles2[2] + " )", childForm2);
                WCC.Add(iss + vehicles2[0] + " (" + vehicles2[1] + " | " + vehicles2[2] + " )", int.Parse(x));
            }
            VK = WC.Keys.Count;

            string keys2 = Communicator.SendMessage((int)Communicator.Codes.GetAllArchivedPKeys + "=");
            string[] split1 = keys2.Split(Convert.ToChar("="))[1].Split(Convert.ToChar("$"));
            //string[] split1 = s.Split(Convert.ToChar("="));
            //string[] persons = split1[1].Split(Convert.ToChar(Communicator.Separator));
            WP.Clear();
            WPC.Clear();
            foreach (var x in split1)
            {
                if (string.IsNullOrEmpty(x))
                {
                    continue;
                }
                childForm = new KorozottSzemelyTorolt();
                //var y = x.Split(Convert.ToChar("$"));
                string keydata = Communicator.SendMessage((int)Communicator.Codes.GetArchivedPKey + "=" + x);
                string[] persons2 = keydata.Split(Convert.ToChar("="))[1].Split(Convert.ToChar("<"));
                childForm.TextB1.Text = persons2[5];
                childForm.TextB2.Text = persons2[4];
                childForm.TextB3.Text = persons2[3];
                childForm.TextB4.Text = persons2[2];
                childForm.TextB5.Text = persons2[0];
                childForm.TextB6.Text = persons2[1];
                childForm.TextB7.Text = persons2[6];
                childForm.TextB8.Text = persons2[7];
                childForm.TextB9.Text = persons2.Length == 9 ? persons2[8] : "Nincs";
                int iss = this.Persons.Items.Add(persons2[0] + " (" + persons2[1] + " )");
                WP.Add(iss + persons2[0] + " (" + persons2[1] + " )", childForm);
                WPC.Add(iss + persons2[0] + " (" + persons2[1] + " )", int.Parse(x));
            }
            PK = WP.Keys.Count;
            Vehicles.SelectedItems.Clear();
            Persons.SelectedItems.Clear();
            Vehicles.SelectedItem = selectedv;
            Persons.SelectedItem = selectedp;
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Lista.childForm3 = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Vehicles.SelectedIndex >= 0)
            {
                KorozesJarmuTorolt kj = WC[Vehicles.SelectedIndex + Vehicles.GetItemText(Vehicles.Items[Vehicles.SelectedIndex])];
                kj.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Persons.SelectedIndex >= 0)
            {
                KorozottSzemelyTorolt kj = WP[Persons.SelectedIndex + Persons.GetItemText(Persons.Items[Persons.SelectedIndex])];
                kj.Show();
            }
        }
    }
}
