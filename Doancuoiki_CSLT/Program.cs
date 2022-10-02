using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cocaro
{
    internal class Program
    {

        static string player = "O";
        static bool timedown = true;
        static int KhoiTao()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(11, 0);
            Console.WriteLine("Welcome to CARO chess");
            Console.ResetColor();
            Thread.Sleep(1500);
            Console.WriteLine($"\nHai cach dieu khien");
            Thread.Sleep(1000);
            Console.WriteLine("Cach 1: W_(len) A_(trai) S_(xuong) D_(phai) va Spacebar_(danh)");
            Console.WriteLine("Cach 2: Cac phim Len - Xuong - Trai - Phai va Enter_(danh) trong ban phim");
            Thread.Sleep(3000);
            bool kt_size = true;
            Console.Clear();
            Console.Write("Nhap kich thuoc ban co NxN (10=<N=<100): ");
            int n = int.Parse(Console.ReadLine());
            do
            {
                kt_size = true;
                if (n < 10 || n > 100)
                {
                    Console.WriteLine("Ngoai gioi han cho phep! Moi nhap lai.");
                    n = int.Parse(Console.ReadLine());
                    Console.Clear();
                    kt_size = false;
                }
            } while (kt_size == false);
            return n;
        }
        static int n = KhoiTao();
        static int dong = 3 + n / 2;
        static int cot = 3 + n / 2;
        static string[,] banco = new string[n + 8, n + 8];
        static void BanCoCaro()
        {
            for (int i = 0; i < n + 8; i++)
                for (int j = 0; j < n + 8; j++)
                    banco[i, j] = "*";
        }
        static void PrintBanCo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Luot cua '{0}'.", player.ToUpper());
            Console.ResetColor();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Thoi gian: ");
            Console.SetCursorPosition(0, 4);
            for (int i = 4; i < n + 4; i++)
            {
                for (int j = 4; j < n + 4; j++)
                {
                    if (i == dong && j == cot)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(banco[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (banco[i, j] == "O")
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else
                            if (banco[i, j] == "X")
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(banco[i, j] + " ");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }
        static int KiemTra()
        //2 = Win ./ 1 = Draw ./0 = Continute
        {
            // Dòng
            for (int i = 4; i >= 0; i--)
            {
                int dem = 0;
                for (int j = 0; j <= 4; j++)
                {
                    if (banco[dong, cot] == banco[dong, cot - i + j]) dem++;
                }
                if (dem == 5) return 2;
            }
            // Cột
            for (int i = 4; i >= 0; i--)
            {
                int dem = 0;
                for (int j = 0; j <= 4; j++)
                {
                    if (banco[dong, cot] == banco[dong - i + j, cot]) dem++;
                }
                if (dem == 5) return 2;
            }
            // Chéo
            for (int i = 4; i >= 0; i--)
            {
                int dem = 0;
                for (int j = 0; j <= 4; j++)
                {
                    if (banco[dong, cot] == banco[dong - i + j, cot - i + j]) dem++;
                }
                if (dem == 5) return 2;
            }
            for (int i = 4; i >= 0; i--)
            {
                int dem = 0;
                for (int j = 0; j <= 4; j++)
                {
                    if (banco[dong, cot] == banco[dong + i - j, cot - i + j]) dem++;
                }
                if (dem == 5) return 2;
            }
            // Hòa
            if (KtOChuaDanh() == false)
            {
                return 1;
            }
            else
                return 0;
        }
        static bool KtOChuaDanh()
        {
            bool kt_o_chua_danh = false;
            for (int i = 4; i < n + 4; i++)
                for (int j = 4; j < n + 4; j++)
                    if (banco[i, j] == "*")
                    {
                        kt_o_chua_danh = true;
                        goto breakOut;
                    }
                breakOut:
            return kt_o_chua_danh;
        }
        static void ThreadingTime()
        {
            for (int i = 60; i >= 0; i--)
            {
                Console.SetCursorPosition(7, 2);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(i + " ");
                Console.ResetColor();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                if (i == 0) 
                    timedown = false;
            }
        }
        static void Main(string[] args)
        {
            bool chonOX = true;
            bool playagain = true;
            //Chọn X và O để đi trước
            do
            {
                timedown = true;
                BanCoCaro();
                do
                {
                    chonOX = true;
                    Console.WriteLine($"\nChon quan X hay O di truoc?");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("1. O");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("2. X");
                    Console.ResetColor();
                    string choice = Console.ReadLine();

                    if (choice.Trim().Equals("1"))
                        player = "O";
                    else if (choice.Trim().Equals("2"))
                        player = "X";
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("ERROR! " + "Moi Ban Nhap Lai!!!");
                        chonOX = false;
                    }
                } while (chonOX == false);
                Thread t = new Thread(ThreadingTime);
                int tam = 1;
                while (true)
                {
                    Console.Clear();
                    PrintBanCo();
                    if (tam == 1) t.Start();
                    tam = 0;
                    if (timedown == false)
                    {
                        t.Abort();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nEnd Game!!! Vi qua thoi gian quy dinh");
                        Console.ForegroundColor = ConsoleColor.Red;
                        player = (player == "o") ? "x" : "o";
                        Console.WriteLine($"Nguoi choi {player.ToUpper()} Thang!!!");
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        ConsoleKeyInfo key = Console.ReadKey();
                        if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                        {
                            dong--;
                            if (dong < 4) dong = n + 3;
                        }
                        else
                        if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                        {
                            dong++;
                            if (dong > n + 3) dong = 4;
                        }
                        else
                        if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                        {
                            cot++;
                            if (cot > n + 3) cot = 4;
                        }
                        else
                        if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                        {
                            cot--;
                            if (cot < 4) cot = n + 3;
                        }
                        else
                        if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                        {
                            if (timedown != false)
                            {
                                if (banco[dong, cot] == "*")
                                {
                                    t.Abort();
                                    t = new Thread(ThreadingTime);
                                    t.Start();
                                    banco[dong, cot] = player;
                                    if (KiemTra() == 2)
                                    {
                                        t.Abort();
                                        Console.Clear();
                                        PrintBanCo();
                                        Console.WriteLine();
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine("End Game!!!");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"Nguoi choi {player.ToUpper()} Thang!!!");
                                        Console.ResetColor();
                                        break;
                                    }
                                    else if (KiemTra() == 1)
                                    {
                                        t.Abort();
                                        Console.Clear();
                                        PrintBanCo();
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine($"\nEnd Game!!!");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"Hai nguoi choi hoa.");
                                        Console.ResetColor();
                                        break;
                                    }
                                    player = (player == "O") ? "X" : "O";
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition(0, n + 5);
                                    Console.WriteLine("Can danh vao o trong!!!");
                                    Console.ResetColor();
                                    Thread.Sleep(2000);
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(3000);
                Console.WriteLine($"\nAn phim bat ki de tiep tuc...");
                Console.ReadKey();
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("Ban co muon choi lai khong?");
                Console.WriteLine("1. Co");
                Console.WriteLine("2. Khong");
                bool kt = true;
                do
                {
                    kt = true;
                    string again = Console.ReadLine();
                    if (again.Equals("2")) playagain = false;
                    else if (again.Equals("1")) Console.Clear();
                    else
                    {
                        Console.WriteLine("ERROR! Moi Nhap Lai!!!");
                        kt = false;
                    }
                } while (kt == false);
            }
            while (playagain == true);
        }
    }
}