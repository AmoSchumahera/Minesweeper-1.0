using Minesweeper_1._0.Data;
using Minesweeper_1._0.Models;
using Minesweeper_1._0.Project;
using Minesweeper_1._0.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper_1._0
{
    public partial class Form1 : Form
    {
        private DatabaseManager db = new DatabaseManager();
        private bool gameEnded = false;


        private GameEngine game;
        private Button[,] buttons;
        private int cellSize = 30;
        private int timeElapsed= 0;
        public Form1()
        {
            InitializeComponent();
        }
        //4.
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlayerName.Text))
            {
                MessageBox.Show("Please enter your name to start");
                return;
            }

            StartGame();
        }
        private RecordService recordService = new RecordService();
        private void StartGame()
        {
            gameOver = false;

            int mines = (int)numMines.Value;

            game = new GameEngine(mines);
            buttons = new Button[15, 15];

            panelBoard.Controls.Clear();
            panelBoard.Width = cellSize * 15;
            panelBoard.Height = cellSize * 15 ;

            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 15; c++)
                {
                    Button btn = new Button();
                    btn.Width = cellSize;
                    btn.Height = cellSize;
                    btn.Location = new Point(c * cellSize, r * cellSize);
                    btn.Tag = new Point(r, c);
                    btn.MouseDown += Cell_MouseDown;

                    panelBoard.Controls.Add(btn);
                    buttons[r, c] = btn;
                }
            }
                timeElapsed = 0;
                lblTimer.Text = "Time: 0s";
                gameTimer.Start();
        }
        

        //6.
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            lblTimer.Text = $"Time: {timeElapsed}s";
        }

        private bool gameOver = false;

        private void Cell_MouseDown(object sender, MouseEventArgs e)
        {
            if (gameOver) return;

            Button btn = sender as Button;
            Point p = (Point)btn.Tag;

            if (e.Button == MouseButtons.Left)
            {
                if (game.FirstClick)

                    game.PlaceMines(p.X, p.Y);

                if (game.Board[p.X, p.Y].IsMine)
                {
                    ShowAllMines();
                    EndGame(false);
                    return;
                }

                game.RevealCell(p.X, p.Y);
                UpdateBoard();

                if (game.CheckWin() || game.CheckWinByFlags())
                    EndGame(true);
            }

            else if (e.Button == MouseButtons.Right)
            {
                if (!game.Board[p.X, p.Y].IsRevealed)
                {
                    if (!game.Board[p.X, p.Y].IsMine)
                    {
                        btn.Text = "❌";
                        ShowAllMines();
                        EndGame(false);
                        return;
                    }

                    // Ако е мина -> позволяваме флаг
                    btn.Text = btn.Text == "🚩" ? "" : "🚩";
                    game.Board[p.X, p.Y].IsFlagged = !game.Board[p.X, p.Y].IsFlagged;

                    if (game.CheckWin() || game.CheckWinByFlags())
                    {
                        EndGame(true);
                    }
                }
            }
        }
        private void ShowAllMines()
        {
            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 15; c++)
                {
                    if (game.Board[r, c].IsMine)
                    {
                        buttons[r, c].Text = "💣";
                    }
                }
            }
        }



        private void UpdateBoard()
        {
            for (int r = 0; r < 15; r++)
            {
                for (int c = 0; c < 15; c++)
                {
                    if (game.Board[r, c].IsRevealed)
                    {
                        buttons[r, c].BackColor = Color.LightGray;

                        int number = game.Board[r, c].AdjacentMines;
                        if (number>0)
                            buttons[r, c].Text = number.ToString();   
                        
                    }
                }
            }
        }
        private void EndGame(bool win)
        {
            gameOver = true;

            gameTimer.Stop();

            foreach (Button btn in buttons)
                btn.Enabled = false;

            if (win)
            {
                SoundPlayer windSound = new SoundPlayer(@"..\..\win.wav");
                windSound.Play();
                MessageBox.Show("!WINNER WINNER CHICKEN DINNER!");
                recordService.SaveGame(txtPlayerName.Text,
                       (int)numMines.Value,
                       timeElapsed,
                       win);
            }
            else

            {
                SoundPlayer loseSound = new SoundPlayer(@"..\..\lose.wav");
                loseSound.Play();
                MessageBox.Show("!CHICKEN DINNER ONLY FOR THE WINNER!");
                recordService.SaveGame(txtPlayerName.Text,
              (int)numMines.Value,
              timeElapsed,
              false);
            }
            LoadLeaderboard();

        }
        private void LoadLeaderboard()
        {
            string filter = cmbFilter.SelectedItem?.ToString() ?? "All Time";
            dgvLeaderboard.DataSource = recordService.GetLeaderboard(filter);
        }
        //3.
        private void numMines_Value(object sender, EventArgs e)
        {

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        //2.
        private void txtPlayerName_Text(object sender, EventArgs e)
        {

        }

        

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        //5.
        private void lblTimer_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        //1.
        private void panelBoard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLeaderboard();
        }
    }
}
