using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace spel
{
    class Game
    {
        public static int WindowWidth { get; set; }
        public static int WindowHeight { get; set; }

        public static int Width { get; set; }
        public static int Height { get; set; }

        public static int Highscore { get; set; }
        public static int Score { get; set; }
        

        public Worm worm;
        public Food food;

        public Game()
        {

        }

        public void Init()
        {
            WindowWidth = 50;
            WindowHeight = 50;

            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.BufferHeight = WindowHeight;
            Console.BufferWidth = WindowWidth;
            Console.Clear();


            // Rita upp låda
            Width = 30;
            Height = 25;
            Console.BackgroundColor = ConsoleColor.Blue;

            for (var i = 0; i < Width; i++ )
            {
                Console.SetCursorPosition(i, Height);
                Console.WriteLine(" ");
                Console.SetCursorPosition(i, 0);
                Console.WriteLine(" ");
            }
            for (var i = 0; i <= Height; i++)
            {
                Console.SetCursorPosition(Width, i);
                Console.WriteLine(" ");
                Console.SetCursorPosition(0, i);
                Console.WriteLine(" ");
            }
            Console.ResetColor();

            Score = 0;

            UpdateScore();
            UpdateHighscore();
            worm = new Worm(this);

            food = new Food(this);

            Console.ReadKey(true);

        }

        public void Play()
        {
            while (!worm.Dead)
            {
                UpdateScore();
                food.PrintFood();
                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {

                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            worm.Move("left");
                            break;
                        case ConsoleKey.RightArrow:
                            worm.Move("right");
                            break;
                        case ConsoleKey.UpArrow:
                            worm.Move("up");
                            break;
                        case ConsoleKey.DownArrow:
                            worm.Move("down");
                            break;
                        default:
                            worm.Move();
                            break;
                    }

                }
                else
                {
                    worm.Move();
                }
            }
            Console.ReadKey();
        }

        public void AddNewFood()
        {
            food = new Food(this);
        }

        public void UpdateScore()
        {
            Console.SetCursorPosition(5, Height + 3);
            Console.WriteLine("Score: {0}", Score);
        }

        public void UpdateHighscore()
        {
            Console.SetCursorPosition(5, Height + 5);
            Console.WriteLine("Highscore: {0}", Highscore);
        }
    }
}
