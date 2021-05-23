using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SHES
{
    class Program
    {
        static void Main(string[] args)
        {
            double solarPanelsOutput = 0;




            Thread solarPanelServer = new Thread(SolarPanelServerThread);
            solarPanelServer.Start();

            while (true)
            {
               
                solarPanelsOutput = SHESSolarPanel.bufferPowerOutput;
                Console.WriteLine(solarPanelsOutput);

                Thread.Sleep(3000);
            }

        }
        static void SolarPanelServerThread()
        {

            using (ServiceHost host = new ServiceHost(typeof(SHESSolarPanel)))
            {

                host.Open();
                while (true) ;


            }
        }
    }

}
   

