using System;

namespace NoughtsAndCrosses
{
    class MainClass
    {
        static void Main(string[] args)
        {
            while (true)
            {
                char[,] grid = { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
                bool win = false;
                OutputGrid(grid);
                while (!win)
                {
                    int attempts = 0;
                    InputChar(ref grid, ref attempts);
                    win = CheckWin(grid, false);
                    if (win) { break; }
                    win = CheckFull(grid);
                    if (win) { break; }
                    ArtificialIntelligence(ref grid, ref attempts);
                    win = CheckWin(grid, true);
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.WriteLine("Press any key for a new game...");
                Console.ReadKey();
                Console.Clear();
            }



        }
        public static void OutputGrid(char[,] grid)
        {
            Console.WriteLine("   |(1)|(2)|(3)|");
            for (int a = 0; a < 3; a++)
            {
                Console.Write($"({a + 1})|");
                for (int b = 0; b < 3; b++)
                {
                    Console.Write($" {grid[a, b]} |");
                }
                Console.WriteLine();
            }
        }

        public static void InputChar(ref char[,] grid, ref int attempts)
        {
            int row = 0, col = 0;
            Console.WriteLine("Enter the row");
            row = ValidateInput() - 1;
            Console.WriteLine("Enter the column");
            col = ValidateInput() - 1;
            CheckEntry(ref grid, row, col, 'X', ref attempts);
        }
        public static int ValidateInput()
        {
            string testNum = Console.ReadLine();
            while (string.IsNullOrEmpty(testNum) || testNum.Length > 1 || (testNum.Length == 1 && Int32.TryParse(testNum, out int result) == false) || (testNum.Length == 1 && Int32.TryParse(testNum, out int resultt) == true && (Convert.ToInt32(testNum) < 1 || Convert.ToInt32(testNum) > 3)))
            {
                Console.WriteLine("Value not accepted. Try entering another value");
                testNum = Console.ReadLine();
            }
            return Convert.ToInt32(testNum);
        }
        public static void CheckEntry(ref char[,] grid, int row, int col, char player, ref int attempts)
        {
            if (grid[row, col] == ' ') { grid[row, col] = player; }
            else
            {
                if (attempts < 5 && player == 'X') { Console.WriteLine("Space is already taken"); attempts++; InputChar(ref grid, ref attempts); }
                else if (attempts > 9) { bool untaken = true; for (int a = 0; a < 3; a++) { for (int b = 0; b < 3; b++) { if (grid[a, b] == ' ' && untaken) { grid[a, b] = 'O'; untaken = false; break; } } } }
                else if (player == 'O') { attempts++; FillRandom(ref grid, ref attempts); }
                else { Console.WriteLine("Space is already taken"); Console.WriteLine("Too many attempts taken..."); attempts = 0; }
            }
        }
        public static bool CheckFull(char[,] grid)
        {
            foreach (char c in grid)
            {
                if (c == ' ') { return false; }
            }
            Console.WriteLine("Nobody wins");
            return true;
        }
        public static void FillRandom(ref char[,] grid, ref int attempts)
        {
            Random ran = new Random();
            int ranRow = ran.Next(0, 3);
            int ranCol = ran.Next(0, 3);
            CheckEntry(ref grid, ranRow, ranCol, 'O', ref attempts);
        }
        public static bool CheckWin(char[,] grid, bool output)
        {
            if (output) { OutputGrid(grid); }
            if (grid[0, 0] == grid[0, 1] && grid[0, 1] == grid[0, 2] && grid[0, 0] != ' ') { Console.WriteLine($"{grid[0, 0]} wins!"); return true; }
            else if (grid[1, 0] == grid[1, 1] && grid[1, 1] == grid[1, 2] && grid[1, 0] != ' ') { Console.WriteLine($"{grid[1, 0]} wins!"); return true; }
            else if (grid[2, 0] == grid[2, 1] && grid[2, 1] == grid[2, 2] && grid[2, 0] != ' ') { Console.WriteLine($"{grid[2, 0]} wins!"); return true; }
            else if (grid[0, 0] == grid[1, 0] && grid[1, 0] == grid[2, 0] && grid[0, 0] != ' ') { Console.WriteLine($"{grid[0, 0]} wins!"); return true; }
            else if (grid[0, 1] == grid[1, 1] && grid[1, 1] == grid[2, 1] && grid[0, 1] != ' ') { Console.WriteLine($"{grid[0, 1]} wins!"); return true; }
            else if (grid[0, 2] == grid[1, 2] && grid[1, 2] == grid[2, 2] && grid[0, 2] != ' ') { Console.WriteLine($"{grid[0, 2]} wins!"); return true; }
            else if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2] && grid[0, 0] != ' ') { Console.WriteLine($"{grid[0, 0]} wins!"); return true; }
            else if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0] && grid[0, 2] != ' ') { Console.WriteLine($"{grid[0, 2]} wins!"); return true; }
            return false;
        }

        public static void ArtificialIntelligence(ref char[,] g, ref int a)
        {
            //this gets very confusing. Proceed with caution.
            for (int pl = 1; pl < 3; pl++)
            {
                char player;
                if (pl == 1) { player = 'O'; }
                else { player = 'X'; }

                for (int s = 0; s < 3; s++)
                {
                    if (g[s, s] == player && g[s, s] == g[AddOne(s), AddOne(s)] && g[AddTwo(s), AddTwo(s)] == ' ') { g[AddTwo(s), AddTwo(s)] = 'O'; return; }
                    else if (g[Inverse(s), s] == player && g[Inverse(s), s] == g[AddOne(AddOne(Inverse(s))), AddOne(s)] && g[AddOne(Inverse(s)), AddTwo(s)] == ' ') { g[AddOne(Inverse(s)), AddTwo(s)] = 'O'; return; }
                    for (int z = 0; z < 3; z++)
                    {
                        if (g[s, z] == player && g[s, z] == g[AddOne(s), z] && g[AddTwo(s), z] == ' ') { g[AddTwo(s), z] = 'O'; return; }
                        else if (g[z, s] == player && g[z, s] == g[z, AddOne(s)] && g[z, AddTwo(s)] == ' ') { g[z, AddTwo(s)] = 'O'; return; }
                    }
                }
            }
            FillRandom(ref g, ref a);
        }
        public static int AddOne(int val)
        {
            val++;
            if (val > 2) { val -= 3; }
            return val;
        }
        public static int AddTwo(int val)
        {
            val = AddOne(AddOne(val));
            return val;
        }
        public static int Inverse(int val)
        {
            val = 3 - (val + 1);

            return val;
        }
    }
}
