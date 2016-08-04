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
    internal sealed partial class KorozesSzemelyLap : Form
    {
        internal KorozesSzemelyLap()
        {
            Closing += Form1_FormClosing;
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Panel.childForm = null;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var getuserdata = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + Login.User);
            string[] getuserdataspl = getuserdata.Split(Convert.ToChar("="));

            if (getuserdataspl[1].Contains("null"))
            {
                MessageBox.Show("A felhasználód nem létezik!", "SeeWanted");
                return;
            }

            if (textBox1.Text == "Ha ismert...(Példa)")
            {
                MessageBox.Show("Minden mezőt kötelező kitölteni! Ha nem ismert, írj oda ismeretlent!", "SeeWanted");
                return;
            }
            if (textBox2.Text == "Azonosítás miatt(Példa)")
            {
                MessageBox.Show("Minden mezőt kötelező kitölteni! Ha nem ismert, írj oda ismeretlent!", "SeeWanted");
                return;
            }
            if (textBox3.Text == "REM,RUM(Példa)")
            {
                MessageBox.Show("Minden mezőt kötelező kitölteni! Ha nem ismert, írj oda ismeretlent!", "SeeWanted");
                return;
            }
            if (textBox4.Text == "Fehér bőrű, raszta hajjal rendelkező személy piros kendőt visel az arcán (PÉLDA)")
            {
                MessageBox.Show("Minden mezőt kötelező kitölteni! Ha nem ismert, írj oda ismeretlent!", "SeeWanted");
                return;
            }
            if (!Program.CheckForChars(textBox1.Text) || !Program.CheckForChars(textBox2.Text) ||
                !Program.CheckForChars(textBox3.Text) || !Program.CheckForChars(textBox4.Text))
            {
                MessageBox.Show("Ne használj ~<= karaktereket a jelentésben!", "SeeWanted");
                return;
            }
            var dnow = DateTime.Now;
            string s = Communicator.SendMessage(((int)Communicator.Codes.RegisterPerson) + "=" + textBox1.Text + "<" +
                textBox2.Text + "<" +
                textBox3.Text + "<" +
                textBox4.Text + "<" +
                Login.User + " - " + Login.Faction + "<" +
                dnow + "<" +
                dnow.AddDays(7)
                );
            var num = int.Parse(s.Split(Convert.ToChar("="))[0]);
            if ((Communicator.Codes)num == Communicator.Codes.OkRVehicle)
            {
                MessageBox.Show("Sikeresen kiadtál egy körözést!", "SeeWanted");
            }
            this.Close();
        }
    }
}
