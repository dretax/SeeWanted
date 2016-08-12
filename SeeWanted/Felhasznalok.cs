using System;
using System.ComponentModel;

namespace SeeWanted
{
    internal partial class Felhasznalok : MetroFramework.Forms.MetroForm
    {
        internal Felhasznalok()
        {
            Closing += Form1_FormClosing;
            InitializeComponent();
            listView1.SmallImageList = Program.ImgList;
            listView1.LargeImageList = Program.ImgList;
            string data = Communicator.SendMessage((int)Communicator.Codes.GetAllUsers + "=");
            string[] split = data.Split(Convert.ToChar("="));
            string[] split2 = split[1].Split(Convert.ToChar(Communicator.Separator));
            foreach (var x in split2)
            {
                string[] split3 = x.Split(Convert.ToChar(">"));
                listView1.Items.Add(split3[0], split3[1]);
            }
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Panel.childForm6 = null;
        }
    }
}
