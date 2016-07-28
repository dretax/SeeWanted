using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace SeeWanted
{
    internal partial class Login : Form
    {
        private Panel childForm;
        private SecureString User = new SecureString();
        private SecureString Pw = new SecureString();

        internal Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Egyik mező sem lehet üres!", "SeeWanted");
                return;
            }
            string Hash = Program.SHA256Code(textBox2.Text);
            if (textBox1.Text.ToLower() == "test" && textBox2.Text == "test")
            {
                childForm = new Panel(this);
                childForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Hibás Adatok!", "SeeWanted");
            }
            textBox1.Text.ToLower().ToCharArray().ToList().ForEach(p => User.AppendChar(p));
            Hash.ToCharArray().ToList().ForEach(p => User.AppendChar(p));
            //todo: Login system
        }
    }
}
