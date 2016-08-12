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
    internal partial class NyilvantartasLapArch : MetroFramework.Forms.MetroForm
    {
        private string _name;
        private string _id;
        private string _reason;

        internal NyilvantartasLapArch(string name, string d)
        {
            Closing += Form1_FormClosing;
            InitializeComponent();
            var spl = d.Split(Convert.ToChar("$"));
            _name = name;
            _id = spl[0];
            RunUpdate();
        }

        internal void RunUpdate()
        {
            textBox1.Text = _name;
            textBox2.Text = _id;
            textBox3.Clear();
            var reason = Communicator.SendMessage((int)Communicator.Codes.GetBookedAPDData + "=" +
                _name.ToLower());
            var dspl = reason.Split(Convert.ToChar("="))[1];
            var spl = dspl.Split(Convert.ToChar("$"));
            _reason = spl[1];

            string[] split = _reason.Split(Convert.ToChar(";"));
            foreach (var x in split)
            {
                if (string.IsNullOrEmpty(x))
                {
                    continue;
                }
                textBox3.AppendText(x + "\r\n");
            }
            textBox4.Text = spl[2] + " - " + spl[3] + " - " + spl[4];
            textBox5.Text = spl[5];
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            NyilvantartasArch.childForm = null;
        }
    }
}
