using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Enums.BatteryRezim rezim;

            Thread server1 = new Thread(Server1);
            Thread server2 = new Thread(Server2);

            server1.Start();
            server2.Start();

            while (true)
            {
                rezim = EVChargerSHES.rezimBuffer;
                Trace.TraceInformation("Sent from SHES: " + rezim.ToString());

                //kreirati proxy ka SHESu da mu se posalje trazeni rezim rada sa GUI-ja
                Trace.TraceInformation("Sent from GUI: " + EVChargerGUI.rezimBuffer.ToString());
                Thread.Sleep(1000);
            }
            
        }

        static void Server1()
        {
            using (ServiceHost host = new ServiceHost(typeof(EVChargerSHES)))
            {
                host.Open();
                while (true) ;
            }
        }

        static void Server2()
        {
            using (ServiceHost host = new ServiceHost(typeof(EVChargerGUI)))
            {
                host.Open();
                while (true) ;
            }
        }
    }
}
