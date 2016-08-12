using System;
using System.ComponentModel;
using MetroFramework;

namespace SeeWanted
{
    internal sealed partial class KorozesJarmuLap : MetroFramework.Forms.MetroForm
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

        private void button1_Click(object sender, EventArgs e)
        {
            var getuserdata = Communicator.SendMessage((int)Communicator.Codes.GetUserData + "=" + Login.User);
            string[] getuserdataspl = getuserdata.Split(Convert.ToChar("="));

            if (getuserdataspl[1].Contains("null"))
            {
                MetroMessageBox.Show(this, "A felhasználód nem létezik!", "SeeWanted");
                return;
            }

            if (textBox3.Text == "SEE-00000(Példa)")
            {
                MetroMessageBox.Show(this, "Minden mezőt kötelező kitölteni! Ha nem ismert, írj oda ismeretlent!", "SeeWanted");
                return;
            }
            if (textBox4.Text == "REM,RUM(Példa)")
            {
                MetroMessageBox.Show(this, "Minden mezőt kötelező kitölteni! Ha nem ismert, írj oda ismeretlent!", "SeeWanted");
                return;
            }
            if (!Program.CheckForChars(textBox1.Text) || !Program.CheckForChars(textBox2.Text) ||
                !Program.CheckForChars(textBox3.Text) || !Program.CheckForChars(textBox4.Text) || 
                !Program.CheckForChars(textBox5.Text))
            {
                MetroMessageBox.Show(this, "Ne használj ~<=$ karaktereket a jelentésben!", "SeeWanted");
                return;
            }
            var dnow = DateTime.Now;
            string s = Communicator.SendMessage(((int)Communicator.Codes.RegisterVehicle) + "=" + textBox1.Text + "<" +
                textBox2.Text + "<" +
                textBox3.Text + "<" +
                textBox4.Text + "<" +
                textBox5.Text + "<" +
                Login.User + " - " + Login.Faction + "<" +
                dnow + "<" +
                dnow.AddDays(7)
                );
            var num = int.Parse(s.Split(Convert.ToChar("="))[0]);
            if ((Communicator.Codes)num == Communicator.Codes.OkRVehicle)
            {
                MetroMessageBox.Show(this, "Sikeresen kiadtál egy körözést!", "SeeWanted");
            }
            this.Close();
        }
    }
}
