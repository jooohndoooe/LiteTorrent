using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteTorrent
{
    public class Utils
    {
        public static string ProgressBar(int progress, int size)
        {
            string bar = "";
            for (int i = 0; i < progress * size / 100; i++)
            {
                bar += "■";
            }
            for (int i = progress * size / 100; i < size; i++)
            {
                bar += "*";
            }
            return bar;
        }
    }
}
