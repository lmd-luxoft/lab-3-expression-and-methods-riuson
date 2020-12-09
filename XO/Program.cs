using System;

namespace XO
{
    internal class Program
    {
        private const char Player1Char = 'X';
        private const char Player2Char = '0';
        private static char win = '-';
        private static string PlayerName1, PlayerName2;
        private static readonly char[] cells = {'-', '-', '-', '-', '-', '-', '-', '-', '-'};

        private static string EnterPlayerName(int playerNumber)
        {
            string result;

            do
            {
                Console.Write($"Введите имя игрока {playerNumber}: ");
                result = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(result));

            return result;
        }

        private static int EnterCellNumber(string prompt)
        {
            int result;
            string stringInput;

            do
            {
                Console.Write(prompt);
                stringInput = Console.ReadLine();
                Console.WriteLine();
            } while (!int.TryParse(stringInput, out result));

            return result;
        }

        private static string GetPlayerName(int number)
        {
            return number == 1 ? PlayerName1 : PlayerName2;
        }

        private static bool IsFirstPlayer(int step)
        {
            return step % 2 != 0;
        }

        private static bool IsRowCompleted(int rowIndex, out char value)
        {
            value = cells[rowIndex * 3];
            return cells[rowIndex * 3] == cells[rowIndex * 3 + 1] && cells[rowIndex * 3 + 1] == cells[rowIndex * 3 + 2];
        }

        private static bool IsColumnCompleted(int columnIndex, out char value)
        {
            value = cells[columnIndex];
            return cells[columnIndex] == cells[columnIndex + 3] && cells[columnIndex + 3] == cells[columnIndex + 6];
        }

        private static bool IsAnyDiagonalCompleted(out char value)
        {
            if (cells[2] == cells[4] && cells[4] == cells[6])
            {
                value = cells[2];
                return true;
            }

            value = cells[0];
            return cells[0] == cells[4] && cells[4] == cells[8];
        }

        private static bool IsCellNumberInRange(int cellNumber)
        {
            return cellNumber >= 1 && cellNumber <= 9;
        }

        private static bool IsCellUsed(int cellNumber)
        {
            return cells[cellNumber - 1] == Player2Char || cells[cellNumber - 1] == Player1Char;
        }

        private static bool IsCompleted(out char symbol)
        {
            char result;

            for (var i = 0; i < 3; i++)
            {
                if (IsRowCompleted(i, out symbol))
                    return true;

                if (IsColumnCompleted(i, out symbol))
                    return true;
            }

            if (IsAnyDiagonalCompleted(out symbol))
                return true;

            return false;
        }

        private static void SetCell(int cellNumber, int playerNumber)
        {
            cells[cellNumber - 1] = playerNumber == 1 ? Player1Char : Player2Char;
        }

        private static void MakeMove(int num)
        {
            Console.Write(GetPlayerName(num));

            var cell = EnterCellNumber(", введите номер ячейки,сделайте свой ход: ");

            while (!IsCellNumberInRange(cell) || IsCellUsed(cell))
                cell = EnterCellNumber(
                    "Введите номер правильного ( 1-9 ) или пустой ( --- ) клетки , чтобы сделать ход:");

            SetCell(cell, num);
        }

        private static void ShowResult(bool anyWin)
        {
            if (!anyWin)
            {
                Console.WriteLine("Ничья.");
                return;
            }

            if (win == Player1Char)
                Console.WriteLine($"{PlayerName1}, вы  выиграли поздравляем\n{PlayerName2}, а вы проиграли...");
            else if (win == Player2Char)
                Console.WriteLine($"{PlayerName2}, вы  выиграли поздравляем\n {PlayerName1}, а вы проиграли...");
        }

        private static void ShowCells()
        {
            Console.Clear();

            Console.WriteLine("Числа клеток:");
            Console.WriteLine("-1-|-2-|-3-");
            Console.WriteLine("-4-|-5-|-6-");
            Console.WriteLine("-7-|-8-|-9-");

            Console.WriteLine("Текущая ситуация (---пустой):");
            Console.WriteLine($"-{cells[0]}-|-{cells[1]}-|-{cells[2]}-");
            Console.WriteLine($"-{cells[3]}-|-{cells[4]}-|-{cells[5]}-");
            Console.WriteLine($"-{cells[6]}-|-{cells[7]}-|-{cells[8]}-");
        }

        private static void Main(string[] args)
        {
            do
            {
                PlayerName1 = EnterPlayerName(1);
                PlayerName2 = EnterPlayerName(2);
                Console.WriteLine();
            } while (PlayerName1 == PlayerName2);

            ShowCells();

            for (var move = 1; move <= cells.Length; move++)
            {
                MakeMove(IsFirstPlayer(move) ? 1 : 2);

                ShowCells();

                if (move >= 5)
                    if (IsCompleted(out win))
                        break;
            }

            ShowResult(IsCompleted(out win));

            Console.ReadLine();
        }
    }
}