using System;
using System.Drawing;
using System.Windows.Forms;

namespace FiveInARow
{
    public partial class MainForm : Form
    {
        private const int BoardSize = 10;
        private const int CellSize = 40;
        private const int WinLength = 5;

        private Button[,] board;
        private bool playerXTurn;

        public MainForm()
        {
            InitializeComponent();
            InitializeBoard();
            playerXTurn = true;
        }

        private void InitializeBoard()
        {
            board = new Button[BoardSize, BoardSize];

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var button = new Button
                    {
                        Size = new Size(CellSize, CellSize),
                        Location = new Point(col * CellSize, row * CellSize),
                        Tag = new Cell(row, col)
                    };
                    button.Click += Cell_Click;
                    Controls.Add(button);
                    board[row, col] = button;
                }
            }
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            Button cell = (Button)sender;
            Cell position = (Cell)cell.Tag;

            if (cell.Text != "")
            {
                MessageBox.Show("Invalid move!");
                return;
            }

            cell.Text = playerXTurn ? "X" : "O";

            if (CheckWin(position.Row, position.Column, playerXTurn))
            {
                MessageBox.Show((playerXTurn ? "X" : "O") + " wins!");
                ResetGame();
                return;
            }

            playerXTurn = !playerXTurn;
        }

        private bool CheckWin(int row, int col, bool isPlayerX)
        {
            int count;

            // Проверка горизонтальных линий
            count = 1;
            for (int i = 1; i < WinLength; i++)
            {
                if (col - i < 0 || board[row, col - i].Text != (isPlayerX ? "X" : "O"))
                    break;
                count++;
            }
            for (int i = 1; i < WinLength; i++)
            {
                if (col + i >= BoardSize || board[row, col + i].Text != (isPlayerX ? "X" : "O"))
                    break;
                count++;
            }
            if (count >= WinLength)
                return true;

            // Проверка вертикальных линий
            count = 1;
            for (int i = 1; i < WinLength; i++)
            {
                if (row - i < 0 || board[row - i, col].Text != (isPlayerX ? "X" : "O"))
                    break;
                count++;
            }
            for (int i = 1; i < WinLength; i++)
            {
                if (row + i >= BoardSize || board[row + i, col].Text != (isPlayerX ? "X" : "O"))
                    break;
                count++;
            }
            if (count >= WinLength)
                return true;

            // Проверка диагоналей слева направо
            count = 1;
            for (int i = 1; i < WinLength; i++)
            {
                if (row - i < 0 || col - i < 0 || board[row - i, col - i].Text != (isPlayerX ? "X" : "O"))
                    break;
                count++;
            }
            for (int i = 1; i < WinLength; i++)
            {
                if (row + i >= BoardSize || col + i >= BoardSize || board[row + i, col + i].Text != (isPlayerX ? "X" : "O"))
                    break;
                count++;
            }
            if (count >= WinLength)
                return true;

            // Проверка диагоналей справа налево
            count = 1;
            for (int i = 1; i < WinLength; i++)
            {
                if (row - i < 0 || col + i >= BoardSize || board[row - i, col + i].Text != (isPlayerX ? "X" : "O"))
                    break;
                count++;
            }
            for (int i = 1; i < WinLength; i++)
            {
                if (row + i >= BoardSize || col - i < 0 || board[row + i, col - i].Text != (isPlayerX ? "X" : "O"))
                    break;
                count++;
            }
            if (count >= WinLength)
                return true;

            return false;
        }

        private void ResetGame()
        {
            foreach (Button cell in board)
            {
                cell.Text = "";
            }
        }
    }

    public class Cell
    {
        public int Row { get; }
        public int Column { get; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}