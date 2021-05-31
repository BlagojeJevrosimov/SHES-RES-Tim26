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
            DatabaseLayer.DAO.Implementacije.Utility ut = new DAO.Implementacije.Utility();
            Common.Utility u = new Common.Utility(0.33);
            ut.Save(u);
            Console.WriteLine(ut.GetCurrentPrice());
            Console.ReadKey();

            

        }
    }
}
