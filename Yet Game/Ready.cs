using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yet_Game
{
    public partial class Ready : Form
    {
        public Ready()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            Form_GS.game_name = textBox1.Text.ToString();
            Form form_gs = new Form_GS();
            form_gs.Show();
            this.Hide();
        }

        private void button_About_Click(object sender, EventArgs e)
        {
            Form about = new Form_About();
            about.Show();
        }

        public static bool Form_GS_load = false;
    }
}
