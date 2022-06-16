using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace match_4
{
    public partial class Form1 : Form
    {
        public Button[][] buttons = new Button[7][];
        public enum Player
        {
            red,
            yellow
        }
        public Player player = Player.red;
        public bool winner = false;
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 7; i++)
            {
                Button[] buttonscol = new Button[6];
                for (int j = 0; j < 6; j++)
                {
                    Button newbutton = new Button();
                    newbutton.Location = new System.Drawing.Point(85 * i, 85 * j);
                    newbutton.Name = $"newbtn{i}{j}";
                    newbutton.Size = new System.Drawing.Size(85, 85);
                    newbutton.TabIndex = i + j;
                    newbutton.Text = String.Empty;
                    newbutton.UseVisualStyleBackColor = true;
                    newbutton.BackColor = Color.White;
                    newbutton.Click += new EventHandler(BoardClick);
                    buttonscol[j] = newbutton;
                    gbGame.Controls.Add(newbutton);
                }
                buttons[i] = buttonscol;
            }

        }
        private void BoardClick(object sender, EventArgs e)
        {
            if (winner == false)
            {
                Button clicked = sender as Button;
                int col = 0;
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (buttons[i][j] == clicked) col = i;
                    }
                }
                for (int i = 5; i >= 0; i--)
                {
                    if (buttons[col][i].BackColor == Color.White)
                    {
                        if (player == Player.red)
                        {
                            buttons[col][i].BackColor = Color.Red;
                            if (GetWinner())
                            {
                                winner = true;
                                lblWinner.Text = "Red wins";
                            }
                            player = Player.yellow;
                            break;
                        }
                        if (player == Player.yellow)
                        {
                            buttons[col][i].BackColor = Color.Yellow;
                            if (GetWinner())
                            {
                                winner = true;
                                lblWinner.Text = "Yellow wins";
                            }
                            player = Player.red;
                            break;
                        }
                    }
                }
            }
        }
        public bool GetWinner()
        {
            int win ;
            //checking for winners vertically
            for (int i = 0; i < 7; i++)
            {
                win = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (buttons[i][j].BackColor == buttons[i][j + 1].BackColor && buttons[i][j].BackColor != Color.White)
                    {
                        win++;
                        if (win == 3)
                        {
                            return true;
                        }
                    }
                    else win = 0;
                }
            }
            //checking for winners horizontally
            for (int i = 0; i < 6; i++)
            {
                win = 0;
                for (int j = 0; j < 6; j++)
                {
                    if (buttons[j][i].BackColor == buttons[j + 1][i].BackColor && buttons[j][i].BackColor != Color.White)
                    {
                        win++;
                        if (win == 3)
                        {
                            return true;
                        }
                    }
                    else win = 0;
                }
            }
            //checking for winners on descending diagonals that touch the left wall
            for (int i = 2; i>=0;i--)
            {
                win = 0;
                int k = i;
                for (int j = 0; j<6-i-1;j++)
                {
                        if (buttons[j][k].BackColor == buttons[j + 1][k + 1].BackColor && buttons[j][k].BackColor != Color.White)
                        {
                            win++;
                            if (win == 3) return true;
                        }
                        else win = 0;
                    k++;
                }
            }
            // checking for winners on descending diagonals that do not touch the left wall
            for (int i=1; i<4;i++)
            {
                win = 0;
                int k = i;
                for(int j = 0; j<6-i;j++)
                {
                    if (buttons[k][j].BackColor == buttons[k+ 1][j + 1].BackColor && buttons[k][j].BackColor != Color.White)
                    {
                        win++;
                        if (win == 3) return true;
                    }
                    else win = 0;
                    k++;
                }
            }
            //checking for winners on ascending diagonals that touch the left wall
            for (int i=3;i<6;i++)
            {
                win = 0;
                int k = i;
                for (int j=0; j<i;j++)
                {
                    if (buttons[j][k].BackColor == buttons[j+1][k-1].BackColor && buttons[j][k].BackColor != Color.White)
                    {
                        win++;
                        if (win == 3) return true;
                    }
                    else win = 0;
                    k--;
                }
            }
            // checking for winners on ascending diagonals that do not touch the left wall
            for (int i=1;i<4;i++)
            {
                win = 0;
                int k = i;
                for(int j = 5; j>=i;j--)
                {
                    if (buttons[k][j].BackColor == buttons[k + 1][j - 1].BackColor && buttons[k][j].BackColor != Color.White)
                    {
                        win++;
                        if (win == 3) return true;
                    }
                    else win = 0;
                    k++;
                }
            }
            return false;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    buttons[i][j].BackColor = Color.White;
                }
            }
            player = Player.red;
            winner = false;
            lblWinner.Text = String.Empty;
        }
    }
}
