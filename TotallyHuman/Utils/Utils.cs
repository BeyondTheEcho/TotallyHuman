using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotallyHuman.Utils
{
    public static class Utils
    {
        static Random random = new Random();

        public static int RandomRange(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
