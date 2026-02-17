using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_1._0.Project
{
    public class GameEngine
    {
        public const int Rows = 15;
        public const int Cols = 15;

        public Cell[,] Board { get; private set; }
        public int MineCount { get; private set; }
        public bool FirstClick { get; private set; }

        public GameEngine(int mines)
        {
            if (mines >= Rows * Cols)
                throw new ArgumentException("Too many mines!");

            MineCount = mines;
            Board = new Cell[Rows, Cols];
            InitializeBoard();
            FirstClick = true;
        }

        private Random rand = new Random();

        private void InitializeBoard()
        {

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    Board[r, c] = new Cell();
                }
            }
        }
        public void PlaceMines(int firstRow, int firstCol)
        {
            List<(int r, int c)> allCells = new List<(int, int)>();

            // Създаваме списък с всички клетки
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    // Първата клетка не може да е мина
                    if (!(r == firstRow && c == firstCol))
                    {
                        allCells.Add((r, c));
                    }
                }
            }

            // Разбъркване (Fisher-Yates Shuffle)
            for (int i = allCells.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                var temp = allCells[i];
                allCells[i] = allCells[j];
                allCells[j] = temp;
            }

            // Поставяме първите MineCount клетки като мини
            for (int i = 0; i < MineCount; i++)
            {
                var (r, c) = allCells[i];
                Board[r, c].IsMine = true;
            }

            CalculateNumbers();
            FirstClick = false;
        }
        private void CalculateNumbers()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Board[r, c].IsMine) continue;

                    int count = 0;

                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                        {
                            int nr = r + i;
                            int nc = c + j;

                            if (IsValid(nr, nc) && Board[nr, nc].IsMine)
                                count++;
                        }

                    Board[r, c].AdjacentMines = count;
                }

            }
        }
        public void RevealCell(int r, int c)
        {
            if (!IsValid(r, c)) return;
            if (Board[r, c].IsRevealed || Board[r, c].IsFlagged) return;

            Board[r, c].IsRevealed = true;


            if (Board[r, c].AdjacentMines == 0)
            {
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                    {
                        RevealCell(r + i, c + j);
                    }
            }
        }
        public bool CheckWin()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (!Board[r, c].IsMine && !Board[r, c].IsRevealed)
                        return false;
                }
            }

            return true;
        }
        public bool CheckWinByFlags()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Board[r, c].IsMine && !Board[r, c].IsFlagged)
                        return false;

                    if (!Board[r, c].IsMine && Board[r, c].IsFlagged)
                        return false;
                }
            }

            return true;
        }


        private bool IsValid(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Cols;
        }
    }
}
