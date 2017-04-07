using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        private Random random = new Random();
        TcpClient client;
        string colour = "black";
        public Form1(String player)
        {
            type = player;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = false;
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

        public void connection()
        {
            timer1.Enabled = true;
        }

        private int[] current;

        private void boardpart_Click(object sender, EventArgs e)
        {
            if (type == "tour")
            {

            }
            if (type == "mem")
            {
                int y = ((Cursor.Position.X + 20 - this.DesktopLocation.X) / 50) - 1;
                int x = ((Cursor.Position.Y - this.DesktopLocation.Y) / 50) - 1;
                y = y > 7 ? 7 : y < 0 ? 0 : y;
                x = x > 7 ? 7 : x < 0 ? 0 : x;
                int blsch = blackAte;
                int whsch = whiteAte;
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
                        if (!checkForHighlith())
                            analyser();
                    }
                    if (board[current[0], current[1]].Image == whitesh.Image && x == 0)
                    {
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                        cleaner();
                        whiteStepper(current[0], current[1], x, y);
                        cleaner();
                        history.Text += hod[x].ToString() + (current[1] - 1).ToString() + " -> " + hod[current[0]].ToString() + (y - 1).ToString() + "\n";
                        numberOfStep++;
                        player = numberOfStep % 2 != 0 ? "white" : "black";
                        playersname.Text = player + " turn";
                        if (!checkForHighlith())
                            analyser();
                    }
                    if (board[current[0], current[1]].Image == whiteQueen.Image)
                    {
                        whiteScore.Text = whiteAte.ToString();
                        blackScore.Text = blackAte.ToString();
                        cleaner();
                        whiteQueenStepper(current[0], current[1], x, y);
                        if ((whiteQueenEatChecker(x, y) && whiteAte > whsch))
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
                        if (!checkForHighlith())
                            analyser();
                    }
                }
            }
            judje();
            if (whiteAte.ToString() == "12")
                MessageBox.Show("White win!");
            if (blackAte.ToString() == "12")
                MessageBox.Show("Black win!");
        }

        public void judje()
        {
            int w = 0, b = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    w += (board[i, j].Image == whitesh.Image || board[i, j].Image == whiteQueen.Image) ? 1 : 0;
                    b += (board[i, j].Image == blacksh.Image || board[i, j].Image == blackQueen.Image) ? 1 : 0;
                }
            whiteScore.Text = (12 - b).ToString();
            blackScore.Text = (12 - w).ToString();
        }

        public void analyser()
        {
            List<int[]> coords = new List<int[]>();
            if (colour == "black")
            {
                coords = searchForCheckers();
                List<AICheckers> ocenki = new List<AICheckers>();
                for (int i = 0; i < coords.Count; i++)
                {
                    var step = allhods(coords[i][0], coords[i][1]);
                    for (int j = 0; j < step.Count; j++)
                        ocenki.Add(step[j]);
                }

                List<AICheckers> allh = new List<AICheckers>();
                for (int i = 0; i < ocenki.Count; i++)
                    if (ocenki[i].ocenka >= 0)
                        allh.Add(ocenki[i]);

                Console.WriteLine("allh.count = " + allh.Count);
                foreach (AICheckers h in allh)
                    Console.WriteLine(h.ocenka + " | " + h.firstXPos + " ; " + h.firstYPos + " | " + h.secondXPos + " ; " + h.secondYPos);

                Console.WriteLine("after sorting");

                var all = sorter(allh);
                Console.WriteLine("all.count = " + all.Count);
                foreach (AICheckers h in all)
                    Console.WriteLine(h.ocenka + " | " + h.firstXPos + " ; " + h.firstYPos + " | " + h.secondXPos + " ; " + h.secondYPos);
                cleaner();
                if (!checkForHighlith() && all.Count > 0)
                {
                    int curate = blackAte;
                    Console.Write(curate);
                    Console.Write(" curate\n");
                    int r = random.Next(all.Count);
                    makestep(all[r].firstXPos, all[r].firstYPos, all[r].secondXPos, all[r].secondYPos);

                    history.Text += hod[all[r].firstXPos].ToString() + (all[r].firstYPos - 1).ToString() + " -> " + hod[all[r].secondXPos].ToString() + (all[r].secondYPos - 1).ToString() + "\n";
                    Console.WriteLine("MADE STEP");
                    Console.WriteLine(all[r].ocenka + " | " + all[r].firstXPos + " ; " + all[r].firstYPos + " | " + all[r].secondXPos + " ; " + all[r].secondYPos);
                    if (curate - blackAte == 0 || !checkIfCanEat(all[r].secondXPos, all[r].secondYPos))
                    {
                        Console.WriteLine(curate - blackAte == 0 ? "MADE STEP without eating" : "");
                        numberOfStep++;
                        player = numberOfStep % 2 != 0 ? "white" : "black";
                        playersname.Text = player + " turn";
                    }
                    else if (curate != blackAte && checkIfCanEat(all[0].secondXPos, all[0].secondYPos))
                    {
                        while (curate != blackAte && checkIfCanEat(all[0].secondXPos, all[0].secondYPos))
                        {
                            curate = blackAte;
                            bool wp = board[all[0].secondXPos, all[0].secondYPos].Image == blacksh.Image;
                            var hodiks = allhodstoEat(all[0].secondXPos, all[0].secondYPos);

                            var hodik = sorter(hodiks);

                            for (int i = 0; i < hodik.Count; i++)
                                Console.WriteLine(hodik[i].ocenka + " | " + hodik[i].firstXPos + " | " + hodik[i].firstYPos + " | " + hodik[i].secondXPos + " | " + hodik[i].secondYPos);
                            r = random.Next(hodik.Count);
                            Console.WriteLine("after sorting");
                            makestep(hodik[r].firstXPos, hodik[r].firstYPos, hodik[r].secondXPos, hodik[r].secondYPos);
                            foreach (AICheckers h in hodik)
                                Console.WriteLine(h.ocenka + " | " + h.firstXPos + " ; " + h.firstYPos + " | " + h.secondXPos + " ; " + h.secondYPos);
                            if (hodik[r].secondXPos == 7 & wp) break;
                        }
                        numberOfStep++;
                        player = numberOfStep % 2 != 0 ? "white" : "black";
                        playersname.Text = player + " turn";
                    }
                }
            }
            if (colour == "white")
            {
                //нормальный ИИ за белых
                List<int[]> checkers = searchForCheckers(); // Ищем все белые шашки
                List<AICheckers> possibleMoves = new List<AICheckers>();
                for (int i = 0; i < checkers.Count; i++)
                {
                    List<AICheckers> currentCheckersPossibleMoves = allhods(checkers[i][0], checkers[i][1]); // Находим все ходы конкретной шашки
                    for (int j = 0; j < currentCheckersPossibleMoves.Count; j++)
                        if (currentCheckersPossibleMoves[j].ocenka >= 0)
                            possibleMoves.Add(currentCheckersPossibleMoves[j]); // Добавляем их в список всех ходов
                }
                //possibleMoves.RemoveAll(null); //Чистим от "плохих" ходов
                var moves = sorter(possibleMoves);


                int curate = whiteAte;
                if (!checkForHighlith() && moves.Count > 0)
                {
                    int r = random.Next(moves.Count);
                    makestep(moves[r].firstXPos, moves[r].firstYPos, moves[r].secondXPos, moves[r].secondYPos);

                    history.Text += hod[moves[r].firstXPos].ToString() + (moves[r].firstYPos - 1).ToString() + " -> " + hod[moves[r].secondXPos].ToString() + (moves[r].secondYPos - 1).ToString() + "\n";
                    Console.WriteLine("MADE STEP");

                    if (curate - whiteAte == 0 || !checkIfCanEat(moves[r].secondXPos, moves[r].secondYPos))
                    { 
                        Console.WriteLine(curate - whiteAte== 0 ? "MADE STEP without eating" : "");
                        numberOfStep++;
                        player = numberOfStep % 2 != 0 ? "white" : "black";
                        playersname.Text = player + " turn";
                    }
                    else if (curate != whiteAte && checkIfCanEat(moves[0].secondXPos, moves[0].secondYPos))
                    {
                        while (curate != whiteAte && checkIfCanEat(moves[0].secondXPos, moves[0].secondYPos))
                        {
                            curate = whiteAte;
                            bool wp = board[moves[0].secondXPos, moves[0].secondYPos].Image == whitesh.Image;
                            var hodiks = allhodstoEat(moves[0].secondXPos, moves[0].secondYPos);

                            var hodik = sorter(hodiks);

                            for (int i = 0; i < hodik.Count; i++)
                                Console.WriteLine(hodik[i].ocenka + " | " + hodik[i].firstXPos + " | " + hodik[i].firstYPos + " | " + hodik[i].secondXPos + " | " + hodik[i].secondYPos);
                            r = random.Next(hodik.Count);
                            Console.WriteLine("after sorting");
                            makestep(hodik[r].firstXPos, hodik[r].firstYPos, hodik[r].secondXPos, hodik[r].secondYPos);
                            foreach (AICheckers h in hodik)
                                Console.WriteLine(h.ocenka + " | " + h.firstXPos + " ; " + h.firstYPos + " | " + h.secondXPos + " ; " + h.secondYPos);
                            if (hodik[r].secondXPos == 0 & wp) break;
                        }
                        numberOfStep++;
                        player = numberOfStep % 2 != 0 ? "white" : "black";
                        playersname.Text = player + " turn";
                    }
                }


            }
            judje();
        }


        public List<int[]> searchForCheckers()
        {
            List<int[]> checkers = new List<int[]>();
            if (colour == "white")
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (board[i, j].Image == whitesh.Image || board[i, j].Image == whiteQueen.Image)
                            checkers.Add(new int[] { i, j });
            if (colour == "black")
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (board[i, j].Image == blacksh.Image || board[i, j].Image == blackQueen.Image)
                            checkers.Add(new int[] { i, j });
            return checkers;
        }

        public List<AICheckers> sorter(List<AICheckers> list)
        {
            List<AICheckers> newlist = new List<AICheckers>();
            if (list.Count > 1)
            {
                for (int i = 0; i < list.Count; i++)
                    for (int j = 0; j < list.Count - i - 1; j++)
                        if (list[j].ocenka < list[j + 1].ocenka)
                        {
                            AICheckers temp = list[j];
                            list[j] = list[j + 1];
                            list[j + 1] = temp;
                        }
                for (int i = 0; i < list.Count; i++)
                    if (list[i].ocenka == list[0].ocenka)
                        newlist.Add(list[i]);
                for (int i = 0; i < newlist.Count; i++)
                    for (int j = 0; j < newlist.Count - i - 1; j++)
                        if (newlist[j].secondXPos > newlist[j + 1].secondXPos)
                        {
                            AICheckers temp = newlist[j];
                            newlist[j] = newlist[j + 1];
                            newlist[j + 1] = temp;
                        }
                return newlist;
            }
            else
                return list;
        }

        public void makestep(int x1, int y1, int x2, int y2)
        {
            if (colour == "white")
            {
                if (board[x1, y1].Image == whitesh.Image)
                    whiteStepper(x1, y1, x2, y2);
                if (board[x1, y1].Image == whiteQueen.Image)
                    whiteQueenStepper(x1, y1, x2, y2);
            }
            if (colour == "black")
            {
                if (board[x1, y1].Image == blacksh.Image)
                    blackStepper(x1, y1, x2, y2);
                if (board[x1, y1].Image == blackQueen.Image)
                    blackQueenStepper(x1, y1, x2, y2);
            }
            judje();
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

        public List<AICheckers> allhodstoEat(int x, int y)
        {
            List<AICheckers> hods = new List<AICheckers>();
            if (colour == "white")
            {
                if (board[x, y].Image == whitesh.Image)
                {
                    if (x > 1 && y > 1 && x < 7 && y < 7)
                    {
                        if (board[x - 1, y + 1].Image == blacksh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                        if (board[x - 1, y + 1].Image == blackQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });

                        if (board[x - 1, y - 1].Image == blacksh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                        if (board[x - 1, y - 1].Image == blackQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });

                        if (board[x + 1, y + 1].Image == blacksh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        if (board[x + 1, y + 1].Image == blackQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });

                        if (board[x + 1, y - 1].Image == blacksh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                        if (board[x + 1, y - 1].Image == blackQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                    }
                    if (y < 1)
                    {
                        if (x > 6 || x < 1)
                        {
                            if (x > 6)
                            {
                                if (board[x - 1, y - 1].Image == blacksh.Image || board[x - 1, y - 1].Image == blackQueen.Image)
                                    hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x - 1, y - 1) ? -1 : 1) + (board[x - 1, y - 1].Image == blackQueen.Image ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = x - 1, secondYPos = y - 1 });
                            }
                            if (x < 1)
                            {
                                if (board[x + 1, y + 1].Image == blacksh.Image)
                                    if (checkIfCanEat(x, y))
                                        hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                                if (board[x + 1, y + 1].Image == blackQueen.Image)
                                    if (checkIfCanEat(x, y))
                                        hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                            }
                        }
                        else
                        {
                            if (board[x - 1, y + 1].Image == blacksh.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                            if (board[x - 1, y + 1].Image == blackQueen.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                            if (board[x + 1, y + 1].Image == blacksh.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                            if (board[x + 1, y + 1].Image == blackQueen.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        }
                    }
                    if (y > 6)
                    {
                        if (x > 6 || x < 1)
                        {
                            if (x > 6)
                            {
                                if (board[x - 1, y - 1].Image == blacksh.Image || board[x - 1, y - 1].Image == blackQueen.Image)
                                    hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x - 1, y - 1) ? -1 : 1) + (board[x - 1, y - 1].Image == blackQueen.Image ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = x - 1, secondYPos = y - 1 });
                            }
                            if (x < 1)
                            {
                                if (board[x + 1, y - 1].Image == blacksh.Image)
                                    if (checkIfCanEat(x, y))
                                        hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                                if (board[x + 1, y - 1].Image == blackQueen.Image)
                                    if (checkIfCanEat(x, y))
                                        hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                            }
                        }
                        else
                        {
                            if (board[x - 1, y - 1].Image == blacksh.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                            if (board[x - 1, y - 1].Image == blackQueen.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                            if (board[x + 1, y - 1].Image == blacksh.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                            if (board[x + 1, y - 1].Image == blackQueen.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        }
                    }
                }
                if (board[x, y].Image == whiteQueen.Image)
                {
                    int ulx = x, uly = y;
                    while (ulx >= 0 && uly >= 0)
                    {
                        if (board[ulx, uly].Image == null){ }
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
                                    if (board[ulx - 1, uly - 1].Image == null & (board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image))
                                    {
                                        hods.Add(new AICheckers { ocenka = 4 + (checkIfCantDie(x, y, ulx, uly) ? -1 : 1) + (board[ulx, uly].Image == blackQueen.Image ? 1 : 0) + (whiteQueenEatChecker(ulx, uly) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = ulx - 1, secondYPos = uly - 1 });
                                        cleaner();
                                        break;
                                    }
                                }
                                else break;
                            }
                        }
                        ulx--; uly--;
                    }
                    int urx = x, ury = y;
                    while (urx >= 0 && ury <= 7)
                    {
                        if (board[urx, ury].Image == null)
                        {
                        }
                        else
                        {
                            if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image)
                                if (ury != y && urx != x) break;

                            if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image)
                            {
                                if (urx >= 1 && ury <= 6)
                                {
                                    if (board[urx - 1, ury + 1].Image == null & (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image))
                                    {
                                        hods.Add(new AICheckers { ocenka = 4 + (checkIfCantDie(x, y, urx, ury) ? -1 : 1) + (board[urx, ury].Image == blackQueen.Image ? 1 : 0) + (whiteQueenEatChecker(urx, ury) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = urx - 1, secondYPos = ury + 1 });
                                        cleaner();
                                        break;
                                    }
                                }
                                else break;
                            }
                        }
                        urx--; ury++;
                    }

                    int dlx = x, dly = y;
                    while (dlx <= 7 && dly >= 0)
                    {
                        if (board[dlx, dly].Image == null)
                        {
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
                                    if (board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image)
                                    {
                                        if (board[dlx + 1, dly - 1].Image == null)
                                        {
                                            hods.Add(new AICheckers { ocenka = 4 + (checkIfCantDie(x, y, dlx, dly) ? -1 : 1) + (whiteQueenEatChecker(dlx, dly) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = dlx + 1, secondYPos = dly - 1 });
                                            cleaner();
                                            break;
                                        }
                                    }
                                }
                                else break;
                            }
                        }
                        dlx++; dly--;
                    }
                    int drx = x, dry = y;
                    while (drx <= 7 && dry <= 7)
                    {
                        if (board[drx, dry].Image == null)
                        {
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
                                    if (board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image)
                                        if (board[drx + 1, dry + 1].Image == null)
                                        {
                                            hods.Add(new AICheckers { ocenka = 4 + (checkIfCantDie(x, y, drx, dry) ? -1 : 1) + (board[drx, dry].Image == blackQueen.Image ? 1 : 0) + (whiteQueenEatChecker(drx, dry) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = drx + 1, secondYPos = dry + 1 });
                                            break;
                                        }
                                        else break;
                            }
                        }
                        drx++; dry++;
                    }
                }
            }
            if (colour == "black")
            {
                if (board[x, y].Image == blacksh.Image)
                {
                    if (x > 1 && y > 1 && x < 7 && y < 7)
                    {
                        if (board[x + 1, y + 1].Image == whitesh.Image || board[x + 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                            {
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                            }
                        if (board[x + 1, y - 1].Image == whitesh.Image || board[x + 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                            {
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                            }
                        if (board[x - 1, y + 1].Image == whitesh.Image || board[x - 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                            {
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                            }
                        if (board[x - 1, y - 1].Image == whitesh.Image || board[x - 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                            {
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                            }
                    }
                    if (x <= 1 && y > 1 && y < 7)
                    {
                        if (board[x + 1, y + 1].Image == whitesh.Image || board[x + 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                            {
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                            }
                        if (board[x + 1, y - 1].Image == whitesh.Image || board[x + 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                            {
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                            }
                    }
                    if (y <= 1 && x > 1 && x < 7)
                    {
                        if (board[x + 1, y + 1].Image == whitesh.Image || board[x + 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        if (board[x - 1, y + 1].Image == whitesh.Image || board[x - 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                    }

                    if (y >= 6 && x > 1 && x < 7)
                    {
                        if (board[x + 1, y - 1].Image == whitesh.Image || board[x + 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                        if (board[x - 1, y - 1].Image == whitesh.Image || board[x - 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });

                    }
                    if (y <= 1 && x <= 1)
                        if (board[x + 1, y + 1].Image == whitesh.Image || board[x + 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });

                    if (y >= 6 && x <= 1)
                        if (board[x + 1, y - 1].Image == whitesh.Image || board[x + 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                    if (y <= 1 && x >= 6)
                        if (board[x - 1, y + 1].Image == whitesh.Image || board[x - 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                    if (y >= 6 && x >= 6)
                        if (board[x - 1, y - 1].Image == whitesh.Image || board[x - 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                }
                if (board[x, y].Image == blackQueen.Image)
                {
                    int ulx = x, uly = y;
                    int ulanswer = 0;
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
                                    if (board[ulx - 1, uly - 1].Image == null & (board[ulx, uly].Image == whiteQueen.Image || board[ulx, uly].Image == whitesh.Image))
                                    {
                                        hods.Add(new AICheckers { ocenka = ulanswer + 4 + (blackQueenEatChecker(ulx, uly) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = ulx - 1, secondYPos = uly - 1 });
                                        cleaner();
                                        break;
                                    }
                                    else break;
                                }
                            }
                        }
                        ulx--; uly--;
                    }
                    int urx = x, ury = y;
                    int uranswer = 0;
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
                                    if (board[urx - 1, ury + 1].Image == null & (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image))
                                    {
                                        hods.Add(new AICheckers { ocenka = uranswer + 4 + (blackQueenEatChecker(urx, ury) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = urx - 1, secondYPos = ury + 1 });
                                        cleaner();
                                        break;
                                    }
                                    else break;
                                }
                            }
                        }
                        urx--; ury++;
                    }

                    int dlx = x, dly = y;
                    int dlanswer = 0;
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
                                if (dlx <= 6 && dly >= 1 & (board[dlx, dly].Image == whiteQueen.Image || board[dlx, dly].Image == whitesh.Image))
                                {
                                    if (board[dlx + 1, dly - 1].Image == null)
                                    {
                                        hods.Add(new AICheckers { ocenka = dlanswer + 4 + (blackQueenEatChecker(dlx, dly) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = dlx + 1, secondYPos = dly - 1 });
                                        cleaner();
                                        break;
                                    }
                                    else break;
                                }
                            }
                        }
                        dlx++; dly--;
                    }
                    int drx = x, dry = y;
                    int dranswer = 0;
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
                                if (drx <= 6 && dry <= 6 & (board[drx, dry].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image))
                                {
                                    if (board[drx + 1, dry + 1].Image == null)
                                    {
                                        hods.Add(new AICheckers { ocenka = dranswer + 4 + (blackQueenEatChecker(drx, dry) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = drx + 1, secondYPos = dry + 1 });
                                        cleaner();
                                        break;
                                    }
                                    else break;
                                }
                            }
                        }
                        drx++; dry++;
                    }
                }
            }
            for (int i = 0; i < hods.Count; i++)
            {
                var h = hods[i];
                if ((h.secondXPos > 7 || h.secondXPos < 0 || h.secondYPos > 7 || h.secondYPos < 0) || (board[h.secondXPos, h.secondYPos].Image == whitesh.Image || board[h.secondXPos, h.secondYPos].Image == whiteQueen.Image) || (board[h.secondXPos, h.secondYPos].Image == blacksh.Image || board[h.secondXPos, h.secondYPos].Image == blackQueen.Image))
                    h.ocenka = -1;
                if (h.secondXPos == 7 & board[h.firstXPos, h.firstYPos].Image == blacksh.Image)
                    h.ocenka++;
                hods[i] = h;
            }
            return hods;
        }

        public List<AICheckers> allhods(int x, int y)
        {
            List<AICheckers> hods = new List<AICheckers>();
            if (colour == "white")
            {
                // мб забить все 32 клетки??
                if (board[x, y].Image == whitesh.Image)
                {
                    if (x > 1 && y > 1 && x < 7 && y < 7)
                    {
                        if (board[x - 1, y + 1].Image == null)
                            hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x - 1, y + 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x - 1, secondYPos = y + 1 });
                        if (board[x - 1, y - 1].Image == null)
                            hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x - 1, y - 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x - 1, secondYPos = y - 1 });

                        if (board[x - 1, y + 1].Image == blacksh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                        if (board[x - 1, y + 1].Image == blackQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });

                        if (board[x - 1, y - 1].Image == blacksh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                        if (board[x - 1, y - 1].Image == blackQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });

                        if (board[x + 1, y + 1].Image == blacksh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        if (board[x + 1, y + 1].Image == blackQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });

                        if (board[x + 1, y - 1].Image == blacksh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                        if (board[x + 1, y - 1].Image == blackQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                    }
                    if (y < 1)
                    {
                        if (board[x - 1, y + 1].Image == null)
                            hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x - 1, y + 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x - 1, secondYPos = y + 1 });
                    if (x > 6 || x < 1)
                        { 
                            if (x > 6)
                            {
                                if (board[x - 1, y + 1].Image == blacksh.Image || board[x - 1, y + 1].Image == blackQueen.Image)
                                    if (checkIfCanEat(x, y))
                                        hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x - 2, y + 2) ? -1 : 1) + (board[x - 1, y + 1].Image == blackQueen.Image ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                            }
                            if (x < 1)
                            {
                                if (board[x + 1, y + 1].Image == blacksh.Image)
                                    if (checkIfCanEat(x, y))
                                        hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                                if (board[x + 1, y + 1].Image == blackQueen.Image)
                                    if (checkIfCanEat(x, y))
                                        hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                            }
                        }
                    else
                    {
                        if (board[x - 1, y + 1].Image == blacksh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                        if (board[x - 1, y + 1].Image == blackQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                        if (board[x + 1, y + 1].Image == blacksh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        if (board[x + 1, y + 1].Image == blackQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                    }
                    }
                    if (y > 6)
                    {
                        if (board[x - 1, y - 1].Image == null)
                            hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x - 1, y - 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x - 1, secondYPos = y - 1 });
                        if (x > 6 || x < 1)
                        {
                            if (x > 6)
                            {
                                if (board[x - 1, y - 1].Image == blacksh.Image || board[x - 1, y - 1].Image == blackQueen.Image)
                                    if (checkIfCanEat(x, y))
                                        hods.Add(new AICheckers { ocenka = 2 + (checkIfCantDie(x, y, x - 2, y - 2) ? -1 : 1) + (board[x - 1, y - 1].Image == blackQueen.Image ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                            }
                            if (x < 1)
                            {
                                if (board[x + 1, y - 1].Image == blacksh.Image)
                                    if (checkIfCanEat(x, y))
                                        hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                                if (board[x + 1, y - 1].Image == blackQueen.Image)
                                    if (checkIfCanEat(x, y))
                                        hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                            }
                        }
                        else
                        {
                            if (board[x - 1, y - 1].Image == blacksh.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                            if (board[x - 1, y - 1].Image == blackQueen.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                            if (board[x + 1, y - 1].Image == blacksh.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                            if (board[x + 1, y - 1].Image == blackQueen.Image)
                                if (checkIfCanEat(x, y))
                                    hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        }
                    }
                }
                if (board[x,y].Image == whiteQueen.Image)
                {
                    int ulx = x, uly = y;
                    while (ulx >= 0 && uly >= 0)
                    {
                        if (board[ulx, uly].Image == null)
                        {
                            hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, ulx, uly) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = ulx, secondYPos = uly });
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
                                    if (board[ulx - 1, uly - 1].Image == null & (board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image))
                                    {
                                        hods.Add(new AICheckers { ocenka = 4 + (checkIfCantDie(x,y, ulx, uly) ? -1 : 1) + (board[ulx, uly].Image == blackQueen.Image ? 1 : 0) + (whiteQueenEatChecker(ulx, uly) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = ulx - 1, secondYPos = uly - 1 });
                                        cleaner();
                                        break;
                                    }
                                }
                                else break;
                            }
                        }
                        ulx--; uly--;
                    }
                    int urx = x, ury = y;
                    while (urx >= 0 && ury <= 7)
                    {
                        if (board[urx, ury].Image == null)
                        {
                            hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, urx, ury) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = urx, secondYPos = ury });
                        }
                        else
                        {
                            if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image)
                                if (ury != y && urx != x) break;

                            if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image)
                            {
                                if (urx >= 1 && ury <= 6)
                                {
                                    if (board[urx - 1, ury + 1].Image == null & (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image))
                                    {
                                        hods.Add(new AICheckers { ocenka = 4 + (checkIfCantDie(x,y,urx,ury) ? -1 : 1) + (board[urx, ury].Image == blackQueen.Image ? 1 : 0) + (whiteQueenEatChecker(urx, ury) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = urx - 1, secondYPos = ury + 1 });
                                        cleaner();
                                        break;
                                    }
                                }
                                else break;
                            }
                        }
                        urx--; ury++;
                    }

                    int dlx = x, dly = y;
                    while (dlx <= 7 && dly >= 0)
                    {
                        if (board[dlx, dly].Image == null)
                        {
                            hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x,y,dlx,dly) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = dlx, secondYPos = dly });
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
                                    if (board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image)
                                    {
                                        if (board[dlx + 1, dly - 1].Image == null)
                                        {
                                            hods.Add(new AICheckers { ocenka = 4 + (checkIfCantDie(x,y,dlx,dly) ? -1 : 1) + (whiteQueenEatChecker(dlx, dly) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = dlx + 1, secondYPos = dly - 1 });
                                            cleaner();
                                            break;
                                        }
                                    }
                                }
                                else break;
                            }
                        }
                        dlx++; dly--;
                    }
                    int drx = x, dry = y;
                    while (drx <= 7 && dry <= 7)
                    {
                        if (board[drx, dry].Image == null)
                        {
                            hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, drx, dry) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = drx, secondYPos = dry });
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
                                    if (board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image)
                                        if (board[drx + 1, dry + 1].Image == null)
                                        {
                                            hods.Add(new AICheckers { ocenka = 4 + (checkIfCantDie(x,y,drx, dry) ? -1 : 1) + (board[drx, dry].Image == blackQueen.Image ? 1 : 0) + (whiteQueenEatChecker(drx, dry) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = drx + 1, secondYPos = dry + 1 });
                                            break;
                                        }
                                        else break;
                            }
                        }
                        drx++; dry++;
                    }
                }
            }
            if (colour == "black")
            {
                if (board[x, y].Image == blacksh.Image)
                {
                    if (x > 1 && y > 1 && x < 7 && y < 7)
                    {
                        if (board[x + 1, y + 1].Image == null)
                            hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x + 1, y + 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y + 1 });
                        if (board[x + 1, y - 1].Image == null)
                            hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x + 1, y - 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y - 1 });

                        if (board[x + 1, y + 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        if (board[x + 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });

                        if (board[x + 1, y - 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                        if (board[x + 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });

                        if (board[x - 1, y + 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                        if (board[x - 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });

                        if (board[x - 1, y - 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                        if (board[x - 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                    }
                    if (x == 1 && y == 1)
                    {
                        if (board[x + 1, y + 1].Image == null) hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x + 1, y + 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y + 1 });
                        if (board[x + 1, y - 1].Image == null) hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x + 1, y - 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y - 1 });
                        if (board[x + 1, y + 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        if (board[x + 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                    }
                    if (x <= 1 && y > 1 && y < 7)
                    {
                        if (board[x + 1, y + 1].Image == null) hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x + 1, y + 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y + 1 });
                        if (board[x + 1, y - 1].Image == null) hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x + 1, y - 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y - 1 });
                        if (board[x + 1, y + 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        if (board[x + 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });

                        if (board[x + 1, y - 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                        if (board[x + 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                    }
                    if (y <= 1 && x > 1 && x < 7)
                    {
                        if (board[x + 1, y + 1].Image == null) hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x + 1, y + 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y + 1 });
                        if (board[x + 1, y + 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        if (board[x + 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });

                        if (board[x - 1, y + 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                        if (board[x - 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y + 2 });
                    }

                    if (y >= 6 && x >= 1 && x <= 7)
                    {
                        if (board[x + 1, y - 1].Image == null) hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x , y , x+1, y-1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y - 1 });
                        if (board[x + 1, y - 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                        if (board[x + 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });

                        if (board[x - 1, y - 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                        if (board[x - 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                    }
                    if (y <= 1 && x <= 1)
                    {
                        if (board[x + 1, y + 1].Image == null) hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x + 1, y + 1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y + 1 });
                        if (board[x + 1, y + 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                        if (board[x + 1, y + 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y + 2 });
                    }

                    if (y >= 6 && x <= 1)
                    {
                        if (board[x + 1, y - 1].Image == null) hods.Add(new AICheckers { ocenka = 1 + (checkIfCantDie(x, y, x+1, y-1) ? -1 : 1), firstXPos = x, firstYPos = y, secondXPos = x + 1, secondYPos = y - 1 });
                        if (board[x + 1, y - 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                        if (board[x + 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x + 2, secondYPos = y - 2 });
                    }
                    if (y <= 1 && x >= 6)
                    {
                        if (board[x - 1, y - 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                        if (board[x - 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                    }
                    if (y >= 6 && x >= 6)
                    {
                        if (board[x - 1, y - 1].Image == whitesh.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 2 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                        if (board[x - 1, y - 1].Image == whiteQueen.Image)
                            if (checkIfCanEat(x, y))
                                hods.Add(new AICheckers { ocenka = 3 + (checkIfCanEatAndCantDie(x, y)), firstXPos = x, firstYPos = y, secondXPos = x - 2, secondYPos = y - 2 });
                    }
                }
                if (board[x, y].Image == blackQueen.Image)
                {
                    int ulx = x, uly = y;
                    int ulanswer = 0;
                    while (ulx >= 0 && uly >= 0)
                    {
                        if (board[ulx, uly].Image == null)
                        {
                            hods.Add(new AICheckers { ocenka = ulanswer + 2, firstXPos = x, firstYPos = y, secondXPos = ulx, secondYPos = uly });
                        }
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
                                    if (board[ulx - 1, uly - 1].Image == null & (board[ulx, uly].Image == whiteQueen.Image || board[ulx, uly].Image == whitesh.Image))
                                    {
                                        hods.Add(new AICheckers { ocenka = ulanswer + 4 + (board[ulx, uly].Image == whiteQueen.Image ? 1 : 0) + (blackQueenEatChecker(ulx, uly) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = ulx - 1, secondYPos = uly - 1 });
                                        cleaner();
                                        break;
                                    }
                                }
                                else break;
                            }
                        }
                        ulx--; uly--;
                    }
                    int urx = x, ury = y;
                    int uranswer = 0;
                    while (urx >= 0 && ury <= 7)
                    {
                        if (board[urx, ury].Image == null)
                        {
                            hods.Add(new AICheckers { ocenka = uranswer + 2, firstXPos = x, firstYPos = y, secondXPos = urx, secondYPos = ury });
                        }
                        else
                        {
                            if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image)
                                if (ury != y && urx != x) break;

                            if (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image)
                            {
                                if (urx >= 1 && ury <= 6)
                                {
                                    if (board[urx - 1, ury + 1].Image == null & (board[urx, ury].Image == whiteQueen.Image || board[urx, ury].Image == whitesh.Image))
                                    {
                                        hods.Add(new AICheckers { ocenka = uranswer + 4 + (board[urx, ury].Image == whiteQueen.Image ? 1 : 0) + (blackQueenEatChecker(urx, ury) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = urx - 1, secondYPos = ury + 1 });
                                        cleaner();
                                        break;
                                    }
                                }
                                else break;
                            }
                        }
                        urx--; ury++;
                    }

                    int dlx = x, dly = y;
                    int dlanswer = 0;
                    while (dlx <= 7 && dly >= 0)
                    {
                        if (board[dlx, dly].Image == null)
                        {
                            hods.Add(new AICheckers { ocenka = dlanswer + 2, firstXPos = x, firstYPos = y, secondXPos = dlx, secondYPos = dly });
                        }
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
                                    if (board[dlx, dly].Image == whiteQueen.Image || board[dlx, dly].Image == whitesh.Image)
                                    {
                                        if (board[dlx + 1, dly - 1].Image == null)
                                        {
                                            hods.Add(new AICheckers { ocenka = dlanswer + 4 + (blackQueenEatChecker(dlx, dly) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = dlx + 1, secondYPos = dly - 1 });
                                            cleaner();
                                            break;
                                        }
                                    }
                                }
                                else break;
                            }
                        }
                        dlx++; dly--;
                    }
                    int drx = x, dry = y;
                    int dranswer = 0;
                    while (drx <= 7 && dry <= 7)
                    {
                        if (board[drx, dry].Image == null)
                        {
                            hods.Add(new AICheckers { ocenka = dranswer + 2, firstXPos = x, firstYPos = y, secondXPos = drx, secondYPos = dry });
                        }
                        else
                        {
                            if (board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image)
                            {
                                if (dry != y && drx != x) break;
                            }
                            if (board[drx, dry].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image)
                            {
                                if (drx <= 6 && dry <= 6)
                                    if (board[drx, dry].Image == whiteQueen.Image || board[drx, dry].Image == whitesh.Image)
                                        if (board[drx + 1, dry + 1].Image == null)
                                        {
                                            hods.Add(new AICheckers { ocenka = dranswer + 4 + (board[drx, dry].Image == whiteQueen.Image ? 1 : 0) + (blackQueenEatChecker(drx, dry) ? 1 : 0), firstXPos = x, firstYPos = y, secondXPos = drx + 1, secondYPos = dry + 1 });
                                            break;
                                        }
                                        else break;
                            }
                        }
                        drx++; dry++;
                    }
                }
            }
            for (int i = 0; i < hods.Count; i++)
            {
                var h = hods[i];
                if ((h.secondXPos > 7 || h.secondXPos < 0 || h.secondYPos > 7 || h.secondYPos < 0) || (board[h.secondXPos, h.secondYPos].Image == whitesh.Image || board[h.secondXPos, h.secondYPos].Image == whiteQueen.Image) || (board[h.secondXPos, h.secondYPos].Image == blacksh.Image || board[h.secondXPos, h.secondYPos].Image == blackQueen.Image))
                    h.ocenka = -1;
                if (h.secondXPos == 7 && board[h.firstXPos, h.firstYPos].Image == blacksh.Image)
                    h.ocenka++;
                if (h.secondXPos == 0 && board[h.firstXPos, h.firstYPos].Image == whitesh.Image)
                    h.ocenka++;
                hods[i] = h;
            }
            return hods;
        }

        public bool checkIfCanEat(int x, int y)
        {
            bool answer = false;
            if (x > 1 && x < 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                    answer = true;
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                    answer = true;
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                    answer = true;
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                    answer = true;
            }
            if (x > 1 && x < 6 && y <= 1)
            {
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                    answer = true;
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                    answer = true;
            }
            if (x > 1 && x < 6 && y >= 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                    answer = true;
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                    answer = true;
            }
            if (x <= 1 && y > 1 && y < 6)
            {
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                    answer = true;
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                    answer = true;
            }
            if (x >= 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                    answer = true;
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                    answer = true;
            }
            return answer;
        }
        public bool checkIfCantDie(int x1, int y1, int x, int y)
        {
            if (board[x, y].Image == null)
            {
                var temp = board[x1, y1].Image;
                board[x, y].Image = temp;
                board[x1, y1].Image = null;
                bool death = false;
                if (colour == "white")
                {
                    if (x >= 1 && x <= 6 && y >= 1 && y <= 6)
                    {
                        if ((board[x - 1, y - 1].Image == blacksh.Image) && board[x + 1, y + 1].Image == null)
                            death = true;
                        if ((board[x - 1, y + 1].Image == blacksh.Image) && board[x + 1, y - 1].Image == null)
                            death = true;
                        if ((board[x + 1, y - 1].Image == blacksh.Image) && board[x - 1, y + 1].Image == null)
                            death = true;
                        if ((board[x + 1, y + 1].Image == blacksh.Image) && board[x - 1, y - 1].Image == null)
                            death = true;
                        int ulx = x, uly = y;
                        while (ulx >= 0 && uly >= 0)
                        {
                            if (board[ulx, uly].Image == null) { }
                            else
                            {
                                if (board[ulx, uly].Image == whitesh.Image || board[ulx, uly].Image == whiteQueen.Image)
                                    if (uly != y && ulx != x) break;
                                if (board[ulx, uly].Image == blackQueen.Image)
                                    if (ulx >= 1 && uly >= 1)
                                    {
                                        if (board[x + 1, y + 1].Image == null)
                                        {
                                            death = true;
                                            break;
                                        }
                                        else break;
                                    }
                            }
                            ulx--; uly--;
                        }

                        int urx = x, ury = y;
                        while (urx >= 0 && ury <= 7)
                        {
                            if (board[urx, ury].Image == null) { }
                            else
                            {
                                if (board[urx, ury].Image == whitesh.Image || board[urx, ury].Image == whiteQueen.Image)
                                    if (ury != y && urx != x) break;
                                if (board[urx, ury].Image == blackQueen.Image)
                                    if (urx >= 1 && ury <= 6)
                                    {
                                        if (board[x + 1, y - 1].Image == null)
                                        {
                                            death = true;
                                            break;
                                        }
                                        else break;
                                    }
                            }
                            urx--; ury++;
                        }

                        int dlx = x, dly = y;
                        while (dlx <= 7 && dly >= 0)
                        {
                            if (board[dlx, dly].Image == null) { }
                            else
                            {
                                if (board[dlx, dly].Image == whitesh.Image || board[dlx, dly].Image == whiteQueen.Image)
                                    if (dly != y && dlx != x) break;
                                if (board[dlx, dly].Image == blackQueen.Image)
                                    if (dlx <= 6 && dly >= 1)
                                    {
                                        if (board[x - 1, y + 1].Image == null)
                                        {
                                            death = true;
                                            break;
                                        }
                                        else break;
                                    }
                            }
                            dlx++; dly--;
                        }

                        int drx = x, dry = y;
                        while (drx <= 7 && dry <= 7)
                        {
                            if (board[drx, dry].Image == null) { }
                            else
                            {
                                if (board[drx, dry].Image == whitesh.Image || board[drx, dry].Image == whiteQueen.Image)
                                {
                                    if (dry != y && drx != x) break;
                                }
                                if (board[drx, dry].Image == blackQueen.Image)
                                    if (drx <= 6 && dry <= 6)
                                    {
                                        if (board[x - 1, y - 1].Image == null)
                                        {
                                            death = true;
                                            break;
                                        }
                                        else break;
                                    }
                            }
                            drx++; dry++;
                        }
                    }
                    else death = false;
                }
                if (colour == "black")
                {
                    if (x >= 1 && x <= 6 && y >= 1 && y <= 6)
                    {
                        if ((board[x - 1, y - 1].Image == whiteQueen.Image | board[x - 1, y - 1].Image == whitesh.Image) && board[x + 1, y + 1].Image == null)
                            death = true;
                        if ((board[x - 1, y + 1].Image == whiteQueen.Image | board[x - 1, y + 1].Image == whitesh.Image) && board[x + 1, y - 1].Image == null)
                            death = true;
                        if ((board[x + 1, y - 1].Image == whiteQueen.Image | board[x + 1, y - 1].Image == whitesh.Image) && board[x - 1, y + 1].Image == null)
                            death = true;
                        if ((board[x + 1, y + 1].Image == whiteQueen.Image | board[x + 1, y + 1].Image == whitesh.Image) && board[x - 1, y - 1].Image == null)
                            death = true;

                        int ulx = x, uly = y;
                        while (ulx >= 0 && uly >= 0)
                        {
                            if (board[ulx, uly].Image == null) { }
                            else
                            {
                                if (board[ulx, uly].Image == blackQueen.Image || board[ulx, uly].Image == blacksh.Image)
                                {
                                    if (uly != y && ulx != x) break;
                                }

                                if (board[ulx, uly].Image == whiteQueen.Image)
                                    if (ulx >= 1 && uly >= 1)
                                    {
                                        if (board[x + 1, y + 1].Image == null)
                                        {
                                            death = true;
                                            break;
                                        }
                                        else break;
                                    }
                            }
                            ulx--; uly--;
                        }
                        int urx = x, ury = y;
                        while (urx >= 0 && ury <= 7)
                        {
                            if (board[urx, ury].Image == null) { }
                            else
                            {
                                if (board[urx, ury].Image == blackQueen.Image || board[urx, ury].Image == blacksh.Image)
                                    if (ury != y && urx != x) break;
                                if (board[urx, ury].Image == whiteQueen.Image)
                                    if (urx >= 1 && ury <= 6)
                                    {
                                        if (board[x + 1, y - 1].Image == null)
                                        {
                                            death = true;
                                            break;
                                        }
                                        else break;
                                    }
                            }
                            urx--; ury++;
                        }

                        int dlx = x, dly = y;
                        while (dlx <= 7 && dly >= 0)
                        {
                            if (board[dlx, dly].Image == null) { }
                            else
                            {
                                if (board[dlx, dly].Image == blackQueen.Image || board[dlx, dly].Image == blacksh.Image)
                                    if (dly != y && dlx != x) break;
                                if (board[dlx, dly].Image == whiteQueen.Image)
                                    if (dlx <= 6 && dly >= 1)
                                    {
                                        if (board[x - 1, y + 1].Image == null)
                                        {
                                            death = true;
                                            break;
                                        }
                                        else break;
                                    }
                            }
                            dlx++; dly--;
                        }
                        int drx = x, dry = y;
                        while (drx <= 7 && dry <= 7)
                        {
                            if (board[drx, dry].Image == null) { }
                            else
                            {
                                if (board[drx, dry].Image == blackQueen.Image || board[drx, dry].Image == blacksh.Image)
                                    if (dry != y && drx != x) break;
                                if (board[drx, dry].Image == whiteQueen.Image)
                                    if (drx <= 6 && dry <= 6)
                                    {
                                        if (board[x - 1, y - 1].Image == null)
                                        {
                                            death = true;
                                            break;
                                        }
                                        else break;
                                    }
                            }
                            drx++; dry++;
                        }
                    }
                    else death = false;
                }

                temp = board[x, y].Image;
                board[x1, y1].Image = temp;
                board[x, y].Image = null;

                return death;
            }
            else return false;
        }

        
        public int checkIfCanEatAndCantDie(int x, int y)
        {
            int answer = 0;
            if (x > 1 && x < 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x - 2, y - 2))
                        answer = 2;
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x - 2, y + 2))
                        answer = 2;
                }

                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x + 2, y - 2))
                        answer = 2;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x + 2, y + 2))
                        answer = 2;
                }
            }
            if (x > 1 && x < 6 && y <= 1)
            {
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x - 2, y + 2))
                        answer = 2;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x + 2, y + 2))
                        answer = 2;
                }
            }
            if (x > 1 && x < 6 && y >= 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x - 2, y - 2))
                        answer = 2;
                }
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x + 2, y - 2))
                        answer = 2;
                }
            }
            if (x <= 1 && y > 1 && y < 6)
            {
                if ((board[x + 1, y - 1].Image == whitesh.Image | board[x + 1, y - 1].Image == whiteQueen.Image) && board[x + 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x + 2, y - 2))
                        answer = 2;
                }
                if ((board[x + 1, y + 1].Image == whitesh.Image | board[x + 1, y + 1].Image == whiteQueen.Image) && board[x + 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x + 2, y + 2))
                        answer = 2;
                }
            }
            if (x >= 6 && y > 1 && y < 6)
            {
                if ((board[x - 1, y - 1].Image == whitesh.Image | board[x - 1, y - 1].Image == whiteQueen.Image) && board[x - 2, y - 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x - 2, y - 2))
                        answer = 2;
                }
                if ((board[x - 1, y + 1].Image == whitesh.Image | board[x - 1, y + 1].Image == whiteQueen.Image) && board[x - 2, y + 2].Image == null)
                {
                    answer = 1;
                    if (checkIfCantDie(x, y, x - 2, y + 2))
                        answer = 2;
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
                    board[x - 1, y - 1].BackgroundImage = highlight.Image;
                if (board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image)
                    whiteEater(x, y);
                if (board[x - 1, y + 1].Image == null)
                    board[x - 1, y + 1].BackgroundImage = highlight.Image;
                if (board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image)
                    whiteEater(x, y);
                if (board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blackQueen.Image)
                    whiteEater(x, y);
                if (board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image)
                    whiteEater(x, y);
            }
            if ((y >= 6 && x <= 1) && (board[x - 1, y - 1].Image == blacksh.Image || board[x - 1, y - 1].Image == blackQueen.Image))
                whiteEater(x, y);
            if ((y <= 1 && x <= 6) && (board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blackQueen.Image))
                whiteEater(x, y);
            if (y <= 1 && x >= 1 && board[x - 1, y + 1].Image == null)
                board[x - 1, y + 1].BackgroundImage = highlight.Image;
            if (y <= 1 & x <= 1)
                if ((board[x + 1, y + 1].Image == blacksh.Image | board[x + 1, y + 1].Image == blacksh.Image) & board[x + 2, y + 2].Image == null)
                    board[x + 2, y + 2].BackgroundImage = highlight.Image;
            if ((y <= 1 && x > 1) && (board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image))
                whiteEater(x, y);
            if ((y >= 6 && x <= 6) && (board[x + 1, y - 1].Image == blacksh.Image | board[x + 1, y - 1].Image == blackQueen.Image))
                whiteEater(x, y);
            if (y >= 6 && x >= 1 && board[x - 1, y - 1].Image == null)
                board[x - 1, y - 1].BackgroundImage = highlight.Image;
            if ((y >= 6 && x > 1) && (board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image))
                whiteEater(x, y);
            if (y >= 1 && y <= 6 && x >= 6)
            {
                if (board[x - 1, y - 1].Image == null)
                    board[x - 1, y - 1].BackgroundImage = highlight.Image;
                if ((board[x - 1, y - 1].Image == blacksh.Image | board[x - 1, y - 1].Image == blackQueen.Image) && y != 1)
                    whiteEater(x, y);
                if (board[x - 1, y + 1].Image == null)
                    board[x - 1, y + 1].BackgroundImage = highlight.Image;
                if ((board[x - 1, y + 1].Image == blacksh.Image | board[x - 1, y + 1].Image == blackQueen.Image) && y != 6)
                    whiteEater(x, y);
            }
        }

        public void whiteEater(int x, int y)
        {
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
            if (xk == 7)
            {
                board[xk, yk].Image = blackQueen.Image;
            }
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
                for (int j = 0; j < 8; j++)
                    board[i, j].BackgroundImage = board[i, j].BackgroundImage == highlight.Image ? blacksh.BackgroundImage : board[i, j].BackgroundImage;
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
                for (int j = 0; j < 8; j++)
                    board[i, j].Image = null;

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0) { }
                        else
                        {
                            if (i <= 2)
                                board[i, j].Image = blacksh.Image;
                            if (i > 4)
                                board[i, j].Image = whitesh.Image;
                        }
                    }
                    else
                    {
                        if (j % 2 != 0) { }
                        else
                        {
                            if (i <= 2)
                                board[i, j].Image = blacksh.Image;
                            if (i > 4)
                                board[i, j].Image = whitesh.Image;
                        }
                    }
                }
        }

        private void startNewGame_Click(object sender, EventArgs e)
        {
            newGame();
            timer1.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            colour = "black";
            connection();
        }

        public string convertBoard()
        {
            string pole = "";
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Image == whitesh.Image)
                        pole += '1';
                    if (board[i, j].Image == whiteQueen.Image)
                        pole += '3';
                    if (board[i, j].Image == blacksh.Image)
                        pole += '2';
                    if (board[i, j].Image == blackQueen.Image)
                        pole += '4';
                    if (board[i, j].Image == null)
                        pole += '0';
                }
            string pole2 = rs(pole);
            pole2 += "\r\n\r\n";
            return pole2;
        }

        public void convertToBoard(string pole1)
        {
            string pole = rs(pole1);
            history.Text += pole + "\n";
            if (pole1 != "")
                for (int i = 0; i < pole.Length; i++)
                    board[i / 8, i % 8].Image = pole[i] == '1' ? whitesh.Image : pole[i] == '2' ? blacksh.Image : pole[i] == '3' ? whiteQueen.Image : pole[i] == '4' ? blackQueen.Image : null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            net();
        }

        public void net()
        {
            try
            {
                client = new TcpClient(ip.Text, 8005);
            }
            catch (SocketException s)
            {
                history.Text += "Error with connection\n" + s.Message;
                net();
            }

            try
            {
                if (client.Connected)
                {
                    history.Text += "connected\n";

                    NetworkStream stream = client.GetStream();

                    string colur = colour+"\r\n\r\n";
                    stream.Write(Encoding.ASCII.GetBytes(colur), 0, Encoding.ASCII.GetBytes(colur).Length);

                    string response = "";
                    byte[] d = new byte[1024];
                    Int32 counter;
                    stream.Flush();
                    counter = client.GetStream().Read(d, 0, d.Length);
                    response = Encoding.ASCII.GetString(d);
                    history.Text += "response\n" + response + "\n;";

                    if (response == "BAD\r\n\r\n")
                    {
                        Console.WriteLine("end");
                        stream.Flush();
                        stream.Close();
                        client.Close();
                        return;
                    }

                    response = response.Substring(0, 64);
                    convertToBoard(response);
                    string data = convertBoard();
                    stream.Flush();
                    analyser();
                    data = convertBoard();
                    stream.Write(Encoding.ASCII.GetBytes(data), 0, Encoding.ASCII.GetBytes(data).Length);
                    stream.Flush();
                    response = "";

                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception c)
            {
                Console.WriteLine("косяк\n" + c.Message + "\n");
                history.Text += "косяк\n" + c.Message + "\n";
            }
            finally { history.Text += "\ntick\n"; }
        }
        public static string rs(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colour = "white";
            connection();
        }
    }
}