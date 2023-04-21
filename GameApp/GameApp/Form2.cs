using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameApp
{
    public partial class Form2 : Form
    {
        public static string userName = "";
        public static bool userStart = true;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
                MessageBox.Show("Please enter your name");
            else
            {
                this.Hide();
                userName = txtName.Text;
                userStart = radioButton1.Checked;
                Form3 form3 = new Form3();
                form3.ShowDialog();
            }
        }
    }
}
