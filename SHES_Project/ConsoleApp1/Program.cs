using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            long a = new DateTime(2020,1,1).Ticks + (long)44841900*10000000;
            DateTime d = new DateTime(a);
            Console.WriteLine(d);
            Console.ReadKey();
        }
    }
}
