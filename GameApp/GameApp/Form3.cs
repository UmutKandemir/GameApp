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
    public partial class Form3 : Form
    {
        private int stones = 10;
        private bool isMaxPlayer = true;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int move = Convert.ToInt32(comboBox1.SelectedItem);
            stones -= move;
            listBox1.Items.Add(string.Format("{0} played {1}", Form2.userName, move));
            if (stones > 0)
            {
                int moveComputer = play();
                stones -= moveComputer;
                lblCount.Text = stones.ToString();
                listBox1.Items.Add(string.Format("Computer played {0}", moveComputer));

                if (stones <= 0)
                {
                    MessageBox.Show("Computer Win!!");
                    btnDisable();
                    btnStartNewGame.Visible = true;
                }
            }
            else
            {
                MessageBox.Show(string.Format("Congratulations {0}, You Win!!", Form2.userName));
                btnDisable();
                btnStartNewGame.Visible = true;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            btnStartNewGame.Visible = false;
            comboBox1.SelectedIndex = 0;
            if (!Form2.userStart)
            {
                btnDisable();
                int move = play();
                stones -= move;
                lblCount.Text = stones.ToString();
                listBox1.Items.Add(string.Format("Computer played {0}", move));
                btnEnable();
            }
        }

        private int play()
        {
            int move = 0;
            int bestScore = int.MinValue;
            if (isMaxPlayer)
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (stones >= i)
                    {
                        int score = Minimax(stones - i, 0, false);
                        if (score > bestScore)
                        {
                            bestScore = score;
                            move = i;
                        }
                    }
                }
            }
            // Determine the best move for the min player
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (stones >= i)
                    {
                        int score = Minimax(stones - i, 0, true);
                        if (score < bestScore)
                        {
                            bestScore = score;
                            move = i;
                        }
                    }
                }
            }

            return move;
        }

        private void btnDisable()
        {
            btnOk.Enabled = false;
        }

        private void btnEnable()
        {
            btnOk.Enabled = true;
        }

        private int Minimax(int stones, int depth, bool isMaxPlayer)
        {
            // Check if the game is over
            if (stones == 0)
            {
                return (isMaxPlayer) ? -1 : 1;
            }

            // Max player's turn
            if (isMaxPlayer)
            {
                int bestScore = int.MinValue;

                for (int i = 1; i <= 3; i++)
                {
                    if (stones >= i)
                    {
                        int score = Minimax(stones - i, depth + 1, !isMaxPlayer);
                        if (bestScore < score)
                            bestScore = score;
                    }
                }

                return bestScore;
            }
            // Min player's turn
            else
            {
                int bestScore = int.MaxValue;

                for (int i = 1; i <= 3; i++)
                {
                    if (stones >= i)
                    {
                        int score = Minimax(stones - i, depth + 1, !isMaxPlayer);
                        if (bestScore > score)
                            bestScore = score;
                    }
                }

                return bestScore;
            }
        }

        private void btnStartNewGame_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
