using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace spel
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();

            while(true)
            {
                game.Init();

                game.Play();
            }
           
        }
    }
}
