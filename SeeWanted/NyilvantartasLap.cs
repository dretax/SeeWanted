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
    internal partial class NyilvantartasLap : MetroFramework.Forms.MetroForm
    {
        private string _name;
        private string _id;

        internal NyilvantartasLap(string name, string id)
        {
            Closing += Form1_FormClosing;
            InitializeComponent();
            _name = name;
            _id = id;
            RunUpdate();
        }

        internal void RunUpdate()
        {
            textBox1.Text = _name;
            textBox2.Text = _id;
            textBox3.Clear();
            var _reason = Communicator.SendMessage((int)Communicator.Codes.GetBookedPDData + "=" +
                _name.ToLower());
            _reason = _reason.Split(Convert.ToChar("="))[1];

            string[] split = _reason.Split(Convert.ToChar(";"));
            foreach (var x in split)
            {
                if (string.IsNullOrEmpty(x))
                {
                    continue;
                }
                textBox3.AppendText(x + "\r\n");
            }
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Nyilvantartas.childForm = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Program.CheckForChars(textBox4.Text))
            {
                MessageBox.Show("Ne használj ~<=$ karaktereket a jelentésben!", "SeeWanted");
                return;
            }

            string s = Communicator.SendMessage((int) Communicator.Codes.AddBookedReason + "=" + _name + Communicator.Separator + DateTime.Now + ": " + textBox4.Text);
            if ((Communicator.Codes) int.Parse(s.Split(Convert.ToChar("="))[0]) == Communicator.Codes.FailedToAddBookedR)
            {
                MessageBox.Show("Sikertelen! Valószínűleg az adott nevet törölték!");
                return;
            }
            RunUpdate();
            MessageBox.Show("Sikeresen hozzáadtál egy újabb bűncselekményt!");
            textBox4.Clear();
        }
    }
}
