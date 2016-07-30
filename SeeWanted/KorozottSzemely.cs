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
    internal partial class KorozottSzemely : Form
    {
        public KorozottSzemely()
        {
            Closing += Form1_FormClosing;
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public TextBox TextB1
        {
            get
            {
                return textBox1;
            }
        }

        public TextBox TextB2
        {
            get
            {
                return textBox2;
            }
        }

        public TextBox TextB3
        {
            get
            {
                return textBox3;
            }
        }

        public TextBox TextB4
        {
            get
            {
                return textBox4;
            }
        }

        public TextBox TextB5
        {
            get
            {
                return textBox5;
            }
        }

        public TextBox TextB6
        {
            get
            {
                return textBox6;
            }
        }

        public TextBox TextB7
        {
            get
            {
                return textBox7;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
