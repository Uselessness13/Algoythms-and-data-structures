using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Шашечки
{
    public partial class ChooseURMight : Form
    {
        public ChooseURMight()
        {
            InitializeComponent();
        }

        private void starter_Click(object sender, EventArgs e)
        {
            String type = mec.Checked | plblack.Checked | plwhite.Checked ? "mec" : mem.Checked ? "mem" : "";
            string col = plblack.Checked ? "white" : plblack.Checked ? "black" : "";
            var game = new Form1(type, col);
            game.Show();
        }

        private void mec_CheckedChanged(object sender, EventArgs e)
        {
            plwhite.Enabled = true;
            plblack.Enabled = true;
        }

        private void mem_CheckedChanged(object sender, EventArgs e)
        {
            plwhite.Enabled = false;
            plblack.Enabled = false;
        }
    }
}