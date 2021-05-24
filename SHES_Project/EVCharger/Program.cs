using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EVCharger
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ServiceHost host = new ServiceHost(typeof(EVChargerSHES))) {

                host.Open();

                while (true) {
                    Enums.Rezim rezim = EVChargerSHES.rezimBuffer;
                    Console.WriteLine(rezim);
                    Thread.Sleep(1000);
                }

            }
        }
    }
}
