﻿using System;
using System.ComponentModel;
using MetroFramework.Controls;

namespace SeeWanted
{
    internal partial class KorozesJarmuTorolt : MetroFramework.Forms.MetroForm
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

        internal MetroTextBox TextB1
        {
            get
            {
                return textBox1;
            }
        }

        internal MetroTextBox TextB2
        {
            get
            {
                return textBox2;
            }
        }

        internal MetroTextBox TextB3
        {
            get
            {
                return textBox3;
            }
        }

        internal MetroTextBox TextB4
        {
            get
            {
                return textBox4;
            }
        }

        internal MetroTextBox TextB5
        {
            get
            {
                return textBox5;
            }
        }

        internal MetroTextBox TextB6
        {
            get
            {
                return textBox6;
            }
        }

        internal MetroTextBox TextB7
        {
            get
            {
                return textBox7;
            }
        }

        internal MetroTextBox TextB8
        {
            get
            {
                return textBox8;
            }
        }

        internal MetroTextBox TextB9
        {
            get
            {
                return textBox9;
            }
        }

        internal MetroTextBox TextB10
        {
            get
            {
                return textBox10;
            }
        }
    }
}