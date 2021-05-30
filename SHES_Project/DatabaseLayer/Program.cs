using Common;
using DatabaseLayer.DAO.Implementacije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Batteries b = new Batteries();
            Battery b1 = new Battery(0.8, "4", 200, Enums.BatteryRezim.PRAZNJENJE);
            Battery b2 = new Battery(0.9, "5", 250, Enums.BatteryRezim.PRAZNJENJE);
            List<Battery> list = new List<Battery>();
            list.Add(b1);
            list.Add(b2);
            
            Console.WriteLine(b.FindById("5").State);
            Console.WriteLine(b.FindById("4").State);
            Console.ReadKey();

            

        }
    }
}
