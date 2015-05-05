using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spel
{
    class Worm
    {
        public string Direction { get; set; }
        private Game Game;
        public bool Dead { get; set; }

        public List<Point> WormArray = new List<Point>();
        public Worm(Game game)
        {
            Game = game;
            Direction = "up";
            for (var i = 0; i < 5; i++)
            {
                WormArray.Add(new Point(15, 15 + i));
            }

            PrintWorm();
        }

        public void PrintWorm()
        {
            foreach (Point part in WormArray)
            {
                Console.SetCursorPosition(part.X, part.Y);
                Console.Write("X");
            }
        }

        public void Move(string direction = "continue")
        {
            // kolla så man inte går bakåt
            var opposite = "";
            switch (Direction)
            {
                case "left":
                    opposite = "right";
                    break;
                case "right":
                    opposite = "left";
                    break;
                case "up":
                    opposite = "down";
                    break;
                case "down":
                    opposite = "up";
                    break;
                case "continue":
                default:
                    break;
            }
           
            if (direction != "continue" && direction != opposite)
                Direction = direction;

            try
            {
                // Skriv över sista punkten och ta bort ur array
                var lastPoint = WormArray[WormArray.Count - 1];
                Console.SetCursorPosition(lastPoint.X, lastPoint.Y);
                Console.Write(" ");
                WormArray.RemoveAt(WormArray.Count - 1);
                
                var firstPoint = WormArray[0];

                switch (Direction)
                {
                    case "left":
                        lastPoint.X = firstPoint.X - 1;
                        lastPoint.Y = firstPoint.Y;
                        break;
                    case "right":
                        lastPoint.X = firstPoint.X + 1;
                        lastPoint.Y = firstPoint.Y;
                        break;
                    case "up":
                        lastPoint.X = firstPoint.X;
                        lastPoint.Y = firstPoint.Y - 1;
                        break;
                    case "down":
                        lastPoint.X = firstPoint.X;
                        lastPoint.Y = firstPoint.Y + 1;
                        break;
                    case "continue":
                    default:
                        lastPoint.X = firstPoint.X;
                        lastPoint.Y = firstPoint.Y;
                        break;
                }

                WormArray.Insert(0, lastPoint);
                firstPoint = WormArray[0];
                PrintWorm();

                // Kolla kanterna
                if (firstPoint.Y >= Game.Height || firstPoint.Y <= 0)
                {
                    Die();
                }
                if (firstPoint.X >= Game.Width || firstPoint.X <= 0)
                {
                    Die();
                }

                // kolla egen hitbox
                foreach (var point in WormArray)
                {
                    if (WormArray.IndexOf(point) != 0)
                    {
                        if (firstPoint.X == point.X && firstPoint.Y == point.Y)
                        {
                            Die();
                        }
                    }
                }

                // kolla om den äter
                if (firstPoint.X == Game.food.X && firstPoint.Y == Game.food.Y)
                {
                    Game.Score += 1;
                    WormArray.Add(new Point(Game.food.X, Game.food.Y));
                    Game.AddNewFood();
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                Die();
            }
        }

        public void Die()
        {

            Game.PressEnter("Du dog. Enter för att spela igen");
            Dead = true;

            if (Game.Score > Game.Highscore)
            {
                Game.Highscore = Game.Score;
            }
        }
    }
}
