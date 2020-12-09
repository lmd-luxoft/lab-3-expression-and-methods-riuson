using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    class Program
    {
       static char win = '-';
       static string PlayerName1, PlayerName2;
       static char[] cells = new char[]{ '-', '-', '-', '-', '-', '-', '-', '-', '-' };

        static void show_cells()
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
        static void make_move(int num)
        {
            string raw_cell;
            int cell;
            if (num == 1) Console.Write(PlayerName1);
            else Console.Write(PlayerName2);
            do
            {
                Console.Write(",введите номер ячейки,сделайте свой ход:");

                raw_cell = Console.ReadLine();
            }
            while (!Int32.TryParse(raw_cell, out cell));
            while (cell > 9 || cell < 1 || cells[cell - 1] == 'O' || cells[cell - 1] == 'X')
            {
                do
                {
                    Console.Write("Введите номер правильного ( 1-9 ) или пустой ( --- ) клетки , чтобы сделать ход:");
                    raw_cell = Console.ReadLine();
                }
                while (!Int32.TryParse(raw_cell, out cell));
                Console.WriteLine();
            }
            if (num == 1) cells[cell - 1] = 'X';
            else cells[cell - 1] = 'O';
            
        }
        static char check()
        {
            for (int i = 0; i < 3; i++)
                if (cells[i * 3] == cells[i * 3 + 1] && cells[i * 3 + 1] == cells[i * 3 + 2])
                    return cells[i];
                else if (cells[i] == cells[i + 3] && cells[i + 3] == cells[i + 6])
                    return cells[i];
                else if ((cells[2] == cells[4] && cells[4] == cells[6]) || (cells[0] == cells[4] && cells[4] == cells[8]))
                    return cells[i];
            return '-';
        }

        static void result()
        {
            if (win == 'X')
                Console.WriteLine($"{PlayerName1} вы  выиграли поздравляем {PlayerName2} а вы проиграли...");
            else if (win == 'O')
                Console.WriteLine($"{PlayerName2} вы  выиграли поздравляем {PlayerName1} а вы проиграли...");

        }

        static void Main(string[] args)
        {
            do
            {
                Console.Write("Введите имя первого игрока : ");
                PlayerName1 = Console.ReadLine();

                Console.Write("Введите имя второго игрока: ");
                PlayerName2 = Console.ReadLine();
                Console.WriteLine();
            } while (PlayerName1 == PlayerName2);

            show_cells();

            for (int move = 1; move <= 9; move++)
            {
                if (move % 2 != 0) make_move(1);
                else make_move(2);

                show_cells();

                if (move >= 5)
                {
                    win = check();
                    if (win != '-')
                        break;
                }

            }

            result();
        }
    }
}
