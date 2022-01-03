using System;
using System.Collections.Generic;

namespace RobotMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            string mazeStr =
@"Z 0 0 0 0 0 0 0 0
0 1 1 1 1 1 0 0 0
1 0 0 0 0 0 0 1 1
0 0 0 0 1 0 0 1 1
1 1 1 1 1 1 0 1 1
1 1 1 1 1 0 0 0 0
0 0 K 0 0 0 0 0 0
";
            Maze maze = Maze.CreateFromString(mazeStr);

            MazeRenderer renderer = new MazeRenderer(maze);
            renderer.Print();
        }
    }

    public class Maze
    {
        public Maze(bool[,] field, Vector2 start, Vector2 end)
        {
            Field = field;
            Start = start;
            End = end;
            RobotLocation = Start;
        }

        public bool[,] Field { get; set; }
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }
        public Vector2 RobotLocation { get; set; }

        public static Maze CreateFromString(string mazeStr)
        {
            Vector2 start = new Vector2();
            Vector2 end = new Vector2();
            List<bool[]> rows = new List<bool[]>();
            List<bool> currRow = new List<bool>();
            foreach (char c in mazeStr)
            {
                if (c != ' ')
                {
                    switch (c)
                    {
                        case '0':
                            currRow.Add(false);
                            break;
                        case '1':
                            currRow.Add(true);
                            break;
                        case '\n':
                            rows.Add(currRow.ToArray());
                            currRow.Clear();
                            break;
                        case 'Z':
                            start = new Vector2(currRow.Count, rows.Count);
                            currRow.Add(false);
                            break;
                        case 'K':
                            end = new Vector2(currRow.Count, rows.Count);
                            currRow.Add(false);
                            break;
                    }
                }
            }
            bool[,] mazeArr = new bool[rows[0].Length, rows.Count];
            for (int y = 0; y < rows.Count; y++)
            {
                for (int x = 0; x < rows[0].Length; x++)
                {
                    mazeArr[x, y] = rows[y][x];
                }
            }

            return new Maze(mazeArr, start, end);
        }
    }

    public class MazeRenderer
    {
        public MazeRenderer(Maze maze)
        {
            Maze = maze;
        }

        public Maze Maze { get; set; }

        public void Print()
        {
            Console.Clear();
            for (int y = 0; y < Maze.Field.GetLength(1); y++)
            {
                for (int x = 0; x < Maze.Field.GetLength(0); x++)
                {
                    Console.SetCursorPosition(x * 2, y);
                    Console.Write(Maze.Field[x, y] ? 'X' : ' ');
                }
                Console.WriteLine();
            }

            Console.SetCursorPosition(Maze.Start.X * 2, Maze.Start.Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('S');
            Console.SetCursorPosition(Maze.End.X * 2, Maze.End.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('E');
            Console.SetCursorPosition(Maze.RobotLocation.X * 2, Maze.RobotLocation.Y);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write('R');
            Console.ResetColor();
        }
    }

    public struct Vector2
    {
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }


}
