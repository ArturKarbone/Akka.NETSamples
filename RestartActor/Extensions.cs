using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestartActor
{
    public static class Extensions
    {
        public static void Times( this int times, Action action)
        {
            for (int i = 0; i< times;i++)
            {
                action();
            }
        }
    }
}
