using System;

namespace Dungeons
{
    static class Randomizer
    {
        static Random random = new Random();

        public static bool Percentage(int chance)
        {
            return random.Next(101) <= chance;
        }

        public static bool Percentage(int chance, out int result)
        {
            result = random.Next(101);
            return result <= chance;
        }

        public static int Number(int max)
        {
            return random.Next(max + 1);
        }

        public static int Number(int min, int max)
        {
            return random.Next(min, max + 1);
        }

    }
}
