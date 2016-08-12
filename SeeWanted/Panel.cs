using System;
using System.Timers;
using MetroFramework;
using MetroFramework.Controls;
using Timer = System.Timers.Timer;

namespace SeeWanted
{
    internal sealed partial class Panel : MetroFramework.Forms.MetroForm
    {
        internal static KorozesSzemelyLap childForm;
        internal static Lista childForm2;
        internal static KorozesJarmuLap childForm3;
        internal static Leader childForm4;
        internal static Nyilvantartas childForm5;
        internal static Felhasznalok childForm6;
        internal static Panel PanelForm = null;
        internal static bool Notification = true;
        internal static Timer _timer;
        private static Timer _timer2;
        internal static int Each = 0;
        internal static int Add = 0;
        internal static bool Updating = false;
        private Login _inst;

        internal Panel(Login instance)
        {
            _inst = instance;
            Closing += Form1_FormClosing;
            InitializeComponent();
            PanelForm = this;
            metroProgressSpinner1.Maximum = 100;
            if (!Program.Leader)
            {
                button4.Text = "Rendvédelmi Szervek Tagjai";
            }
            _timer = new Timer((double) (this.numericUpDown1.Value * 1000));
            _timer.Elapsed += new ElapsedEventHandler(CheckList);
            _timer.Start();
        }

        internal MetroProgressSpinner MetroProgressBarS
        {
            get { return metroProgressSpinner1; }
        }

        private void CheckList(object sender, ElapsedEventArgs e)
        {
            if (Updating)
            {
                return;
            }
            Updating = true;
            _timer.Stop();
            _timer.Dispose();
            int i = 0;
            metroProgressSpinner1.Value = 0;
            if (Nyilvantartas.NyilvantartasInst != null)
            {
                i += Nyilvantartas.NyilvantartasInst.GetNum();
            }
            if (Notification && Lista.ListaInstance != null)
            {
                i += Lista.ListaInstance.GetKeyNum();
            }
            if (i > 0)
            {
                Each = i;
                Add = 100/Each;
            }
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
            if (Nyilvantartas.NyilvantartasInst != null)
            {
                Nyilvantartas.NyilvantartasInst.RunUpdate();
            }
            MetroProgressBarS.Value = 100;
            _timer2 = new Timer(1500);
            _timer2.Elapsed += new ElapsedEventHandler(CheckList2);
            _timer2.Start();
            _timer = new Timer((double)(this.numericUpDown1.Value * 1000));
            _timer.Elapsed += new ElapsedEventHandler(CheckList);
            _timer.Start();
            Updating = false;
        }

        private void CheckList2(object sender, ElapsedEventArgs e)
        {
            _timer2.Stop();
            _timer2.Dispose();
            MetroProgressBarS.Value = 0;
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
            if (Updating)
            {
                MetroMessageBox.Show(this, "Éppen frissül a lista, próbáld meg ez után!", "Várj egy picit!");
                return;
            }
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
            else
            {
                if (childForm6 == null)
                {
                    childForm6 = new Felhasznalok();
                    childForm6.Show();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Notification = checkBox1.Checked;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                MetroMessageBox.Show(this, "Éppen frissül a lista, próbáld meg eu után!", "Várj egy picit!");
                return;
            }
            if (childForm5 == null)
            {
                childForm5 = new Nyilvantartas();
                childForm5.Show();
            }
        }
    }
}
