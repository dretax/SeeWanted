using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace SeeWanted
{
    internal partial class KorozesJarmuTorolt : Form
    {
        internal KorozesJarmuTorolt()
        {
            Closing += Form1_FormClosing;
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        internal TextBox TextB1
        {
            get
            {
                return textBox1;
            }
        }

        internal TextBox TextB2
        {
            get
            {
                return textBox2;
            }
        }

        internal TextBox TextB3
        {
            get
            {
                return textBox3;
            }
        }

        internal TextBox TextB4
        {
            get
            {
                return textBox4;
            }
        }

        internal TextBox TextB5
        {
            get
            {
                return textBox5;
            }
        }

        internal TextBox TextB6
        {
            get
            {
                return textBox6;
            }
        }

        internal TextBox TextB7
        {
            get
            {
                return textBox7;
            }
        }

        internal TextBox TextB8
        {
            get
            {
                return textBox8;
            }
        }

        internal TextBox TextB9
        {
            get
            {
                return textBox9;
            }
        }

        private void KorozesJarmu_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
    }
}