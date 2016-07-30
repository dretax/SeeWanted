using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace SeeWanted
{
    internal sealed partial class Panel : Form
    {
        internal static KorozesSzemelyLap childForm;
        internal static Lista childForm2;
        internal static KorozesJarmuLap childForm3;
        internal static Leader childForm4;
        internal static bool Notification = true;
        internal static Timer _timer;
        private Login _inst;

        internal Panel(Login instance)
        {
            _inst = instance;
            Closing += Form1_FormClosing;
            InitializeComponent();
            if (!Program.Leader)
            {
                button4.Hide();
            }
            _timer = new Timer((double) (this.numericUpDown1.Value * 1000));
            _timer.Elapsed += new ElapsedEventHandler(CheckList);
            _timer.Start();
        }

        private void CheckList(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
            if (Notification && Lista.ListaInstance != null)
            {
                int Voldnum = Lista.VK;
                int Poldnum = Lista.PK;
                Lista.IsUpdating = true;
                Lista.ListaInstance.RunUpdate();
                if (Voldnum < Lista.VK || Poldnum < Lista.PK)
                {
                    System.Media.SoundPlayer player =
                        new System.Media.SoundPlayer(SeeWanted.Properties.Resources.korozes);
                    player.Play();
                }
                Lista.IsUpdating = false;
            }
            _timer = new Timer((double)(this.numericUpDown1.Value * 1000));
            _timer.Elapsed += new ElapsedEventHandler(CheckList);
            _timer.Start();
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (Program.Leader)
            {
                if (childForm4 == null)
                {
                    childForm4 = new Leader();
                    childForm4.Show();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Notification = checkBox1.Checked;
        }
    }
}
