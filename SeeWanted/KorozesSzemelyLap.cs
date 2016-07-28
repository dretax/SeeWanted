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
    internal partial class KorozesSzemelyLap : Form
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
            //todo: upload/update
            MessageBox.Show("Sikeresen kiadtál egy körözést!", "SeeWanted");
            this.Close();
        }
    }
}
