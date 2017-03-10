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
        private String type;
        public Form1(String player)
        {
            type = player;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("loaded");
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
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
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
            if (type == "mem")
            {
                int y = ((Cursor.Position.X + 20 - this.DesktopLocation.X) / 50) - 1;
                int x = ((Cursor.Position.Y - this.DesktopLocation.Y) / 50) - 1;
                y = y > 7 ? 7 : y < 0 ? 0 : y;
                x = x > 7 ? 7 : x < 0 ? 0 : x;
                int blsch = blackAte;
                int whsch = whiteAte;
                //MessageBox.Show(x.ToString() + " " + y.ToString());
                whiteScore.Text = whiteAte.ToString();
                blackScore.Text = blackAte.ToString();
                if (board[x, y].Image == whitesh.Image | board[x, y].Image == blacksh.Image | board[x, y].Image == whiteQueen.Image | board[x, y].Image == blackQueen.Image)
                {
                    whiteScore.Text = whiteAte.ToString();
                    blackScore.Text = blackAte.ToString();
                    player = numberOfStep % 2 != 0 ? "white" : "black";
                    playersname.Text = player + " turn";

                    if (player == "white" && board[x, y].Image == whitesh.Image)
                    {
                        cleaner();
                        current = new int[] { x, y };
                        whiteHighlighter(x, y);
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                    }
                    if (player == "white" && board[x, y].Image == whiteQueen.Image)
                    {
                        cleaner();
                        current = new int[] { x, y };
                        whiteQueenHighlither(x, y);
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                    }

                    if (player == "black" && board[x, y].Image == blacksh.Image)
                    {
                        cleaner();
                        current = new int[] { x, y };
                        blackHighlighter(x, y);
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                    }
                    if (player == "black" && board[x, y].Image == blackQueen.Image)
                    {
                        cleaner();
                        current = new int[] { x, y };
                        blackQueenHighlither(x, y);
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                    }
                }

                if (board[x, y].BackgroundImage == highlight.Image)
                {
                    if (board[current[0], current[1]].Image == whitesh.Image)
                    {
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                        cleaner();
                        whiteStepper(current[0], current[1], x, y);
                        if (whiteEatChecker(x, y) && Math.Abs(current[0] - x) >= 2 && whiteAte > whsch)
                        {
                            current = new int[] { x, y };
                        }
                        else
                        {
                            cleaner();
                            history.Text += hod[x].ToString() + (current[1] - 1).ToString() + " -> " + hod[current[0]].ToString() + (y - 1).ToString() + "\n";
                            numberOfStep++;
                            player = numberOfStep % 2 != 0 ? "white" : "black";
                            playersname.Text = player + " turn";
                        }
                    }
                    if (board[current[0], current[1]].Image == whiteQueen.Image)
                    {
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                        cleaner();
                        whiteQueenStepper(current[0], current[1], x, y);
                        if (whiteQueenEatChecker(x, y) && whiteAte > whsch)
                        {
                            current = new int[] { x, y };
                        }
                        else
                        {
                            cleaner();
                            history.Text += hod[x].ToString() + (current[1] - 1).ToString() + " -> " + hod[current[0]].ToString() + (y - 1).ToString() + "\n";
                            numberOfStep++;
                            player = numberOfStep % 2 != 0 ? "white" : "black";
                            playersname.Text = player + " turn";
                        }
                    }
                    if (board[current[0], current[1]].Image == blacksh.Image)
                    {
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                        cleaner();
                        blackStepper(current[0], current[1], x, y);
                        if (blackEatChecker(x, y) && Math.Abs(current[0] - x) >= 2 && blackAte > blsch)
                        {
                            current = new int[] { x, y };
                        }
                        else
                        {
                            cleaner();
                            history.Text += hod[x].ToString() + (current[1] - 1).ToString() + " -> " + hod[current[0]].ToString() + (y - 1).ToString() + "\n";
                            numberOfStep++;
                            player = numberOfStep % 2 != 0 ? "white" : "black";
                            playersname.Text = player + " turn";
                        }
                    }
                    if (board[current[0], current[1]].Image == blackQueen.Image)
                    {
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                        cleaner();
                        blackQueenStepper(current[0], current[1], x, y);
                        if (blackQueenEatChecker(x, y) && blackAte > blsch)
                        {
                            current = new int[] { x, y };
                        }
                        else
                        {
                            cleaner();
                            history.Text += hod[x].ToString() + (current[1] - 1).ToString() + " -> " + hod[current[0]].ToString() + (y - 1).ToString() + "\n";
                            numberOfStep++;
                            player = numberOfStep % 2 != 0 ? "white" : "black";
                            playersname.Text = player + " turn";
                        }
                    }
                }
            }
                
            if (type == "mec")
            {
                int y = ((Cursor.Position.X + 20 - this.DesktopLocation.X) / 50) - 1;
                int x = ((Cursor.Position.Y - this.DesktopLocation.Y) / 50) - 1;
                y = y > 7 ? 7 : y < 0 ? 0 : y;
                x = x > 7 ? 7 : x < 0 ? 0 : x;
                int blsch = blackAte;
                int whsch = whiteAte;
                //MessageBox.Show(x.ToString() + " " + y.ToString());
                whiteScore.Text = whiteAte.ToString();
                blackScore.Text = blackAte.ToString();
                if (board[x, y].Image == whitesh.Image | board[x, y].Image == blacksh.Image | board[x, y].Image == whiteQueen.Image | board[x, y].Image == blackQueen.Image)
                {
                    whiteScore.Text = whiteAte.ToString();
                    blackScore.Text = blackAte.ToString();
                    player = numberOfStep % 2 != 0 ? "white" : "black";
                    playersname.Text = player + " turn";

                    if (player == "white" && board[x, y].Image == whitesh.Image)
                    {
                        cleaner();
                        current = new int[] { x, y };
                        whiteHighlighter(x, y);
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                    }
                    if (player == "white" && board[x, y].Image == whiteQueen.Image)
                    {
                        cleaner();
                        current = new int[] { x, y };
                        whiteQueenHighlither(x, y);
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                    }

                    //if (player == "black" && board[x, y].Image == blacksh.Image)
                    //{
                    //    cleaner();
                    //    current = new int[] { x, y };
                    //    blackHighlighter(x, y);
                    //    whiteScore.Text = whiteAte.ToString();
                    //    blackScore.Text = blackAte.ToString();
                    //}
                    //if (player == "black" && board[x, y].Image == blackQueen.Image)
                    //{
                    //    cleaner();
                    //    current = new int[] { x, y };
                    //    blackQueenHighlither(x, y);
                    //    whiteScore.Text = whiteAte.ToString();
                    //    blackScore.Text = blackAte.ToString();
                    //}
                }

                if (board[x, y].BackgroundImage == highlight.Image)
                {
                    if (board[current[0], current[1]].Image == whitesh.Image)
                    {
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                        cleaner();
                        whiteStepper(current[0], current[1], x, y);
                        if (whiteEatChecker(x, y) && Math.Abs(current[0] - x) >= 2 && whiteAte > whsch)
                        {
                            current = new int[] { x, y };
                        }
                        else
                        {
                            cleaner();
                            history.Text += hod[x].ToString() + (current[1] - 1).ToString() + " -> " + hod[current[0]].ToString() + (y - 1).ToString() + "\n";
                            numberOfStep++;
                            player = numberOfStep % 2 != 0 ? "white" : "black";
                            playersname.Text = player + " turn";
                        }
                        analyser();
                    }
                    if (board[current[0], current[1]].Image == whiteQueen.Image)
                    {
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                        cleaner();
                        whiteQueenStepper(current[0], current[1], x, y);
                        if (whiteQueenEatChecker(x, y) && whiteAte > whsch)
                        {
                            current = new int[] { x, y };
                        }
                        else
                        {
                            cleaner();
                            history.Text += hod[x].ToString() + (current[1] - 1).ToString() + " -> " + hod[current[0]].ToString() + (y - 1).ToString() + "\n";
                            numberOfStep++;
                            player = numberOfStep % 2 != 0 ? "white" : "black";
                            playersname.Text = player + " turn";
                        }
                        analyser();
                    }
                    //if (board[current[0], current[1]].Image == blacksh.Image)
                    //{
                    //    whiteScore.Text = whiteAte.ToString();
                    //    blackScore.Text = blackAte.ToString();
                    //    cleaner();
                    //    blackStepper(current[0], current[1], x, y);
                    //    if (blackEatChecker(x, y) && Math.Abs(current[0] - x) >= 2 && blackAte > blsch)
                    //    {
                    //        current = new int[] { x, y };
                    //    }
                    //    else
                    //    {
                    //        cleaner();
                    //        history.Text += hod[x].ToString() + (current[1] - 1).ToString() + " -> " + hod[current[0]].ToString() + (y - 1).ToString() + "\n";
                    //        numberOfStep++;
                    //        player = numberOfStep % 2 != 0 ? "white" : "black";
                    //        playersname.Text = player + " turn";
                    //    }
                    //}
                    //if (board[current[0], current[1]].Image == blackQueen.Image)
                    //{
                    //    whiteScore.Text = whiteAte.ToString();
                    //    blackScore.Text = blackAte.ToString();
                    //    cleaner();
                    //    blackQueenStepper(current[0], current[1], x, y);
                    //    if (blackQueenEatChecker(x, y) && blackAte > blsch)
                    //    {
                    //        current = new int[] { x, y };
                    //    }
                    //    else
                    //    {
                    //        cleaner();
                    //        history.Text += hod[x].ToString() + (current[1] - 1).ToString() + " -> " + hod[current[0]].ToString() + (y - 1).ToString() + "\n";
                    //        numberOfStep++;
                    //        player = numberOfStep % 2 != 0 ? "white" : "black";
                    //        playersname.Text = player + " turn";
                    //    }
                    //}
                }
            }

            if (whiteAte.ToString() == "12")
            {
                MessageBox.Show("White win!");
            }

            if (blackAte.ToString() == "12")
            {
                MessageBox.Show("Black win!");
            }
        }
        


        public void analyser()
        {
            List<int[]> coords = new List<int[]>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Image == blacksh.Image | board[i, j].Image == blackQueen.Image)
                    {
                        //Не работает
                        Console.WriteLine(i + "; " + j);
                        int[] cur = { i, j };
                        coords.Add(cur);
                    }
                }
            }
            Console.WriteLine("kolichestwo shashek " + coords.Count);
            for (int i = 0; i < coords.Count; i++)
            {
                Console.WriteLine(i + " " + coords[i][0] + "; " + coords[i][1]);
            }
            List<AICheckers> ocenki = new List<AICheckers>();
            for (int i = 0; i < coords.Count; i++)
            {
                ocenki.Add(ocenshik(coords[i][0], coords[i][1]));
            }
            Console.WriteLine("Вывод оценок " + ocenki.Count);
            for (int i = 0; i < ocenki.Count; i++)
            {
                if (ocenki[i] != null)
                    Console.WriteLine(i + " " + ocenki[i].ocenka + "; " + ocenki[i].firstXPos+"; "+ocenki[i].firstYPos + "; " + ocenki[i].secondXPos + "; " + ocenki[i].secondYPos);
            }
            //var all = new List<int[]>();
            //Dictionary<int, int[]> all = new Dictionary<int, int[]>();
            List<AICheckers> all = new List<AICheckers>();
            for (int i = 0; i < ocenki.Count; i++)
            {
                if (ocenki[i].ocenka != 0)
                {
                    Console.WriteLine("ocenci " + i + " " + ocenki[i]);
                    Console.WriteLine(ocenki[i].ocenka + " " + ocenki[i].firstXPos + " " + ocenki[i].firstYPos + " " + ocenki[i].secondXPos + " " + ocenki[i].secondYPos);
                    all.Add(ocenki[i]);
                }
                else Console.WriteLine(i + " null");
            }
            for (int i = 0; i < all.Count; i++)
            {
                for (int j = 0; j < all.Count - i - 1; j++)
                {
                    if (all[j].ocenka < all[j + 1].ocenka)
                    {
                        AICheckers temp = all[j];
                        all[j] = all[j + 1];
                        all[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine("all.Count = " + all.Count);
            for (int i = 0; i < all.Count; i++)
            {
                Console.WriteLine(all[i] + " " + all[i].ocenka + " " + all[i].firstXPos + " " + all[i].firstYPos + " " + all[i].secondXPos + " " + all[i].secondYPos);
            }

            if (!checkForHighlith() && all.Count > 0)
            {
                if (board[all[0].firstXPos, all[0].firstYPos].Image == blacksh.Image)
                {
                    blackStepper(all[0].firstXPos, all[0].firstYPos, all[0].secondXPos, all[0].secondYPos);
                }
                if (board[all[0].firstXPos, all[0].firstYPos].Image == blackQueen.Image)
                {
                    blackQueenStepper(all[0].firstXPos, all[0].firstYPos, all[0].secondXPos, all[0].secondYPos);
                }
                history.Text += hod[all[0].firstXPos].ToString() + (all[0].firstYPos - 1).ToString() + " -> " + hod[all[0].secondXPos].ToString() + (all[0].secondYPos - 1).ToString() + "\n";
                numberOfStep++;
                player = numberOfStep % 2 != 0 ? "white" : "black";
                playersname.Text = player + " turn";
            }
            //Console.Write("zero" + all[0].ocenka);
            //Console.Write("last" + all[all.Count-1].ocenka);
            // КЛИК по пикчербоксу???
            //InvokeOnClick(board[all[all.Count - 1].firstXPos, all[all.Count - 1].firstYPos], new EventArgs());
        }

        public bool checkForHighlith()
        {
            bool otvet = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].BackgroundImage == highlight.Image)
                        otvet = true;
                }
            }
            return otvet;
        }

        public AICheckers ocenshik(int x, int y)
        {
            int oc = 0;

            if (y > 0 && y < 7 && x > 0 && x < 7)
            {
                Console.WriteLine("Середина!!!");

                if (board[x + 1, y - 1].Image == null)
                {
                    oc = 1;
                    if (checkIfCantDie(x + 1, y - 1)) oc++;
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y - 1 };
                }

                if (board[x + 1, y + 1].Image == null)
                {
                    oc = 1;
                    if (checkIfCantDie(x + 1, y + 1)) oc++;
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y + 1 };
                }
                if (board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image)
                {
                    if (checkIfCanEat(x, y))
                    {
                        oc += checkIfCanEatAndCantDie(x, y);
                        return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 };
                    }
                }
                
                if (board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image)
                {
                    if (checkIfCanEat(x, y))
                    {
                        oc += checkIfCanEatAndCantDie(x, y);
                        return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 };
                    }
                }
                if (board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image)
                {
                    if (checkIfCanEat(x, y))
                    {
                        oc += checkIfCanEatAndCantDie(x, y);
                        return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 };
                    }
                }
                if (board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image)
                {
                    if (checkIfCanEat(x, y))
                    {
                        oc += checkIfCanEatAndCantDie(x, y);
                        return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 };
                    }
                }
            }
            if ((y <= 1 && x <= 6) && (board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image))
            {
                if (board[x + 1, y + 1].Image == null)
                {
                    oc = 1;
                    if (checkIfCantDie(x + 1, y + 1)) oc++;
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y + 1 };
                }
                Console.WriteLine("Левый край!!!");
                if (checkIfCanEat(x, y))
                {
                    oc += checkIfCanEatAndCantDie(x, y);
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 };

                }
            }
            if (y <= 1 && x >= 1 && board[x - 1, y + 1].Image == null)
            {
                Console.WriteLine("ЛЕвый край!!!");
                oc = 1;
                if (checkIfCantDie(x + 1, y + 1))
                {
                    oc++;
                }
                return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x - 1, secondYPos = y + 1 };
            }
            if ((y <= 1 && x > 1) && (board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image))
            {
                Console.WriteLine("ЛЕвый Край!!!");

                if (checkIfCanEat(x, y))
                {
                    oc += checkIfCanEatAndCantDie(x, y);
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 };
                }
            }
            if ((y <= 1 && x < 7) && (board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image))
            {
                Console.WriteLine("ЛЕвый Край!!!");

                if (checkIfCanEat(x, y))
                {
                    oc += checkIfCanEatAndCantDie(x, y);
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 };
                }
            }
            if (y >= 6 && x <= 6)
            {
                if (board[x + 1, y - 1].Image == null)
                {
                    oc = 1;
                    if (checkIfCantDie(x + 1, y - 1)) oc++;
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y - 1 };
                }
            }
            if ((y >= 6 && x <= 6) && (board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image))
            {
                Console.WriteLine("Правый край!!!");

                if (checkIfCanEat(x, y))
                {
                    oc += checkIfCanEatAndCantDie(x, y);
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 };
                }
            }
            if ((y >= 6 && x > 1) && (board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image))
            {
                Console.WriteLine("Правый край!!!");

                if (checkIfCanEat(x, y))
                {
                    oc += checkIfCanEatAndCantDie(x, y);
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 };
                }
            }
            if (y >= 6 && x >= 1 && board[x + 1, y - 1].Image == null)
            {
                Console.WriteLine("ПРавый край!!!");

                oc = 1;
                if (checkIfCantDie(x + 1, y - 1))
                {   
                    oc++;
                }
                return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y - 1 };
            }
            if ((y >= 6 && x > 1) && (board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image))
            {
                Console.WriteLine("Правый Край!!!");

                if (checkIfCanEat(x, y))
                {
                    oc += checkIfCanEatAndCantDie(x, y);
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 };
                }
            }
            if (y >= 1 && y <= 6 && x >= 6)
            {
                Console.WriteLine("НИЗ!!!");

                if (board[x + 1, y - 1].Image == null)
                {
                    oc = 1;
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y - 1 };
                }
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && y != 1)
                {
                    if (checkIfCanEat(x, y))
                    {
                        oc += checkIfCanEatAndCantDie(x, y);
                        return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 };
                    }
                }
                if (board[x - 1, y + 1].Image == null)
                {
                    oc = 1;
                    return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x - 1, secondYPos = y + 1 };
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && y != 6)
                {
                    if (checkIfCanEat(x, y))
                    {
                        oc += checkIfCanEatAndCantDie(x, y);
                        return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 };
                    }
                }
            }
            if (oc == 0)
            {
                Console.WriteLine("null zero ocenka");
                return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y };
            }
            return new AICheckers { ocenka = oc, firstXPos = x, firstYPos = y};
        }

        public bool checkIfCanEat(int x, int y)
        {
            bool answer = false;
            if (x > 1 && x < 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = true;
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = true;
                }

                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = true;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = true;
                }
            }
            if (x > 1 && x < 6 && y <= 1)
            {
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = true;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = true;
                }
            }
            if (x > 1 && x < 6 && y >= 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = true;
                }
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = true;
                }
            }
            if (x <= 1 && y > 1 && y < 6)
            {
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = true;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = true;
                }
            }
            if (x >= 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = true;
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = true;
                }
            }
            return answer;
        }
        public bool checkIfCantDie(int x, int y)
        {
            if (x >=1 && x <=6 && y >= 1 && y <= 6)
            {
                if ((board[x - 1, y - 1].Image == whiteQueen.Image | board[x - 1, y - 1].Image == whitesh.Image) && board[x+1, y+1].Image == null)
                {
                    return false;
                }
                if ((board[x - 1, y + 1].Image == whiteQueen.Image | board[x - 1, y + 1].Image == whitesh.Image) && board[x + 1, y - 1].Image == null)
                {
                    return false;
                }
                if ((board[x + 1, y - 1].Image == whiteQueen.Image | board[x + 1, y - 1].Image == whitesh.Image) && board[x - 1, y + 1].Image == null)
                {
                    return false;
                }
                if ((board[x + 1, y + 1].Image == whiteQueen.Image | board[x + 1, y + 1].Image == whitesh.Image) && board[x - 1, y - 1].Image == null)
                {
                    return false;
                }
            }
            return true;
        }
        public int checkIfCanEatAndCantDie(int x, int y)
        {
            int answer = 0;
            if (x > 1 && x < 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x - 2, y - 2))
                    {
                        answer = 2;
                    }
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x - 2, y + 2))
                    {
                        answer = 2;
                    }
                }

                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x + 2, y - 2))
                    {
                        answer = 2;
                    }
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x + 2, y + 2))
                    {
                        answer = 2;
                    }
                }
            }
            if (x > 1 && x < 6 && y <= 1)
            {
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x - 2, y + 2))
                    {
                        answer = 2;
                    }
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x + 2, y + 2))
                    {
                        answer = 2;
                    }
                }
            }
            if (x > 1 && x < 6 && y >= 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x - 2, y - 2))
                    {
                        answer = 2;
                    }
                }
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x + 2, y - 2))
                    {
                        answer = 2;
                    }
                }
            }
            if (x <= 1 && y > 1 && y < 6)
            {
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x + 2, y - 2))
                    {
                        answer = 2;
                    }
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x + 2, y + 2))
                    {
                        answer = 2;
                    }
                }
            }
            if (x >= 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x - 2, y - 2))
                    {
                        answer = 2;
                    }
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x - 2, y + 2))
                    {
                        answer = 2;
                    }
                }
            }
            return answer;
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
            if (y > 0 && y < 7 && x > 0 && x < 7)
            {
                if (board[x - 1, y - 1].Image == null)
                {
                    board[x - 1, y - 1].BackgroundImage = highlight.Image;
                }
                if (board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image)
                {
                    whiteEater(x, y);
                }
                if (board[x - 1, y + 1].Image == null)
                {
                    board[x - 1, y + 1].BackgroundImage = highlight.Image;
                }
                if (board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image)
                {
                    whiteEater(x, y);
                }
                if (board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blackQueen.Image)
                {
                    whiteEater(x, y);
                }
                if (board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image)
                {
                    whiteEater(x, y);
                }
            }
            if ((y <= 1 && x <= 6) && (board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blackQueen.Image))
            {
                //MessageBox.Show("iyetyi");
                whiteEater(x, y);
            }
            if (y <= 1 && x >= 1 && board[x - 1, y + 1].Image == null)
            {
                board[x - 1, y + 1].BackgroundImage = highlight.Image;
            }
            if ((y <= 1 && x > 1) && (board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image))
            {
                whiteEater(x, y);
            }
            if ((y >= 6 && x <= 6) && (board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image))
            {
                //MessageBox.Show("iyetyi");
                whiteEater(x, y);
            }
            if (y >= 6 && x >= 1 && board[x - 1, y - 1].Image == null)
            {
                board[x - 1, y - 1].BackgroundImage = highlight.Image;
            }
            if ((y >= 6 && x > 1) && (board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image))
            {
                whiteEater(x, y);
            }
            if (y >= 1 && y <= 6 && x >= 6)
            {
                if (board[x - 1, y - 1].Image == null)
                {
                    board[x - 1, y - 1].BackgroundImage = highlight.Image;
                }
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && y != 1)
                {
                    whiteEater(x, y);
                }
                if (board[x - 1, y + 1].Image == null)
                {
                    board[x - 1, y + 1].BackgroundImage = highlight.Image;
                }
                if ((board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image) && y != 6)
                {
                    whiteEater(x, y);
                }
            }
        }

        public void whiteEater(int x, int y)
        {
            //MessageBox.Show("ent");
            if (x > 1 && x < 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x - 2, y - 2].Image == null)
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
                //MessageBox.Show("sh");
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
            }
            if (y >= 6 && x > 1)
            {
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
            }
            if (y >= 6 && x < 1)
            {
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
                }
            }
            if (y <= 1 && x < 6 && x > 1)
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
            if (x <= 1 && y > 1 && y < 6)
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
            if (x >= 6 && y > 1 && y < 6)
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
            if (x >= 6 && y <= 1)
            {
                if ((board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x >= 6 && y >= 6)
            {
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    board[x - 2, y - 2].BackgroundImage = highlight.Image;
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
            if (x >= 6 && y > 1 && y < 6)
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
            return answer;
        }

        public void whiteQueenHighlither(int x, int y)
        {
            //MessageBox.Show("highlighter");
            int ulx = x;
            int uly = y;
            while (ulx >= 0 && uly >= 0)
            {
                if (board[ulx, uly].Image == null)
                {
                    board[ulx, uly].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[ulx, uly].Image == whiteQueen.Image || board[ulx, uly].Image == whitesh.Image)
                    {
                        if (uly != y && ulx != x) break;
                    }
                    if (board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image)
                    {
                        if (ulx >= 1 && uly >= 1)
                        {
                            if (board[ulx - 1, uly - 1].Image == null)
                            {
                                board[ulx - 1, uly - 1].BackgroundImage = highlight.Image;
                                break;
                            }
                            else break;
                        }
                    }
                }
                ulx--; uly--;
            }
            int urx = x;
            int ury = y;
            while (urx >= 0 && ury <= 7)
            {
                if (board[urx, ury].Image == null)
                {
                    board[urx, ury].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image)
                    {
                        if (ury != y && urx != x) break;
                    }
                    if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image)
                    {
                        if (urx >= 1 && ury <= 6)
                        {
                            if (board[urx - 1, ury + 1].Image == null)
                            {
                                board[urx - 1, ury + 1].BackgroundImage = highlight.Image;
                                break;
                            }
                            else break;
                        }
                    }
                }
                urx--; ury++;
            }

            int dlx = x;
            int dly = y;
            while (dlx <= 7 && dly >= 0)
            {
                if (board[dlx, dly].Image == null)
                {
                    board[dlx, dly].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[dlx, dly].Image == whiteQueen.Image || board[dlx, dly].Image == whitesh.Image)
                    {
                        if (dly != y && dlx != x) break;
                    }
                    if (board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image)
                    {
                        if (dlx <= 6 && dly >= 1)
                        {
                            if (board[dlx + 1, dly - 1].Image == null)
                            {
                                board[dlx + 1, dly - 1].BackgroundImage = highlight.Image;
                                break;
                            }
                            else break;
                        }
                    }
                }
                dlx++; dly--;
            }
            int drx = x;
            int dry = y;
            while (drx <= 7 && dry <= 7)
            {
                if (board[drx, dry].Image == null)
                {
                    board[drx, dry].BackgroundImage = highlight.Image;
                }
                else
                {
                    if (board[drx, dry].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image)
                    {
                        if (dry != y && drx != x) break;
                    }
                    if (board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image)
                    {
                        if (drx <= 6 && dry <= 6)
                        {
                            if (board[drx + 1, dry + 1].Image == null)
                            {
                                board[drx + 1, dry + 1].BackgroundImage = highlight.Image;
                                break;
                            }
                            else break;
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
            while (ulx >= 0 && uly >= 0)
            {
                if (board[ulx, uly].Image == null) { }
                else
                {
                    if (board[ulx, uly].Image == whiteQueen.Image || board[ulx, uly].Image == whitesh.Image)
                    {
                        if (uly != y && ulx != x) break;
                    }
                    if (board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image)
                    {
                        if (ulx >= 1 && uly >= 1)
                        {
                            if (board[ulx - 1, uly - 1].Image == null)
                            {
                                board[ulx - 1, uly - 1].BackgroundImage = highlight.Image;
                                ulanswer = true;
                                break;
                            }
                            else break;
                        }
                    }
                }
                ulx--; uly--;
            }
            int urx = x, ury = y;
            bool uranswer = false;
            while (urx >= 0 && ury <= 7)
            {
                if (board[urx, ury].Image == null) { }
                else
                {
                    if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image)
                    {
                        if (ury != y && urx != x) break;
                    }
                    if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image)
                    {
                        if (urx >= 1 && ury <= 6)
                        {
                            if (board[urx - 1, ury + 1].Image == null)
                            {
                                board[urx - 1, ury + 1].BackgroundImage = highlight.Image;
                                uranswer = true;
                                break;
                            }
                            else break;
                        }
                    }
                }
                urx--; ury++;
            }

            int dlx = x, dly = y;
            bool dlanswer = false;
            while (dlx <= 7 && dly >= 0)
            {
                if (board[dlx, dly].Image == null) { }
                else
                {
                    if (board[dlx, dly].Image == whiteQueen.Image || board[dlx, dly].Image == whitesh.Image)
                    {
                        if (dly != y && dlx != x) break;
                    }
                    if (board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image)
                    {
                        if (dlx <= 6 && dly >= 1)
                        {
                            if (board[dlx + 1, dly - 1].Image == null)
                            {
                                board[dlx + 1, dly - 1].BackgroundImage = highlight.Image;
                                dlanswer = true;
                                break;
                            }
                            else break;
                        }
                    }
                }
                dlx++; dly--;
            }
            int drx = x, dry = y;
            bool dranswer = false;
            while (drx <= 7 && dry <= 7)
            {
                if (board[drx, dry].Image == null) { }
                else
                {
                    if (board[drx, dry].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image)
                    {
                        if (dry != y && drx != x) break;
                    }
                    if (board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image)
                    {
                        if (drx <= 6 && dry <= 6)
                        {
                            if (board[drx + 1, dry + 1].Image == null)
                            {
                                board[drx + 1, dry + 1].BackgroundImage = highlight.Image;
                                dranswer = true;
                                break;
                            }
                            else break;
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
            if (xk == 0) board[xk, yk].Image = whiteQueen.Image;
            else board[xk, yk].Image = whitesh.Image;
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
            if (y > 0 && y < 7 && x < 7 && x > 0)
            {
                if (board[x + 1, y - 1].Image == null)
                {
                    board[x + 1, y - 1].BackgroundImage = highlight.Image;
                }
                if (board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image)
                {
                    blackEater(x, y);
                }
                if (board[x + 1, y + 1].Image == null)
                {
                    board[x + 1, y + 1].BackgroundImage = highlight.Image;
                }
                if (board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image)
                {
                    blackEater(x, y);
                }
                if (board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image)
                {
                    blackEater(x, y);
                }
                if (board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image)
                {
                    blackEater(x, y);
                }

            }
            if ((y <= 1 && x >= 1) && (board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image))
            {
                blackEater(x, y);
            }
            if (y <= 1 && x >= 1 && board[x + 1, y + 1].Image == null)
            {
                board[x + 1, y + 1].BackgroundImage = highlight.Image;
            }
            if ((y <= 1 && x >= 1) && (board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image))
            {
                blackEater(x, y);
            }
            if ((y <= 1 && x < 6) && (board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image))
            {
                blackEater(x, y);
            }
            if ((y >= 6 && x >= 1) && (board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image))
            {
                blackEater(x, y);
            }
            if (y >= 6 && x <= 6 && board[x + 1, y - 1].Image == null)
            {
                board[x + 1, y - 1].BackgroundImage = highlight.Image;
            }
            if ((y >= 6 && x <= 6) && (board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image))
            {
                blackEater(x, y);
            }
            if (y >= 1 && y <= 6 && x <= 1)
            {
                if (board[x + 1, y + 1].Image == null)
                {
                    board[x + 1, y + 1].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && y != 6)
                {
                    blackEater(x, y);
                }
                if (board[x + 1, y - 1].Image == null)
                {
                    board[x + 1, y - 1].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && y != 1)
                {
                    blackEater(x, y);
                }
            }
        }

        public void blackEater(int x, int y)
        {
            //MessageBox.Show("ent");
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
                //MessageBox.Show("kj");
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }

            if (x <= 1 && y <= 1)
            {
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
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x <= 1 && y >= 6)
            {
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
            }
            if (x == 0 && y == 0)
            {
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x == 7 && y == 0)
            {
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    board[x - 2, y + 2].BackgroundImage = highlight.Image;
                }
            }
            if (x >= 6 && y >= 7)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    board[x + 2, y - 2].BackgroundImage = highlight.Image;
                }
            }
            if (x >= 6 && y > 1 && y < 6)
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
        }

        public bool blackEatChecker(int x, int y)
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
            if (x >= 6 && y > 1 && y < 6)
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
            return answer;
        }

        public void blackQueenHighlither(int x, int y)
        {
            int ulx, uly;
            ulx = x;
            uly = y;
            while (ulx >= 0 && uly >= 0)
            {
                if (board[ulx, uly].Image == null)
                {
                    board[ulx, uly].BackgroundImage = highlight.Image;
                }
                else
                {
                    if ((board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image) && (ulx != x && uly != y)) break;
                    if (board[ulx, uly].Image == whiteQueen.Image || board[ulx, uly].Image == whitesh.Image)
                    {
                        if (ulx >= 1 && uly >= 1)
                        {
                            if (board[ulx - 1, uly - 1].Image == null)
                            {
                                board[ulx - 1, uly - 1].BackgroundImage = highlight.Image;
                                break;
                            }
                            else break;
                        }
                    }
                }
                ulx--; uly--;
            }
            int urx, ury;
            urx = x;
            ury = y;
            while (urx >= 0 && ury <= 7)
            {
                if (board[urx, ury].Image == null)
                {
                    board[urx, ury].BackgroundImage = highlight.Image;
                }
                else
                {
                    if ((board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image) && (urx != x && ury != y)) break;
                    if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image)
                    {
                        if (urx >= 1 && ury <= 6)
                        {
                            if (board[urx - 1, ury + 1].Image == null)
                            {
                                board[urx - 1, ury + 1].BackgroundImage = highlight.Image;
                                break;
                            }
                            else break;
                        }
                    }
                }
                urx--; ury++;
            }

            int dlx, dly;
            dlx = x;
            dly = y;
            while (dlx <= 7 && dly >= 0)
            {
                if (board[dlx, dly].Image == null)
                {
                    board[dlx, dly].BackgroundImage = highlight.Image;
                }
                else
                {
                    if ((board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image) && (dlx != x && dly != y)) break;
                    if (board[dlx, dly].Image == whiteQueen.Image || board[dlx, dly].Image == whitesh.Image)
                    {
                        if (dlx <= 6 && dly >= 1)
                        {
                            if (board[dlx + 1, dly - 1].Image == null)
                            {
                                board[dlx + 1, dly - 1].BackgroundImage = highlight.Image;
                                break;
                            }
                            else break;
                        }
                    }
                }
                dlx++; dly--;
            }
            int drx = x, dry = y;
            while (drx <= 7 && dry <= 7)
            {
                if (board[drx, dry].Image == null)
                {
                    board[drx, dry].BackgroundImage = highlight.Image;
                }
                else
                {
                    if ((board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image) && (drx != x && dry != y)) break;
                    if (board[drx, dry].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image)
                    {
                        if (drx <= 6 && dry <= 6)
                        {
                            if (board[drx + 1, dry + 1].Image == null)
                            {
                                board[drx + 1, dry + 1].BackgroundImage = highlight.Image;
                                break;
                            }
                            else break;
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
            while (ulx >= 0 && uly >= 0)
            {
                if (board[ulx, uly].Image == null) { }
                else
                {
                    if (board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image)
                    {
                        if (uly != y && ulx != x) break;
                    }

                    if (board[ulx, uly].Image == whiteQueen.Image || board[ulx, uly].Image == whitesh.Image)
                    {
                        if (ulx >= 1 && uly >= 1)
                        {
                            if (board[ulx - 1, uly - 1].Image == null)
                            {
                                board[ulx - 1, uly - 1].BackgroundImage = highlight.Image;
                                ulanswer = true;
                                break;
                            }
                            else break;
                        }
                    }
                }
                ulx--; uly--;
            }
            int urx = x, ury = y;
            bool uranswer = false;
            while (urx >= 0 && ury <= 7)
            {
                if (board[urx, ury].Image == null) { }
                else
                {
                    if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image)
                    {
                        if (ury != y && urx != x) break;
                    }
                    if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image)
                    {
                        if (urx >= 1 && ury <= 6)
                        {
                            if (board[urx - 1, ury + 1].Image == null)
                            {
                                board[urx - 1, ury + 1].BackgroundImage = highlight.Image;
                                uranswer = true;
                                break;
                            }
                            else break;
                        }
                    }
                }
                urx--; ury++;
            }

            int dlx = x, dly = y;
            bool dlanswer = false;
            while (dlx <= 7 && dly >= 0)
            {
                if (board[dlx, dly].Image == null) { }
                else
                {
                    if (board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image)
                    {
                        if (dly != y && dlx != x) break;
                    }
                    if (board[dlx, dly].Image == whiteQueen.Image || board[dlx, dly].Image == whitesh.Image)
                    {
                        if (dlx <= 6 && dly >= 1)
                        {
                            if (board[dlx + 1, dly - 1].Image == null)
                            {
                                board[dlx + 1, dly - 1].BackgroundImage = highlight.Image;
                                dlanswer = true;
                                break;
                            }
                            else break;
                        }
                    }
                }
                dlx++; dly--;
            }
            int drx = x, dry = y;
            bool dranswer = false;
            while (drx <= 7 && dry <= 7)
            {
                if (board[drx, dry].Image == null) { }
                else
                {
                    if (board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image)
                    {
                        if (dry != y && drx != x) break;
                    }
                    if (board[drx, dry].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image)
                    {
                        if (drx <= 6 && dry <= 6)
                        {
                            if (board[drx + 1, dry + 1].Image == null)
                            {
                                board[drx + 1, dry + 1].BackgroundImage = highlight.Image;
                                dranswer = true;
                                break;
                            }
                            else break;
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
            if (xk == 7) board[xk, yk].Image = blackQueen.Image;
            else board[xk, yk].Image = blacksh.Image;
            whiteScore.Text = whiteAte.ToString();
            blackScore.Text = blackAte.ToString();
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
            whiteScore.Text = whiteAte.ToString();
            blackScore.Text = blackAte.ToString();
        }

        public void cleaner()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j].BackgroundImage = board[i, j].BackgroundImage == highlight.Image ? blacksh.BackgroundImage : board[i, j].BackgroundImage;
                }
            }
        }

        public void newGame()
        {
            history.Text = "";
            player = "white";
            playersname.Text = "white turn";
            blackAte = 0;
            whiteAte = 0;
            numberOfStep = 1;
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
                        if (j % 2 == 0) { }
                        else
                        {
                            if (i <= 2)
                            {
                                board[i, j].Image = blacksh.Image;
                            }
                            if (i > 4)
                            {
                                board[i, j].Image = whitesh.Image;
                            }
                        }
                    }
                    else
                    {
                        if (j % 2 != 0) { }
                        else
                        {

                            if (i <= 2)
                            {
                                board[i, j].Image = blacksh.Image;
                            }
                            if (i > 4)
                            {
                                board[i, j].Image = whitesh.Image;
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