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
    internal partial class Panel : Form
    {
        internal static KorozesSzemelyLap childForm;
        internal static Lista childForm2;
        internal static KorozesJarmuLap childForm3;
        private Login _inst;

        internal Panel(Login instance)
        {
            _inst = instance;
            Closing += Form1_FormClosing;
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            if (_inst != null)
            {
                _inst.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (childForm2 == null)
            {
                childForm2 = new Lista();
                childForm2.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (childForm == null)
            {
                childForm = new KorozesSzemelyLap();
                childForm.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (childForm3 == null)
            {
                childForm3 = new KorozesJarmuLap();
                childForm3.Show();
            }
        }
    }
}
