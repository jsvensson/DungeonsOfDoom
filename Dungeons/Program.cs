﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(3, 3);
            game.Start();
        }
    }
}
