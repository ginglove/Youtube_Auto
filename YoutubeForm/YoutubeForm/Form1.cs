using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using YoutubeAtata;
namespace YoutubeForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        UITests1 t = new UITests1();
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtURL.Text))
            {
                MessageBox.Show("Please enter URL!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(txtVideoName.Text))
            {
                MessageBox.Show("Please enter Video Name!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(txtChannelName.Text))
            {
                MessageBox.Show("Please enter Channel Name!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                t.YoutubeTest(txtURL.Text, txtVideoName.Text, txtChannelName.Text);
            }
        }
        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            btnGo.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Please input URL of youtube and name of video and Channel name");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
