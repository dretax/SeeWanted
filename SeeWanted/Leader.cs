﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;

namespace SeeWanted
{
    internal sealed partial class Leader : MetroFramework.Forms.MetroForm
    {
        private bool GiveLeader = false;

        internal Leader()
        {
            Closing += Form1_FormClosing;
            InitializeComponent();
            button2.UseCustomForeColor = true;
            button2.ForeColor = Color.Red;
            RunUpdate();
        }

        private void RunUpdate()
        {
            listView1.Items.Clear();
            var getcurrentfaction = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + Login.User);
            string[] factionsplit = getcurrentfaction.Split(Convert.ToChar("="));
            string[] fsplit = factionsplit[1].Split(Convert.ToChar(Communicator.Separator));
            string faction = fsplit[2];
            if (faction != "adm")
            {
                textBox3.Hide();
                label4.Hide();
            }
            if (!Program.Leader)
            {
                checkBox1.Hide();
            }
            listView1.SmallImageList = Program.ImgList;
            listView1.LargeImageList = Program.ImgList;
            string data = Communicator.SendMessage((int)Communicator.Codes.GetAllUsers + "=");
            string[] split = data.Split(Convert.ToChar("="));
            string[] split2 = split[1].Split(Convert.ToChar(Communicator.Separator));
            foreach (var x in split2)
            {
                string[] split3 = x.Split(Convert.ToChar(">"));
                listView1.Items.Add(split3[0], split3[1]);
            }
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Panel.childForm4 = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MetroMessageBox.Show(this, "Egyik mező sem lehet üres!", "SeeWanted");
                return;
            }

            if (!Program.CheckForChars(textBox1.Text) || !Program.CheckForChars(textBox2.Text))
            {
                MetroMessageBox.Show(this, "Ne használj ~<=$ karaktereket a jelentésben!", "SeeWanted");
                return;
            }

            var getcurrentfaction = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + Login.User);
            string[] factionsplit = getcurrentfaction.Split(Convert.ToChar("="));

            if (factionsplit[1].Contains("null"))
            {
                MetroMessageBox.Show(this, "A felhasználód nem létezik!", "SeeWanted");
                return;
            }

            string[] fsplit = factionsplit[1].Split(Convert.ToChar(Communicator.Separator));
            string faction = fsplit[2];
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                if (faction != "adm" && textBox3.Text == "adm")
                {
                    MetroMessageBox.Show(this, "Adminisztrátort csak Adminisztrátor regisztrálhat!", "SeeWanted");
                    return;
                }
                if (textBox3.Text != "nav" && textBox3.Text != "nni" && textBox3.Text != "pd" && textBox3.Text != "adm")
                {
                    MetroMessageBox.Show(this, "Hibás név! nav/nni/pd/adm", "SeeWanted");
                    return;
                }
                faction = textBox3.Text.ToLower();
            }

            string data = Communicator.SendMessage((int)Communicator.Codes.RegisterUser
                + "=" + textBox1.Text.ToLower() + Communicator.Separator 
                + Program.SHA256Code(textBox2.Text) + Communicator.Separator + faction);
            string[] split = data.Split(Convert.ToChar("="));
            Communicator.Codes code = (Communicator.Codes) int.Parse(split[0]);
            if (code == Communicator.Codes.InvalidRegistration)
            {
                MetroMessageBox.Show(this, "Felhasználó már létezik!", "SeeWanted");
                return;
            }
            if (code == Communicator.Codes.OkRegistration)
            {
                MetroMessageBox.Show(this, "Felhasználó regisztrálva!", "SeeWanted");
            }
            if (GiveLeader)
            {
                Communicator.SendMessage((int)Communicator.Codes.GiveLeader + "=" + Login.User + Communicator.Separator + faction);
                checkBox1.Checked = false;
                GiveLeader = false;
            }
            RunUpdate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var getuserdata = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + Login.User);
                string[] getuserdataspl = getuserdata.Split(Convert.ToChar("="));

                if (getuserdataspl[1].Contains("null"))
                {
                    MetroMessageBox.Show(this, "A felhasználód nem létezik!", "SeeWanted");
                    return;
                }

                List<int> indexes = new List<int>();
                List<string> names = new List<string>();
                for (int i = 0; i < listView1.SelectedItems.Count ; i++)
                {
                    var x = listView1.SelectedItems[i];
                    DialogResult dialogResult = MetroMessageBox.Show(this, "Választott felhasználó: " + x.Text, "Biztosan törölni akarod?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        continue;
                    }

                    var getcurrentfaction = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + x.Text);
                    string[] factionsplit = getcurrentfaction.Split(Convert.ToChar("="));
                    string[] fsplit = factionsplit[1].Split(Convert.ToChar(Communicator.Separator));
                    string faction = fsplit[2];

                    var isleader = Communicator.SendMessage((int)Communicator.Codes.IsLeader + "=" + x.Text);
                    string[] isleadersp = isleader.Split(Convert.ToChar("="));
                    int code = -1;
                    int.TryParse(isleadersp[0], out code);
                    if (code == -1)
                    {
                        return;
                    }
                    if (faction == "adm")
                    {
                        continue;
                    }
                    if (faction == "adm" && Login.Faction == "adm")
                    {
                        continue;
                    }
                    if (Login.Faction == "adm" && faction != "adm")
                    {
                        indexes.Add(x.Index);
                        names.Add(x.Text);
                        continue;
                    }
                    if ((Communicator.Codes)code == Communicator.Codes.IsLeader && Login.Faction != "adm")
                    {
                        continue;
                    }
                    if ((Communicator.Codes) code == Communicator.Codes.NotLeader)
                    {
                        if (faction == Login.Faction)
                        {
                            indexes.Add(x.Index);
                            names.Add(x.Text);
                        }
                    }
                }
                foreach (var x in indexes)
                {
                    listView1.Items.RemoveAt(x);
                }
                foreach (var x in names)
                {
                    Communicator.SendMessage((int) Communicator.Codes.DeleteUser + "=" + x + Communicator.Separator + Login.Faction);
                }
                MetroMessageBox.Show(this,
                    indexes.Count > 0
                        ? "A kiválasztott felhasználók melyekre jogusultsággal rendelkeztél törölve lettek"
                        : "Törlés megszakítva, vagy nem rendelkeztél elég jogusultsággal a törléshez.", "SeeWanted");
                RunUpdate();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GiveLeader = checkBox1.Checked;
        }
    }
}
