using System;

class Program
{
    const int SIZE = 8;
    static int[,] board = new int[SIZE, SIZE];

    static void Main(string[] args)
    {
        PlaceQueens();
        DisplayBoard();
    }

    static void PlaceQueens()
    {
        for (int i = 0; i < SIZE; i++)
        {
            PlaceQueen(i);
        }
    }

    static bool PlaceQueen(int row)
    {
        int maxFreeFields = -1;
        int bestCol = -1;

        for (int col = 0; col < SIZE; col++)
        {
            if (IsSafe(row, col))
            {
                int freeFields = CountFreeFields(row, col);
                if (freeFields > maxFreeFields)
                {
                    maxFreeFields = freeFields;
                    bestCol = col;
                }
            }
        }

        if (bestCol != -1)
        {
            board[row, bestCol] = 1; // Place the queen
            return true;
        }
        return false; // Failed to place a queen in this row
    }

    static bool IsSafe(int row, int col)
    {
        // Check column and diagonals for existing queens
        for (int i = 0; i < row; i++)
        {
            if (board[i, col] == 1 ||
                (col - (row - i) >= 0 && board[i, col - (row - i)] == 1) ||
                (col + (row - i) < SIZE && board[i, col + (row - i)] == 1))
            {
                return false;
            }
        }
        return true;
    }

    static int CountFreeFields(int row, int col)
    {
        int count = 0;

        // Check all possible directions
        for (int r = 0; r < SIZE; r++)
        {
            for (int c = 0; c < SIZE; c++)
            {
                if (board[r, c] == 0)
                {
                    if (r != row && c != col && Math.Abs(r - row) != Math.Abs(c - col))
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    static void DisplayBoard()
    {
        for (int r = 0; r < SIZE; r++)
        {
            for (int c = 0; c < SIZE; c++)
            {
                Console.Write(board[r, c] == 1 ? " Q " : " . ");
            }
            Console.WriteLine();
        }
    }
}