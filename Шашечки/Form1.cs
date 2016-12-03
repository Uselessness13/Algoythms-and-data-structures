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
        private int numberOfStep = 1;
        private int whiteAte;
        private int blackAte;
        private string player = "white";
        private Dictionary<int, string> hod;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hod = new Dictionary<int, string>();
            hod.Add(0, "a");
            hod.Add(1, "b");
            hod.Add(2, "c");
            hod.Add(3, "d");
            hod.Add(4, "e");
            hod.Add(5, "f");
            hod.Add(6, "g");
            hod.Add(7, "h");
            int whiteAte = 0, blackAte = 0;
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

        private int[] current;
        private void boardpart_Click(object sender, EventArgs e)
        {
            int y = ((Cursor.Position.X + 20 - this.DesktopLocation.X) / 50) - 1;
            int x = ((Cursor.Position.Y - this.DesktopLocation.Y) / 50) - 1;

            if (board[x, y].Image == whitesh.Image | board[x, y].Image == blacksh.Image | board[x, y].Image == blackQueen.Image | board[x, y].Image == blackQueen.Image)
            {
                player = numberOfStep % 2 != 0 ? "white" : "black";
                playersname.Text = player + " turn";

                if (player == "white" && board[x, y].Image == whitesh.Image)
                {
                    cleaner();
                    current = new int[] { x, y };
                    whiteHighlighter(x, y);
                }
                if (player == "white" && board[x, y].Image == whiteQueen.Image)
                {
                    cleaner();
                    current = new int[] { x, y };
                    whiteQueenHighlither(x, y);
                }

                if (player == "black" && board[x,y].Image == blacksh.Image)
                {
                    cleaner();
                    current = new int[] { x, y };
                    blackHighlighter(x, y);
                }
                if (player == "black" && board[x, y].Image == blackQueen.Image)
                {
                    cleaner();
                    current = new int[] { x, y };
                    blackQueenHighlither(x, y);
                }
            }

            if (board[x,y].BackgroundImage == highlight.Image)
            {
                if (board[current[0],current[1]].Image == whitesh.Image)
                {
                    cleaner();
                    whiteStepper(current[0], current[1], x, y);
                    if (whiteEatChecker(x, y) && Math.Abs(current[0] - x) >= 2)
                    {
                        current = new int[] { x, y };
                    }
                    else
                    {
                        history.Text += hod[current[0]].ToString() + (current[1] - 1).ToString() + " -> " + hod[x].ToString() + (y - 1).ToString();
                        numberOfStep++;
                    }
                }
                if (board[current[0], current[1]].Image == whiteQueen.Image)
                {
                    cleaner();
                    whiteQueenStepper(current[0], current[1], x, y);
                    if (whiteQueenEatChecker(x, y))
                    {
                        current = new int[] { x, y };
                    }
                    else
                    {
                        history.Text += hod[current[0]].ToString() + (current[1] - 1).ToString() + " -> " + hod[x].ToString() + (y - 1).ToString();
                        numberOfStep++;
                    }
                }
                if (board[current[0],current[1]].Image == blacksh.Image)
                {
                    cleaner();
                    blackStepper(current[0], current[1], x, y);
                    if (blackEatChecker(x, y) && Math.Abs(current[0] - x) >= 2)
                    {
                        current = new int[] { x, y };
                    }
                    else
                    {
                        history.Text += hod[current[0]].ToString() + (current[1] - 1).ToString() + " -> " + hod[x].ToString() + (y - 1).ToString();
                        numberOfStep++;
                    }
                }
                if (board[current[0], current[1]].Image == blackQueen.Image)
                {
                    cleaner();
                    blackQueenStepper(current[0], current[1], x, y);
                    if (blackQueenEatChecker(x, y))
                    {
                        current = new int[] { x, y };
                    }
                    else
                    {
                        history.Text += hod[current[0]].ToString() + (current[1] - 1).ToString() + " -> " + hod[x].ToString() + (y - 1).ToString();
                        numberOfStep++;
                    }
                }
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

        public void whiteHighlighter(int x, int y)
        {
            if(y > 0 && y < 7 && x > 1)
            {
                if (board[x-1,y-1].Image == null)
                {
                    board[x - 1, y - 1].BackgroundImage = highlight.Image;
                }
                else if (board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image)
                {
                    whiteEater(x, y);
                }
                if (board[x - 1, y + 1].Image == null)
                {
                    board[x - 1, y + 1].BackgroundImage = highlight.Image;
                }
                else if (board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image)
                {
                    whiteEater(x, y);
                }

            }
            if (y == 0 && x > 1 && board[x - 1, y + 1].Image == null)
            {
                board[x - 1, y + 1].BackgroundImage = highlight.Image;
            }
            else if ((y == 0 && x > 1) && (board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image))
            {
                whiteEater(x, y);
            }
            if (y == 7 && x > 1 && board[x - 1, y - 1].Image == null)
            {
                board[x - 1, y - 1].BackgroundImage = highlight.Image;
            }
            else if ((y == 7 && x > 1) && (board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image))
            {
                whiteEater(x, y);
            }
        }

        public void whiteEater(int x, int y)
        {
            if (x > 1 && x < 6 && y > 1 && y < 6)
            {
                if ((board[x-1,y-1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x-2,y-2].Image == null)
                {
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }

                if ((board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blackQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x > 1 && x < 6 && y <= 1)
            {
                if ((board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blackQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x > 1 && x < 6 && y >= 6)
            {
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
            }
            if (x <= 1 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x>=6 && y > 1 && y < 6)
            {
                if ((board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blackQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
        }

        public bool whiteEatChecker(int x, int y)
        {
            bool answer = false;
            if (x > 1 && x < 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }

                if ((board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blackQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x > 1 && x < 6 && y <= 1)
            {
                if ((board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blackQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x > 1 && x < 6 && y >= 6)
            {
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
            }
            if (x <= 1 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x >= 6 && y > 1 && y < 6)
            {
                if ((board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blackQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            return answer;
        }

        public void whiteQueenHighlither(int x, int y)
        {
            int ulx = x, uly = y;
            while (ulx > 0 && uly > 0)
            {
                if (board[ulx, uly].Image == null)
                {
                    board[ulx, uly].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[ulx, uly].Image == whiteQueen.Image || board[ulx, uly].Image == whitesh.Image) break;
                    if (board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image)
                    {
                        if (ulx > 1 && uly > 1)
                        {
                            if (board[ulx - 1, uly - 1].Image == null)
                            {
                                board[ulx - 1, uly - 1].BackgroundImage = highlight.Image;
                            }
                        }
                    }
                }
                ulx--; uly--;
            }
            int urx = x, ury = y;
            while (urx > 0 && ury < 7)
            {
                if (board[urx, ury].Image == null)
                {
                    board[urx, ury].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image) break;
                    if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image)
                    {
                        if (urx > 1 && ury < 6)
                        {
                            if (board[urx - 1, ury + 1].Image == null)
                            {
                                board[urx - 1, ury + 1].BackgroundImage = highlight.Image;
                            }
                        }
                    }
                }
                urx--; ury++;
            }

            int dlx = x, dly = y;
            while (dlx < 7 && dly > 0)
            {
                if (board[dlx, dly].Image == null)
                {
                    board[dlx, dly].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[dlx, dly].Image == whiteQueen.Image || board[dlx, dly].Image == whitesh.Image) break;
                    if (board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image)
                    {
                        if (dlx < 6 && dly > 1)
                        {
                            if (board[dlx + 1, dly - 1].Image == null)
                            {
                                board[dlx + 1, dly - 1].BackgroundImage = highlight.Image;
                            }
                        }
                    }
                }
                dlx++; dly--;
            }
            int drx = x, dry = y;
            while (drx < 7 && dry < 7)
            {
                if (board[drx, dry].Image == null)
                {
                    board[drx, dry].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[drx, dry].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image) break;
                    if (board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image)
                    {
                        if (drx < 6 && dry < 6)
                        {
                            if (board[drx + 1, dry + 1].Image == null)
                            {
                                board[drx + 1, dry + 1].BackgroundImage = highlight.Image;
                            }
                        }
                    }
                }
                drx++; dry++;
            }
        }

        public bool whiteQueenEatChecker(int x, int y)
        {
            bool answer = false;
            int ulx = x, uly = y;
            bool ulanswer = false;
            while (ulx > 0 && uly > 0)
            {
                if (board[ulx, uly].Image == null) { }
                else
                {
                    if (board[ulx, uly].Image == whiteQueen.Image || board[ulx, uly].Image == whitesh.Image) break;
                    if (board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image)
                    {
                        if (ulx > 1 && uly > 1)
                        {
                            if (board[ulx - 1, uly - 1].Image == null)
                            {
                                board[ulx - 1, uly - 1].BackgroundImage = highlight.Image;
                                ulanswer = true;
                            }
                        }
                    }
                }
                ulx--; uly--;
            }
            int urx = x, ury = y;
            bool uranswer = false;
            while (urx > 0 && ury < 7)
            {
                if (board[urx, ury].Image == null){}
                else
                {
                    if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image) break;
                    if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image)
                    {
                        if (urx > 1 && ury < 6)
                        {
                            if (board[urx - 1, ury + 1].Image == null)
                            {
                                board[urx - 1, ury + 1].BackgroundImage = highlight.Image;
                                uranswer = true;
                            }
                        }
                    }
                }
                urx--; ury++;
            }

            int dlx = x, dly = y;
            bool dlanswer = false;
            while (dlx < 7 && dly > 0)
            {
                if (board[dlx, dly].Image == null){}
                else
                {
                    if (board[dlx, dly].Image == whiteQueen.Image || board[dlx, dly].Image == whitesh.Image) break;
                    if (board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image)
                    {
                        if (dlx < 6 && dly > 1)
                        {
                            if (board[dlx + 1, dly - 1].Image == null)
                            {
                                board[dlx + 1, dly - 1].BackgroundImage = highlight.Image;
                                dlanswer = true;
                            }
                        }
                    }
                }
                dlx++; dly--;
            }
            int drx = x, dry = y;
            bool dranswer = false;
            while (drx < 7 && dry < 7)
            {
                if (board[drx, dry].Image == null){}
                else
                {
                    if (board[drx, uly].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image) break;
                    if (board[drx, uly].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image)
                    {
                        if (drx < 6 && dry < 6)
                        {
                            if (board[drx + 1, dry + 1].Image == null)
                            {
                                board[drx + 1, dry + 1].BackgroundImage = highlight.Image;
                                dranswer = true;
                            }
                        }
                    }
                }
                drx++; dry++;
            }

            answer = ulanswer | uranswer | dlanswer | dranswer;
            return answer;
        }

        public void whiteStepper(int xn, int yn, int xk, int yk)
        {
            board[xn, yn].Image = null;
            if (xn < xk && yn < yk)
            {
                if (board[xk - 1, yk - 1].Image == blackQueen.Image | board[xk - 1, yk - 1].Image == blacksh.Image)
                {
                    whiteAte++;
                    board[xk - 1, yk - 1].Image = null;
                }
                
            }
            if (xn < xk && yn > yk)
            {
                if (board[xk - 1, yk + 1].Image == blackQueen.Image | board[xk - 1, yk + 1].Image == blacksh.Image)
                {
                    whiteAte++;
                    board[xk - 1, yk + 1].Image = null;
                }
            }
            if (xn > xk && yn < yk)
            {
                if (board[xk + 1, yk - 1].Image == blackQueen.Image | board[xk + 1, yk - 1].Image == blacksh.Image)
                {
                    whiteAte++;
                    board[xk + 1, yk - 1].Image = null;
                }
            }
            if (xn > xk && yn > yk)
            {
                if (board[xk + 1, yk + 1].Image == blackQueen.Image | board[xk + 1, yk + 1].Image == blacksh.Image)
                {
                    whiteAte++;
                    board[xk + 1, yk + 1].Image = null;
                }
            }
            board[xk, yk].Image = whitesh.Image;
        }

        public void whiteQueenStepper(int xn, int yn, int xk, int yk)
        {
            board[xn, yn].Image = null;
            if (xn < xk && yn < yk)
            {
                if (board[xk - 1, yk - 1].Image == blackQueen.Image | board[xk - 1, yk - 1].Image == blacksh.Image)
                {
                    whiteAte++;
                    board[xk - 1, yk - 1].Image = null;
                }

            }
            if (xn < xk && yn > yk)
            {
                if (board[xk - 1, yk + 1].Image == blackQueen.Image | board[xk - 1, yk + 1].Image == blacksh.Image)
                {
                    whiteAte++;
                    board[xk - 1, yk + 1].Image = null;
                }
            }
            if (xn > xk && yn < yk)
            {
                if (board[xk + 1, yk - 1].Image == blackQueen.Image | board[xk + 1, yk - 1].Image == blacksh.Image)
                {
                    whiteAte++;
                    board[xk + 1, yk - 1].Image = null;
                }
            }
            if (xn > xk && yn > yk)
            {
                if (board[xk + 1, yk + 1].Image == blackQueen.Image | board[xk + 1, yk + 1].Image == blacksh.Image)
                {
                    whiteAte++;
                    board[xk + 1, yk + 1].Image = null;
                }
            }
            board[xk, yk].Image = whiteQueen.Image;
        }

        // for black checkers

        public void blackHighlighter(int x, int y)
        {
            if (y > 0 && y < 7 && x < 6)
            {
                if (board[x + 1, y - 1].Image == null)
                {
                    board[x + 1, y - 1].BackgroundImage = highlight.Image;
                }
                else if (board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image)
                {
                    blackEater(x, y);
                }
                if (board[x + 1, y + 1].Image == null)
                {
                    board[x + 1, y + 1].BackgroundImage = highlight.Image;
                }
                else if (board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image)
                {
                    blackEater(x, y);
                }

            }
            if (y == 0 && x > 1 && board[x - 1, y + 1].Image == null)
            {
                board[x + 1, y + 1].BackgroundImage = highlight.Image;
            }
            else if ((y == 0 && x > 1) && (board[x + 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image))
            {
                blackEater(x, y);
            }
            if (y == 7 && x > 1 && board[x - 1, y - 1].Image == null)
            {
                board[x - 1, y - 1].BackgroundImage = highlight.Image;
            }
            else if ((y == 7 && x > 1) && (board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image))
            {
                blackEater(x, y);
            }
        }

        public void blackEater(int x, int y)
        {
            if (x > 1 && x < 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }

                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x > 1 && x < 6 && y <= 1)
            {
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x > 1 && x < 6 && y >= 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
            }
            if (x <= 1 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x >= 6 && y > 1 && y < 6)
            {
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
        }

        public bool blackEatChecker(int x,int y)
        {
            bool answer = false;
            if (x > 1 && x < 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }

                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x > 1 && x < 6 && y <= 1)
            {
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x > 1 && x < 6 && y >= 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
            }
            if (x <= 1 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x >= 6 && y > 1 && y < 6)
            {
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = true;
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            return answer;
        }

        public void blackQueenHighlither(int x, int y)
        {
            int ulx = x, uly = y;
            while (ulx > 0 && uly > 0)
            {
                if (board[ulx, uly].Image == null)
                {
                    board[ulx, uly].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image) break;
                    if (board[ulx, uly].Image == whiteQueen.Image || board[ulx, uly].Image == whitesh.Image)
                    {
                        if (ulx > 1 && uly > 1)
                        {
                            if (board[ulx - 1, uly - 1].Image == null)
                            {
                                board[ulx - 1, uly - 1].BackgroundImage = highlight.Image;
                            }
                        }
                    }
                }
                ulx--; uly--;
            }
            int urx = x, ury = y;
            while (urx > 0 && ury < 7)
            {
                if (board[urx, ury].Image == null)
                {
                    board[urx, ury].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image) break;
                    if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image)
                    {
                        if (urx > 1 && ury < 6)
                        {
                            if (board[urx - 1, ury + 1].Image == null)
                            {
                                board[urx - 1, ury + 1].BackgroundImage = highlight.Image;
                            }
                        }
                    }
                }
                urx--; ury++;
            }

            int dlx = x, dly = y;
            while (dlx < 7 && dly > 0)
            {
                if (board[dlx, dly].Image == null)
                {
                    board[dlx, dly].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image) break;
                    if (board[dlx, dly].Image == whiteQueen.Image || board[dlx, dly].Image == whitesh.Image)
                    {
                        if (dlx < 6 && dly > 1)
                        {
                            if (board[dlx + 1, dly - 1].Image == null)
                            {
                                board[dlx + 1, dly - 1].BackgroundImage = highlight.Image;
                            }
                        }
                    }
                }
                dlx++; dly--;
            }
            int drx = x, dry = y;
            while (drx < 7 && dry < 7)
            {
                if (board[drx, dry].Image == null)
                {
                    board[drx, dry].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image) break;
                    if (board[drx, dry].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image)
                    {
                        if (drx < 6 && dry < 6)
                        {
                            if (board[drx + 1, dry + 1].Image == null)
                            {
                                board[drx + 1, dry + 1].BackgroundImage = highlight.Image;
                            }
                        }
                    }
                }
                drx++; dry++;
            }
        }

        public bool blackQueenEatChecker(int x, int y)
        {
            bool answer = false;
            int ulx = x, uly = y;
            bool ulanswer = false;
            while (ulx > 0 && uly > 0)
            {
                if (board[ulx, uly].Image == null) { }
                else
                {
                    if (board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image) break;
                    if (board[ulx, uly].Image == whiteQueen.Image || board[ulx, uly].Image == whitesh.Image)
                    {
                        if (ulx > 1 && uly > 1)
                        {
                            if (board[ulx - 1, uly - 1].Image == null)
                            {
                                board[ulx - 1, uly - 1].BackgroundImage = highlight.Image;
                                ulanswer = true;
                            }
                        }
                    }
                }
                ulx--; uly--;
            }
            int urx = x, ury = y;
            bool uranswer = false;
            while (urx > 0 && ury < 7)
            {
                if (board[urx, ury].Image == null) { }
                else
                {
                    if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image) break;
                    if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image)
                    {
                        if (urx > 1 && ury < 6)
                        {
                            if (board[urx - 1, ury + 1].Image == null)
                            {
                                board[urx - 1, ury + 1].BackgroundImage = highlight.Image;
                                uranswer = true;
                            }
                        }
                    }
                }
                urx--; ury++;
            }

            int dlx = x, dly = y;
            bool dlanswer = false;
            while (dlx < 7 && dly > 0)
            {
                if (board[dlx, dly].Image == null) { }
                else
                {
                    if (board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image) break;
                    if (board[dlx, dly].Image == whiteQueen.Image || board[dlx, dly].Image == whitesh.Image)
                    {
                        if (dlx < 6 && dly > 1)
                        {
                            if (board[dlx + 1, dly - 1].Image == null)
                            {
                                board[dlx + 1, dly - 1].BackgroundImage = highlight.Image;
                                dlanswer = true;
                            }
                        }
                    }
                }
                dlx++; dly--;
            }
            int drx = x, dry = y;
            bool dranswer = false;
            while (drx < 7 && dry < 7)
            {
                if (board[drx, dry].Image == null) { }
                else
                {
                    if (board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image) break;
                    if (board[drx, dry].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image)
                    {
                        if (drx < 6 && dry < 6)
                        {
                            if (board[drx + 1, dry + 1].Image == null)
                            {
                                board[drx + 1, dry + 1].BackgroundImage = highlight.Image;
                                dranswer = true;
                            }
                        }
                    }
                }
                drx++; dry++;
            }

            answer = ulanswer | uranswer | dlanswer | dranswer;
            return answer;
        }

        public void blackStepper(int xn, int yn, int xk, int yk)
        {
            board[xn, yn].Image = null;
            if (xn < xk && yn < yk)
            {
                if (board[xk - 1, yk - 1].Image == whiteQueen.Image | board[xk - 1, yk - 1].Image == whitesh.Image)
                {
                    blackAte++;
                    board[xk - 1, yk - 1].Image = null;
                }

            }
            if (xn < xk && yn > yk)
            {
                if (board[xk - 1, yk + 1].Image == whiteQueen.Image | board[xk - 1, yk + 1].Image == whitesh.Image)
                {
                    blackAte++;
                    board[xk - 1, yk + 1].Image = null;
                }
            }
            if (xn > xk && yn < yk)
            {
                if (board[xk + 1, yk - 1].Image == whiteQueen.Image | board[xk + 1, yk - 1].Image == whitesh.Image)
                {
                    blackAte++;
                    board[xk + 1, yk - 1].Image = null;
                }
            }
            if (xn > xk && yn > yk)
            {
                if (board[xk + 1, yk + 1].Image == whiteQueen.Image | board[xk + 1, yk + 1].Image == whitesh.Image)
                {
                    blackAte++;
                    board[xk + 1, yk + 1].Image = null;
                }
            }
            board[xk, yk].Image = blacksh.Image;
        }

        public void blackQueenStepper(int xn, int yn, int xk, int yk)
        {
            board[xn, yn].Image = null;
            if (xn < xk && yn < yk)
            {
                if (board[xk - 1, yk - 1].Image == whiteQueen.Image | board[xk - 1, yk - 1].Image == whitesh.Image)
                {
                    blackAte++;
                    board[xk - 1, yk - 1].Image = null;
                }

            }
            if (xn < xk && yn > yk)
            {
                if (board[xk - 1, yk + 1].Image == whiteQueen.Image | board[xk - 1, yk + 1].Image == whitesh.Image)
                {
                    blackAte++;
                    board[xk - 1, yk + 1].Image = null;
                }
            }
            if (xn > xk && yn < yk)
            {
                if (board[xk + 1, yk - 1].Image == whiteQueen.Image | board[xk + 1, yk - 1].Image == whitesh.Image)
                {
                    blackAte++;
                    board[xk + 1, yk - 1].Image = null;
                }
            }
            if (xn > xk && yn > yk)
            {
                if (board[xk + 1, yk + 1].Image == whiteQueen.Image | board[xk + 1, yk + 1].Image == whitesh.Image)
                {
                    blackAte++;
                    board[xk + 1, yk + 1].Image = null;
                }
            }
            board[xk, yk].Image = blackQueen.Image;
        }

        public void cleaner()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j< 8; j++)
                {
                    board[i, j].BackgroundImage = board[i, j].BackgroundImage == highlight.Image? blacksh.BackgroundImage : board[i, j].BackgroundImage;
                }
            }
        }

        public void newGame()
        {
            blackAte = 0;
            whiteAte = 0;
            numberOfStep = 0;
            cleaner();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j].Image = null;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0){}
                        else
                        {
                            if (i <= 2)
                            {
                                board[i,j].Image = blacksh.Image;
                            }
                            if (i > 4)
                            {
                                board[i, j].Image = whitesh.Image;
                            }
                        }
                    }
                    else
                    {
                        if (j % 2 != 0){}
                        else
                        {

                            if (i <= 2)
                            {
                                board[i, j].Image = blacksh.Image;
                            }
                            if (i > 4)
                            {
                                board[i,j].Image = whitesh.Image;
                            }
                        }
                    }
                }
            }
        }

        private void startNewGame_Click(object sender, EventArgs e)
        {
            newGame();
        }
    }
}