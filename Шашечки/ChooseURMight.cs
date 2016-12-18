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
            String type = "";
            if (mem.Checked)
            {
                type = "mem";
            }
            if (mec.Checked)
            {
                type = "mec";
            }
            //this.Close();
            var game = new Form1(type);
            game.Show();
        }
    }
}