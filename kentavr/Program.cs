using System;

class Program
{
    static int[] rookDx = new int[] { 1, -1, 0, 0 };
    static int[] rookDy = new int[] { 0, 0, 1, -1 };

    static int[] knightDx = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
    static int[] knightDy = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };

    static char[,] board = new char[8, 8];
    static int[,] scoreBoard = new int[8, 8];

    static void Main()
    {
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                board[i, j] = '.';

        Console.WriteLine("Initial Board:");
        PrintBoard();

        Console.Write("Enter row (0-7) for Kentavr placement: ");
        int startX = int.Parse(Console.ReadLine());
        Console.Write("Enter column (0-7) for Kentavr placement: ");
        int startY = int.Parse(Console.ReadLine());

        PlaceKentavr(startX, startY);
        PrintBoard();

        while (HasFreeCell())
        {
            var bestCell = FindBestCell();
            if (bestCell.Item1 == -1) break;
            Console.WriteLine($"Placing next Centaur at ({bestCell.Item1}, {bestCell.Item2})");
            PlaceKentavr(bestCell.Item1, bestCell.Item2);
            PrintBoard();
        }
    }

    static void PlaceKentavr(int x, int y)
    {
        board[x, y] = 'K';
        MarkHits(x, y);
        UpdateScores();
    }

    static void MarkHits(int x, int y)
    {
        for (int i = 0; i < 4; i++)
        {
            int nx = x, ny = y;
            while (IsInside(nx + rookDx[i], ny + rookDy[i]))
            {
                nx += rookDx[i];
                ny += rookDy[i];
                if (board[nx, ny] == '.')
                    board[nx, ny] = 'O';
            }
        }

        for (int i = 0; i < 8; i++)
        {
            int nx = x + knightDx[i];
            int ny = y + knightDy[i];
            if (IsInside(nx, ny) && board[nx, ny] == '.')
                board[nx, ny] = 'O';
        }
    }

    static void UpdateScores()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == '.')
                    scoreBoard[i, j] = CalculateFreeCells(i, j);
                else
                    scoreBoard[i, j] = -1;  
            }
        }
    }

    
    static int CalculateFreeCells(int x, int y)
    {
        bool[,] blocked = new bool[8, 8];

        for (int i = 0; i < 4; i++)
        {
            int nx = x, ny = y;
            while (IsInside(nx + rookDx[i], ny + rookDy[i]))
            {
                nx += rookDx[i];
                ny += rookDy[i];
                blocked[nx, ny] = true;
            }
        }

        for (int i = 0; i < 8; i++)
        {
            int nx = x + knightDx[i];
            int ny = y + knightDy[i];
            if (IsInside(nx, ny))
                blocked[nx, ny] = true;
        }

        int count = 0;
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (board[i, j] == '.' && !blocked[i, j])
                    count++;

        return count;
    }

    static bool HasFreeCell()
    {
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (board[i, j] == '.')
                    return true;
        return false;
    }

    static (int, int) FindBestCell()
    {
        int maxFreeCells = -1;
        int bestX = -1, bestY = -1;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == '.')
                {
                    int freeCells = scoreBoard[i, j];
                    if (freeCells > maxFreeCells)
                    {
                        maxFreeCells = freeCells;
                        bestX = i;
                        bestY = j;
                    }
                }
            }
        }

        return (bestX, bestY);
    }

    static bool IsInside(int x, int y) => x >= 0 && y >= 0 && x < 8 && y < 8;
    
    static void PrintBoard()
    {
        Console.WriteLine("\nCurrent Board:");
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == '.')
                    Console.Write(scoreBoard[i, j].ToString().PadLeft(3));
                else
                    Console.Write("  " + board[i, j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
