using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    class StatusQueue
    {
        const string interstitial = "--more--";
        static List<string> queue = new List<string>();
        int row, width;
        bool clearStatus;

        public StatusQueue(int row, int width)
        {
            this.row = row;
            this.width = width;
            clearStatus = false;
        }

        public void Add(string message)
        {
            queue.Add(message);
        }

        public void Write()
        {
            Console.SetCursorPosition(0, row);
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            if (queue.Count > 1)
            {
                Console.CursorVisible = true;
                int i = 0;
                foreach (string status in queue)
                {
                    i++;
                    string message = status;
                    if (i < queue.Count)
                        message = status + "--more--";
                    Console.Write(PadMessage(message));
                    Console.ReadKey(true);
                    clearStatus = true;
                    queue.Clear();
                }
                Console.CursorVisible = false;
            }
            else if (queue.Count == 1)
            {
                Console.Write(PadMessage(queue.First()));
                clearStatus = true;
                queue.Clear();
            }
            else if (queue.Count == 0)
            {
                if (clearStatus)
                    ClearStatus();
            }

        }

        string PadMessage(string value)
        {
            string padding = new string(' ', width - value.Length);
            return value + padding;
        }

        void ClearStatus()
        {
            string row = new string(' ', width - 1);
            Console.Write(row);
            clearStatus = false;
        }
    }
}
