using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
class TicTacToe
{
    private char[,] board;
    private char playerSymbol;
    private char botSymbol;
    private char chislo1;
    private char player2Symbol;


    public TicTacToe(char playerSymbol, char botSymbol)
    {
        Random rnd = new Random();
        chislo1 = (char)rnd.Next(5, 25);

        board = new char[chislo1, chislo1]; // Use a large array to represent an endless field
        this.playerSymbol = playerSymbol;
        this.botSymbol = botSymbol;
    }

    public void StartGame()
    {
        Console.WriteLine("Выберете с кем вы будете играть? ");
        Console.WriteLine("1. С ботом ");
        Console.WriteLine("2. С другом ");

        int choice = int.Parse(Console.ReadLine());

        if (choice == 2)
        {
            Console.WriteLine("Первый Игрок играет за " + playerSymbol);
            Console.WriteLine("Второй Игрок играет за " + botSymbol);
        }
        else if (choice == 1)
        {
            Console.WriteLine("Игрок играет за " + playerSymbol);
        }

        DrawBoard();

        while (true)
        {   // Ход игрока
            Console.Write("Ход первого игрока ");

            Console.Write("Введите строку: ");
            int row = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Введите столбец: ");
            int col = int.Parse(Console.ReadLine()) - 1;

            if (board[row, col] != '\0')
            {
                Console.WriteLine("Это место уже занято!");
                continue;
            }
            board[row, col] = playerSymbol;
            DrawBoard();
            if (CheckWinner(playerSymbol))
            {
                Console.WriteLine("Поздравляем, первый игрок выиграл!");
                Console.ReadLine();
                break;
            }
            // Ход противника
            if (choice == 1) // Бот
            {
                Console.WriteLine("Бот думает...");
                MakeBotMove(row, col);
                DrawBoard();
                if (CheckWinner(botSymbol))
                {
                    Console.WriteLine("Извините, вы проиграли...");
                    Console.ReadLine();
                    break;
                }
            }
            else if (choice == 2) // 2 игрок
            {
                Console.Write("Ход второго игрока ");

                Console.Write("Введите строку: ");
                row = int.Parse(Console.ReadLine()) - 1;
                Console.Write("Введите столбец: ");
                col = int.Parse(Console.ReadLine()) - 1;

                if (board[row, col] != '\0')
                {
                    Console.WriteLine("Это место уже занято!");
                    continue;
                }
                board[row, col] = botSymbol;
                DrawBoard();
                if (CheckWinner(botSymbol))
                {
                    Console.WriteLine("Поздравляем, второй игрок выиграл!");
                    Console.ReadLine();
                    break;
                }
            }
        }
    }
    private void DrawBoard()
    {
        Console.WriteLine("     " + string.Join("     ", Enumerable.Range(1, 9)) + "     " + string.Join("    ", Enumerable.Range(10, chislo1 - 9)));

        Console.WriteLine("  " + new string('-', 6 * chislo1));
        for (int i = 0; i < chislo1; i++)
        {
            if (i < 9)
                Console.Write(i + 1 + " |");
            else Console.Write(i + 1 + "|");
            for (int j = 0; j < chislo1; j++)
            {
                Console.Write(" " + board[i, j] + "   |");
            }
            Console.WriteLine();
            Console.WriteLine("  " + new string('-', 6 * chislo1));
        }
    }
    private bool CheckWinner(char symbol)
    {
        // Check rows
        for (int i = 0; i < chislo1; i++)
        {
            for (int j = 0; j < (chislo1 - 3); j++)
            {
                if (board[i, j] == symbol && board[i, j + 1] == symbol && board[i, j + 2] == symbol)
                {
                    return true;
                }
            }
        }
        // Check columns
        for (int i = 0; i < (chislo1 - 3); i++)
        {
            for (int j = 0; j < chislo1; j++)
            {
                if (board[i, j] == symbol && board[i + 1, j] == symbol && board[i + 2, j] == symbol)
                {
                    return true;
                }
            }
        }
        // Check diagonals
        for (int i = 0; i < (chislo1 - 3); i++)
        {
            for (int j = 0; j < (chislo1 - 3); j++)
            {
                if (board[i, j] == symbol && board[i + 1, j + 1] == symbol && board[i + 2, j + 2] == symbol)
                {
                    return true;
                }
                if (board[i, j + 2] == symbol && board[i + 1, j + 1] == symbol && board[i + 2, j] == symbol)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void MakeBotMove(int row, int col)
    {
        int x = row;
        int y = col;

        // проверяем возможность поставить символ рядом с ходом игрока
        if (x > 0 && board[x - 1, y] == '\0')
        {
            x--;
        }
        else if (x < board.GetLength(0) - 1 && board[x + 1, y] == '\0')
        {
            x++;
        }
        else if (y > 0 && board[x, y - 1] == '\0')
        {
            y--;
        }
        else if (y < board.GetLength(1) - 1 && board[x, y + 1] == '\0')
        {
            y++;
        }
        else // если рядом с ходом игрока нет свободных ячеек, выбираем случайную
        {
            Random rnd = new Random();
            x = rnd.Next(board.GetLength(0));
            y = rnd.Next(board.GetLength(1));

            while (board[x, y] != '\0')
            {
                x = rnd.Next(board.GetLength(0));
                y = rnd.Next(board.GetLength(1));
            }
        }
        board[x, y] = botSymbol;
    }

}
class Program
{
    static void Main(string[] args)
    {
        TicTacToe game = new TicTacToe('X', 'O');
        game.StartGame();
    }
}

