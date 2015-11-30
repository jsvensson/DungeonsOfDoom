using System;

namespace Dungeons.Utils
{
    public static class StringUtils
    {
        static public void WriteCenteredLine(string value)
        {
            int width = Console.WindowWidth;
            string padding = new string(' ', width / 2);
            Console.WriteLine(padding + value);
        }

        static public string ToUpperFirst(string value)
        {
            char[] letters = value.ToCharArray();
            letters[0] = letters[0].ToString().ToUpper()[0];
            return new string(letters);
        }
    }
}
