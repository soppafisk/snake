using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spel
{
    class Food
    {
        public int X { get; set; }
        public int Y { get; set; }

        private Game Game;
        public Food(Game game)
        {
            Game = game;
            var random = new Random();

            X = random.Next(1 , Game.Width);
            Y = random.Next(1, Game.Height);
            Console.SetCursorPosition(X, Y);
            Console.Write("O");
        }

        public void PrintFood()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("O");
        }
    }
}
