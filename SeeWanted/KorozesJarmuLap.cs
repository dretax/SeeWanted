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
    internal partial class KorozesJarmuLap : Form
    {
        internal KorozesJarmuLap()
        {
            Closing += Form1_FormClosing;
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Panel.childForm3 = null;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "SEE-00000(Példa)")
            {
                MessageBox.Show("Minden mezőt kötelező kitölteni! Ha nem ismert, írj oda ismeretlent!", "SeeWanted");
                return;
            }
            if (textBox4.Text == "REM,RUM(Példa)")
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
