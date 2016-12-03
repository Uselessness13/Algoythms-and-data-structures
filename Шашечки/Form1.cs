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
    public partial class Form1 : Form
    {
        PictureBox[,] board = new PictureBox[8, 8];
        private int numberOfStep = 0;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox boardpart = new PictureBox();
                    boardpart.Width = 50;
                    boardpart.Height = 50;
                    boardpart.Name = i.ToString() + "_" + j.ToString();
                    boardpart.Location = new Point((10 + j * 50), (10 + i * 50));
                    boardpart.Visible = true;
                    boardpart.BorderStyle = BorderStyle.FixedSingle;
                    if (i%2 == 0)
                    {
                        if (j%2 == 0)
                        {
                            boardpart.BackgroundImage = Properties.Resources.white_bg;
                        }
                        else
                        {
                            boardpart.BackgroundImage = Properties.Resources.black_bg;
                            if (i <= 2)
                            {
                                boardpart.Image = blacksh.Image;
                            }
                            if (i > 4)
                            {
                                boardpart.Image = whitesh.Image;
                            }
                        }
                    }
                    else
                    {
                        if (j % 2 != 0)
                        {
                            boardpart.BackgroundImage = Properties.Resources.white_bg;
                        }
                        else
                        {
                            boardpart.BackgroundImage = Properties.Resources.black_bg;
                            if (i <= 2)
                            {
                                boardpart.Image = blacksh.Image;
                            }
                            if (i > 4)
                            {
                                boardpart.Image = whitesh.Image;
                            }
                        }
                    }

                    boardpart.Click += new System.EventHandler(this.boardpart_Click);

                    board[i, j] = boardpart;

                    Controls.Add(board[i, j]);

                }
            }
        }

        private void boardpart_Click(object sender, EventArgs e)
        {
            numberOfStep++;

            int y = ((Cursor.Position.X + 20 - this.DesktopLocation.X ) / 50) - 1;
            int x = ((Cursor.Position.Y - this.DesktopLocation.Y ) / 50) - 1;

            history.Text += (x.ToString() + " ; " + y.ToString()+" | ");

            if (board[x, y].Image == whitesh.Image | board[x, y].Image == blacksh.Image)
            {
                string player = numberOfStep % 2 != 0 ? "white" : "black";

                MessageBox.Show(board[x, y].Image.GetType().ToString());
                
                if (player == "white" && board[x, y].Image == whitesh.Image)
                {
                    MessageBox.Show("Белые");
                    if (y > 0 && y < 7 && x > 1 && board[x - 1, y - 1].Image!= whitesh.Image && board[x - 1, y + 1].Image != whitesh.Image)
                    {
                        MessageBox.Show((x - 1).ToString() + " ; (" + (y - 1).ToString() + ", " + (y + 1).ToString()+")");
                        board[x - 1, y - 1].BackgroundImage = Properties.Resources.black_highlited;
                        board[x - 1, y + 1].BackgroundImage = Properties.Resources.black_highlited;

                    }
                    if (y == 0 && x > 1 && board[x - 1, y + 1].Image != whitesh.Image)
                    {
                        board[x - 1, y + 1].BackgroundImage = Properties.Resources.black_highlited;
                    }
                    if (y == 7 && x > 1 && board[x - 1, y - 1].Image != whitesh.Image)
                    {
                        board[x - 1, y - 1].BackgroundImage = Properties.Resources.black_highlited;
                    }
                }

                else
                {
                    if (board[x, y].Image == blacksh.Image)
                    {
                        MessageBox.Show("Чёрные");
                        if (y > 0 && y < 7 && x < 7)
                        {
                            board[x + 1, y - 1].BackgroundImage = Properties.Resources.black_highlited;
                            board[x + 1, y + 1].BackgroundImage = Properties.Resources.black_highlited;

                        }
                        if (y == 0 && x < 7)
                        {
                            board[x + 1, y + 1].BackgroundImage = Properties.Resources.black_highlited;
                        }
                        if (y == 7 && x < 7)
                        {
                            board[x + 1, y - 1].BackgroundImage = Properties.Resources.black_highlited;
                        }
                    }
                }
            }
            else
            {
                history.Text+="MISS\n";
            }             
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            //
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void stepper()
        {

        }
    }
}