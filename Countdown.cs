using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        // 32 --> space || 35 --> # || 80 --> P
        public static int control = 1;
        public static int player_row; public static int player_column;
        public static int row70; public static int column70;
        public static char[,] board = new char[23, 53];
        public static Random random = new Random();
        public static int zero_counter = 0; public static int number = 0;
        public static int life = 5; public static int score = 0;
        public static ConsoleKeyInfo cki;


        static void Main(string[] args)
        {
            Console.CursorVisible = false; Console.ForegroundColor = ConsoleColor.White;
            DateTime time0 = DateTime.Now;

            Console.SetCursorPosition(55, 1);
            Console.WriteLine("Time: ");
            Console.SetCursorPosition(55, 3);
            Console.WriteLine("Life : " + life);
            Console.SetCursorPosition(55, 5);
            Console.WriteLine("Score : " + score);



            // Outer Walls
            Outer_Walls();

            // Inner Walls
            Wall_Placement();

            // Numbers
            Numbers_Location();

            Board();

            // Player Location
            Player_Location();

            // Game
            int counted_time = 0;
            while (life > 0)
            {

                // Time
                Console.SetCursorPosition(61, 1);
                int time = counted_time / 100;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(time);
                Thread.Sleep(10);
                counted_time++;

                // Player Move          
                if (Console.KeyAvailable)
                    Player_Move();

                // Zero Moves
                if (counted_time % 100 == 0)
                    Zero_Moves();

                // Countdown
                if (counted_time % 1500 == 0)
                    Countdown();
            }
            End_Game();
        }

        // Wall
        static void Outer_Walls()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (i == 0 || i == (board.GetLength(0) - 1) || j == 0 || j == (board.GetLength(1) - 1)) board[i, j] = '#';
                    else board[i, j] = ' ';
                }
            }
        }
        static bool Horizontal_Wall_Control(int row, int column, int max_column)
        {
            int currRow = row;
            int currColumn = column;
            for (int i = 0; i < max_column; i++)
            {
                control = 0;
                if (i == 0)
                {
                    if (board[currRow, currColumn + 1] == '#' || board[currRow - 1, currColumn + 1] == '#' || board[currRow + 1, currColumn + 1] == '#' || board[currRow, currColumn] == '#' || board[currRow, currColumn - 1] == '#' || board[currRow - 1, currColumn] == '#' || board[currRow - 1, currColumn - 1] == '#' || board[currRow + 1, currColumn] == '#' || board[currRow + 1, currColumn - 1] == '#')
                    {
                        control = 1;
                        return false;
                    }
                    else
                        currColumn++;
                }
                else if (i == max_column - 1)
                {
                    if (board[currRow, currColumn] == ' ' && board[currRow - 1, currColumn + 1] == ' ' && board[currRow, currColumn + 1] == ' ' && board[currRow + 1, currColumn + 1] == ' ' && board[currRow + 1, currColumn] == ' ' && board[currRow - 1, currColumn] == ' ')
                        currColumn++;
                    else
                    {
                        control = 1;
                        return false;
                    }
                }
                else
                {
                    if (board[currRow, currColumn] == ' ' && board[currRow + 1, currColumn] == ' ' && board[currRow - 1, currColumn] == ' ' && board[currRow - 1, currColumn + 1] == ' ' && board[currRow, currColumn + 1] == ' ' && board[currRow + 1, currColumn + 1] == ' ')
                        currColumn++;
                    else
                    {
                        control = 1;
                        return false;
                    }
                }
            }
            for (int i = 0; i < currColumn - column; i++)
                board[row, column + i] = '#';
            return true;
        }
        static bool Vertical_Wall_Control(int row, int column, int max_row)
        {
            int currRow = row;
            int currColumn = column;
            for (int i = 0; i < max_row; i++)
            {
                control = 0;
                if (i == 0)
                {
                    if (board[currRow, currColumn + 1] == '#' || board[currRow - 1, currColumn + 1] == '#' || board[currRow + 1, currColumn + 1] == '#' || board[currRow, currColumn] == '#' || board[currRow, currColumn - 1] == '#' || board[currRow - 1, currColumn] == '#' || board[currRow - 1, currColumn - 1] == '#' || board[currRow + 1, currColumn] == '#' || board[currRow + 1, currColumn - 1] == '#')
                    {
                        control = 1;
                        return false;
                    }
                    else
                        currRow++;
                }
                else if (i == max_row - 1)
                {
                    if (board[currRow + 1, currColumn - 1] == ' ' && board[currRow + 1, currColumn] == ' ' && board[currRow + 1, currColumn + 1] == ' ' && board[currRow, currColumn + 1] == ' ' && board[currRow, currColumn - 1] == ' ' && board[currRow, currColumn] == ' ')
                        currRow++;
                    else
                    {
                        control = 1;
                        return false;
                    }
                }
                else
                {
                    if (board[currRow + 1, currColumn - 1] == ' ' && board[currRow + 1, currColumn] == ' ' && board[currRow + 1, currColumn + 1] == ' ' && board[currRow, currColumn] == ' ')
                        currRow++;
                    else
                    {
                        control = 1;
                        return false;
                    }
                }
            }
            for (int i = 0; i < currRow - row; i++)
                board[row + i, column] = '#';
            return true;
        }
        static void Wall_Placement()
        {
            int long_wall = random.Next(0, 4); int medium_wall = random.Next(0, 6); int short_wall = random.Next(0, 21);
            for (int i = 0; i < long_wall; i++)
            {
                while (true)
                {
                    if (control == 1)
                        Horizontal_Wall_Control(random.Next(0, 23 - 1), random.Next(0, 53 - 12), 11);
                    else
                    {
                        control = 1;
                        break;
                    }
                }
            }               // Horizontal Long
            for (int i = 0; i < (3 - long_wall); i++)
            {
                while (true)
                {
                    if (control == 1)
                        Vertical_Wall_Control(random.Next(0, 23 - 12), random.Next(0, 53 - 1), 11);
                    else
                    {
                        control = 1;
                        break;
                    }
                }
            }        // Vertical Long
            for (int i = 0; i < medium_wall; i++)
            {
                while (true)
                {
                    if (control == 1)
                        Horizontal_Wall_Control(random.Next(0, 23 - 1), random.Next(0, 53 - 8), 7);
                    else
                    {
                        control = 1;
                        break;
                    }
                }
            }           // Horizontal Medium
            for (int i = 0; i < (5 - medium_wall); i++)
            {
                while (true)
                {
                    if (control == 1)
                        Vertical_Wall_Control(random.Next(0, 23 - 8), random.Next(0, 53 - 1), 7);
                    else
                    {
                        control = 1;
                        break;
                    }
                }
            }    // Vertical Medium
            for (int i = 0; i < short_wall; i++)
            {
                while (true)
                {
                    if (control == 1)
                        Horizontal_Wall_Control(random.Next(0, 23 - 1), random.Next(0, 53 - 4), 3);
                    else
                    {
                        control = 1;
                        break;
                    }
                }
            }          // Horizontal Short
            for (int i = 0; i < (20 - short_wall); i++)
            {
                while (true)
                {
                    if (control == 1)
                        Vertical_Wall_Control(random.Next(0, 23 - 4), random.Next(0, 53 - 1), 3);
                    else
                    {
                        control = 1;
                        break;
                    }
                }
            }  // Vertical Short
        }



        // Number and Player
        static void Numbers_Location()
        {
            int row, column, numbers = 0;
            while (number < 70)
            {
                if (number == 69) numbers = random.Next(53, 58);
                else numbers = random.Next(48, 58);
                row = random.Next(0, 23); column = random.Next(0, 53);
                if (board[row, column] == '#' || board[row, column] == Convert.ToChar(48) || board[row, column] == Convert.ToChar(49) || board[row, column] == Convert.ToChar(50) || board[row, column] == Convert.ToChar(51) || board[row, column] == Convert.ToChar(52) || board[row, column] == Convert.ToChar(53) || board[row, column] == Convert.ToChar(54) || board[row, column] == Convert.ToChar(55) || board[row, column] == Convert.ToChar(56) || board[row, column] == Convert.ToChar(57))
                {
                    row = random.Next(0, 23); column = random.Next(0, 53);
                }
                else
                {
                    board[row, column] = Convert.ToChar(numbers);
                    number++;
                    if (number == 70)
                    {
                        row70 = row;
                        column70 = column;
                    }
                }
            }
        }
        static void Player_Location()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            player_row = random.Next(1, 23); player_column = random.Next(1, 53);
            if (board[player_row, player_column] != ' ')
            {
                player_row = random.Next(1, 23); player_column = random.Next(1, 53);
            }
            else
            {
                board[player_row, player_column] = 'P';
                Console.SetCursorPosition(player_column, player_row);
                Console.WriteLine(board[player_row, player_column]);
            }
        }
        static void Player_Move()
        {
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.RightArrow)
            {
                if (board[player_row, player_column + 1] != '#')
                {
                    if (board[player_row, player_column + 1] < 58 && board[player_row, player_column + 1] >= 48)
                        Pushing();
                    else if (board[player_row, player_column + 1] == ' ')
                        Player_Right();




                }
            }   // Right
            if (cki.Key == ConsoleKey.LeftArrow)
            {
                if (board[player_row, player_column - 1] != '#')
                {
                    if (board[player_row, player_column - 1] < 58 && board[player_row, player_column - 1] >= 48)
                        Pushing();
                    else if (board[player_row, player_column - 1] == ' ')
                        Player_Left();
                }
            }   // Left
            if (cki.Key == ConsoleKey.UpArrow)
            {
                if (board[player_row - 1, player_column] != '#')
                {
                    if (board[player_row - 1, player_column] < 58 && board[player_row - 1, player_column] >= 48)
                        Pushing();

                    else if (board[player_row - 1, player_column] == ' ')
                        Player_Up();
                }
            }    // Up
            if (cki.Key == ConsoleKey.DownArrow)
            {
                if (board[player_row + 1, player_column] != '#')
                {
                    if (board[player_row + 1, player_column] < 58 && board[player_row + 1, player_column] >= 48)
                        Pushing();
                    else if (board[player_row + 1, player_column] == ' ')
                        Player_Down();
                }
            } // Down
        }
        static void Player_Right()
        {
            board[player_row, player_column] = ' '; Console.SetCursorPosition(player_column, player_row); Console.Write(' ');
            board[player_row, player_column + 1] = 'P'; Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine(board[player_row, player_column + 1]);
            player_column++;
        }
        static void Player_Left()
        {
            board[player_row, player_column] = ' '; Console.SetCursorPosition(player_column, player_row); Console.Write(' ');
            board[player_row, player_column - 1] = 'P'; Console.ForegroundColor = ConsoleColor.Blue; Console.SetCursorPosition(player_column - 1, player_row); Console.WriteLine(board[player_row, player_column - 1]);
            player_column--;
        }
        static void Player_Up()
        {
            board[player_row, player_column] = ' '; Console.SetCursorPosition(player_column, player_row); Console.Write(' ');
            board[player_row - 1, player_column] = 'P'; Console.ForegroundColor = ConsoleColor.Blue; Console.SetCursorPosition(player_column, player_row - 1); Console.WriteLine(board[player_row - 1, player_column]);
            player_row--;
        }
        static void Player_Down()
        {
            board[player_row, player_column] = ' '; Console.SetCursorPosition(player_column, player_row); Console.Write(' ');
            board[player_row + 1, player_column] = 'P'; Console.ForegroundColor = ConsoleColor.Blue; Console.SetCursorPosition(player_column, player_row + 1); Console.WriteLine(board[player_row + 1, player_column]);
            player_row++;
        }
        // Zero
        static void Zero_Moves()
        {
            int zero_random;
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    if (board[i, j] == '0')
                    {
                        zero_random = random.Next(4);
                        while (zero_counter == 0)
                        {
                            switch (zero_random)
                            {
                                case 0:
                                    if (board[i, j + 1] == ' ' || board[i, j + 1] == 'P')
                                        Zero_Right(i, j);
                                    else
                                        zero_random = random.Next(4);
                                    break;     // Right
                                case 1:
                                    if (board[i, j - 1] == ' ' || board[i, j - 1] == 'P')
                                        Zero_Left(i, j);
                                    else
                                        zero_random = random.Next(4);
                                    break;    // Left
                                case 2:
                                    if (board[i - 1, j] == ' ' || board[i - 1, j] == 'P')
                                        Zero_Up(i, j);
                                    else
                                        zero_random = random.Next(4);
                                    break;   // Up
                                case 3:
                                    if (board[i + 1, j] == ' ' || board[i + 1, j] == 'P')
                                        Zero_Down(i, j);
                                    else
                                        zero_random = random.Next(4);
                                    break;  // Down
                            }
                        }
                        zero_counter = 0;
                    }
        }
        static void Zero_Right(int i, int j)
        {
            if (board[i, j + 1] == ' ')
            {
                board[i, j] = ' '; Console.SetCursorPosition(j, i); Console.ForegroundColor = ConsoleColor.White; Console.Write(board[i, j]);
                board[i, j + 1] = '0'; Console.SetCursorPosition(j + 1, i); Console.ForegroundColor = ConsoleColor.Red; Console.Write(board[i, j + 1]);
            }
            else if (board[i, j + 1] == 'P')
            {
                life--; Console.SetCursorPosition(62, 3); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(life);
                board[i, j] = ' '; Console.SetCursorPosition(j, i); Console.ForegroundColor = ConsoleColor.White; Console.Write(board[i, j]);
                board[i, j + 1] = '0'; Console.SetCursorPosition(j + 1, i); Console.ForegroundColor = ConsoleColor.Red; Console.Write(board[i, j + 1]);
                Player_Location();
            }
            zero_counter++;
        }
        static void Zero_Left(int i, int j)
        {
            if (board[i, j - 1] == ' ')
            {
                board[i, j] = ' '; Console.SetCursorPosition(j, i); Console.ForegroundColor = ConsoleColor.White; Console.Write(board[i, j]);
                board[i, j - 1] = '0'; Console.SetCursorPosition(j - 1, i); Console.ForegroundColor = ConsoleColor.Red; Console.Write(board[i, j - 1]);
            }
            else if (board[i, j - 1] == 'P')
            {
                life--; Console.SetCursorPosition(62, 3); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(life);
                board[i, j] = ' '; Console.SetCursorPosition(j, i); Console.ForegroundColor = ConsoleColor.White; Console.Write(board[i, j]);
                board[i, j - 1] = '0'; Console.SetCursorPosition(j - 1, i); Console.ForegroundColor = ConsoleColor.Red; Console.Write(board[i, j - 1]);
                Player_Location();
            }
            zero_counter++;
        }
        static void Zero_Up(int i, int j)
        {
            if (board[i - 1, j] == ' ')
            {
                board[i, j] = ' '; Console.SetCursorPosition(j, i); Console.ForegroundColor = ConsoleColor.White; Console.Write(board[i, j]);
                board[i - 1, j] = '0'; Console.SetCursorPosition(j, i - 1); Console.ForegroundColor = ConsoleColor.Red; Console.Write(board[i - 1, j]);
            }
            else if (board[i - 1, j] == 'P')
            {
                life--; Console.SetCursorPosition(62, 3); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(life);
                board[i, j] = ' '; Console.SetCursorPosition(j, i); Console.ForegroundColor = ConsoleColor.White; Console.Write(board[i, j]);
                board[i - 1, j] = '0'; Console.SetCursorPosition(j, i - 1); Console.ForegroundColor = ConsoleColor.Red; Console.Write(board[i - 1, j]);
                Player_Location();
            }
            zero_counter++;
        }
        static void Zero_Down(int i, int j)
        {
            if (board[i + 1, j] == ' ')
            {
                board[i, j] = ' '; Console.SetCursorPosition(j, i); Console.ForegroundColor = ConsoleColor.White; Console.Write(board[i, j]);
                board[i + 1, j] = '0'; Console.SetCursorPosition(j, i + 1); Console.ForegroundColor = ConsoleColor.Red; Console.Write(board[i + 1, j]);
            }
            else if (board[i + 1, j] == 'P')
            {
                life--; Console.SetCursorPosition(62, 3); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(life);
                board[i, j] = ' '; Console.SetCursorPosition(j, i); Console.ForegroundColor = ConsoleColor.White; Console.Write(board[i, j]);
                board[i + 1, j] = '0'; Console.SetCursorPosition(j, i + 1); Console.ForegroundColor = ConsoleColor.Red; Console.Write(board[i + 1, j]);
                Player_Location();
            }
            zero_counter++;
        }

        // Game 
        static void Board()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (j == board.GetLength(1) - 1) Console.WriteLine(board[i, j]);
                    else
                    {
                        if (board[i, j] == '0')
                            Console.ForegroundColor = ConsoleColor.Red;
                        else if (board[i, j] == 'P')
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else if (row70 == i && column70 == j)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(board[i, j]);
                    }
                }
            }
            Console.SetCursorPosition(column70, row70); Console.ForegroundColor = ConsoleColor.White; Console.Write(board[row70, column70]);
        }
        static void Countdown()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] <= '9' && board[i, j] >= '2')
                    {
                        board[i, j]--;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(j, i);
                        Console.Write(board[i, j]);
                    }
                    if (board[i, j] == '1')
                    {
                        int one_to_zero = random.Next(0, 100);
                        if (one_to_zero < 3)
                        {
                            board[i, j] = '0';
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(j, i);
                            Console.Write(board[i, j]);
                        }
                    }
                }
            }
        }
        static void Pushing()
        {
            int number_counter;
            if (cki.Key == ConsoleKey.RightArrow)
            {
                bool flag = true;
                number_counter = 1;
                while (board[player_row, player_column + number_counter] >= 48 && board[player_row, player_column + number_counter] < 58)
                {
                    number_counter++;
                    if (board[player_row, player_column + (number_counter - 1)] > board[player_row, player_column + (number_counter - 2)])
                        flag = false;
                }
                if (flag == true)
                {
                    if (board[player_row, player_column + number_counter] == '#')
                        Smashing(number_counter);
                    else
                    {
                        while (number_counter >= 1)
                        {
                            if (board[player_row, player_column + number_counter] != '#')
                            {
                                board[player_row, player_column + number_counter] = board[player_row, player_column + (number_counter - 1)];
                                Console.SetCursorPosition(player_column + number_counter, player_row);
                                if (board[player_row, player_column + number_counter] == '0')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(board[player_row, player_column + number_counter]);
                                number_counter--;
                            }
                        }
                        Player_Right();
                    }
                }

            }   // Right          
            if (cki.Key == ConsoleKey.LeftArrow)
            {
                bool flag = true;
                number_counter = 1;
                while (board[player_row, player_column - number_counter] >= 48 && board[player_row, player_column - number_counter] < 58)
                {
                    number_counter++;
                    if (board[player_row, player_column - (number_counter - 1)] > board[player_row, player_column - (number_counter - 2)])
                        flag = false;
                }
                if (flag == true)
                {
                    if (board[player_row, player_column - number_counter] == '#')
                        Smashing(number_counter);
                    else
                    {
                        while (number_counter >= 1)
                        {
                            if (board[player_row, player_column - number_counter] != '#')
                            {
                                board[player_row, player_column - number_counter] = board[player_row, player_column - (number_counter - 1)];
                                Console.SetCursorPosition(player_column - number_counter, player_row);
                                if (board[player_row, player_column - number_counter] == '0')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(board[player_row, player_column - number_counter]);
                                number_counter--;
                            }
                        }
                        Player_Left();
                    }
                }
            }   // Left
            if (cki.Key == ConsoleKey.UpArrow)
            {
                bool flag = true;
                number_counter = 1;
                while (board[player_row - number_counter, player_column] >= 48 && board[player_row - number_counter, player_column] < 58)
                {
                    number_counter++;
                    if (board[player_row, player_column - (number_counter - 1)] > board[player_row, player_column - (number_counter - 2)])
                        flag = false;
                }
                if (flag == true)
                {
                    if (board[player_row - number_counter, player_column] == '#')
                        Smashing(number_counter);
                    else
                    {
                        while (number_counter >= 1)
                        {
                            if (board[player_row - number_counter, player_column] != '#')
                            {
                                board[player_row - number_counter, player_column] = board[player_row - (number_counter - 1), player_column];
                                Console.SetCursorPosition(player_column, player_row - number_counter);
                                if (board[player_row - number_counter, player_column] == '0')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(board[player_row - number_counter, player_column]);
                                number_counter--;
                            }
                        }
                        Player_Up();
                    }
                }
            }    // Up
            if (cki.Key == ConsoleKey.DownArrow)
            {
                bool flag = true;
                number_counter = 1;
                while (board[player_row + number_counter, player_column] >= 48 && board[player_row + number_counter, player_column] < 58)
                {

                    number_counter++;
                    if (board[player_row + (number_counter - 1), player_column] > board[player_row + (number_counter - 2), player_column])
                        flag = false;
                }
                if (flag == true)
                {
                    if (board[player_row + number_counter, player_column] == '#')
                        Smashing(number_counter);
                    else
                    {
                        while (number_counter >= 1)
                        {
                            if (board[player_row + number_counter, player_column] != '#')
                            {
                                board[player_row + number_counter, player_column] = board[player_row + (number_counter - 1), player_column];
                                Console.SetCursorPosition(player_column, player_row + number_counter);
                                if (board[player_row + number_counter, player_column] == '0')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(board[player_row + number_counter, player_column]);
                                number_counter--;
                            }
                        }
                        Player_Down();
                    }
                }
            } // Down
        }
        static void Smashing(int number_counter)
        {
            if (cki.Key == ConsoleKey.RightArrow)
            {
                if (number_counter > 2)
                {
                    Score(number_counter);
                    board[player_row, player_column + number_counter] = '#';
                    while (number_counter > 1)
                    {
                        number_counter--;
                        board[player_row, player_column + number_counter] = board[player_row, player_column + (number_counter - 1)];
                        Console.SetCursorPosition(player_column + number_counter, player_row);
                        if (board[player_row, player_column + number_counter] == '0')
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(board[player_row, player_column + number_counter]);
                        number--;
                        Numbers_Location();
                    }
                    Player_Right();
                }
            }   // Right
            if (cki.Key == ConsoleKey.LeftArrow)
            {
                if (number_counter > 2)
                {
                    Score(number_counter);
                    board[player_row, player_column - number_counter] = '#';
                    while (number_counter > 1)
                    {
                        number_counter--;
                        board[player_row, player_column - number_counter] = board[player_row, player_column - (number_counter - 1)];
                        Console.SetCursorPosition(player_column - number_counter, player_row);
                        if (board[player_row, player_column - number_counter] == '0')
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(board[player_row, player_column - number_counter]);
                        number--;
                        Numbers_Location();
                    }
                    Player_Left();
                }
            }   // Left
            if (cki.Key == ConsoleKey.UpArrow)
            {
                if (number_counter > 2)
                {
                    Score(number_counter);
                    board[player_row - number_counter, player_column] = '#';
                    while (number_counter > 1)
                    {
                        number_counter--;
                        board[player_row - number_counter, player_column] = board[player_row - (number_counter - 1), player_column];
                        Console.SetCursorPosition(player_column, player_row - number_counter);
                        if (board[player_row - number_counter, player_column] == '0')
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(board[player_row - number_counter, player_column]);
                        number--;
                        Numbers_Location();
                    }
                    Player_Up();
                }
            }    // Up
            if (cki.Key == ConsoleKey.DownArrow)
            {
                if (number_counter > 2)
                {
                    Score(number_counter);
                    board[player_row + number_counter, player_column] = '#';
                    while (number_counter > 1)
                    {
                        number_counter--;
                        board[player_row + number_counter, player_column] = board[player_row + (number_counter - 1), player_column];
                        Console.SetCursorPosition(player_column, player_row + number_counter);
                        if (board[player_row + number_counter, player_column] == '0')
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(board[player_row + number_counter, player_column]);
                        number--;
                        Numbers_Location();
                    }
                    Player_Down();
                }
            } // Down
            Board();
        }
        static void Score(int number_counter)
        {
            if (cki.Key == ConsoleKey.RightArrow)
            {
                if (board[player_row, player_column + (number_counter - 1)] == 48)
                    score += 20;
                if (board[player_row, player_column + (number_counter - 1)] <= 52 && board[player_row, player_column + (number_counter - 1)] >= 49)
                    score += 2;
                if (board[player_row, player_column + (number_counter - 1)] <= 57 && board[player_row, player_column + (number_counter - 1)] >= 53)
                    score += 1;
                Console.SetCursorPosition(63, 5);
                Console.WriteLine(score);
            }   // Right
            if (cki.Key == ConsoleKey.LeftArrow)
            {
                if (board[player_row, player_column - (number_counter - 1)] == 48)
                    score += 20;
                if (board[player_row, player_column - (number_counter - 1)] <= 52 && board[player_row, player_column - (number_counter - 1)] >= 49)
                    score += 2;
                if (board[player_row, player_column - (number_counter - 1)] <= 57 && board[player_row, player_column - (number_counter - 1)] >= 53)
                    score += 1;
                Console.SetCursorPosition(63, 5);
                Console.WriteLine(score);
            }   // Left
            if (cki.Key == ConsoleKey.UpArrow)
            {
                if (board[player_row - (number_counter - 1), player_column] == 48)
                    score += 20;
                if (board[player_row - (number_counter - 1), player_column] <= 52 && board[player_row - (number_counter - 1), player_column] >= 49)
                    score += 2;
                if (board[player_row - (number_counter - 1), player_column] <= 57 && board[player_row - (number_counter - 1), player_column] >= 53)
                    score += 1;
                Console.SetCursorPosition(63, 5);
                Console.WriteLine(score);
            }    // Up
            if (cki.Key == ConsoleKey.DownArrow)
            {
                if (board[player_row + (number_counter - 1), player_column] == 48)
                    score += 20;
                if (board[player_row + (number_counter - 1), player_column] <= 52 && board[player_row + (number_counter - 1), player_column] >= 49)
                    score += 2;
                if (board[player_row + (number_counter - 1), player_column] <= 57 && board[player_row + (number_counter - 1), player_column] >= 53)
                    score += 1;
                Console.SetCursorPosition(63, 5);
                Console.WriteLine(score);
            } // Down
        }
        static void End_Game()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.SetCursorPosition(10, 4); Console.WriteLine("Your Final Score: " + score);
            Console.SetCursorPosition(10, 6); Console.WriteLine("Press enter to close game.");
            Console.ReadLine();
        }

    }
}
