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
            Enums.BatteryRezim rezimSHES = Enums.BatteryRezim.IDLE;
            Enums.BatteryRezim rezimGUI = Enums.BatteryRezim.IDLE;
            bool plug = false;

            Thread server1 = new Thread(Server1);
            Thread server2 = new Thread(Server2);

            server1.Start();
            server2.Start();

            ChannelFactory<ISHESEVCharger> channelSHES = new ChannelFactory<ISHESEVCharger>("ISHESEVCharger");
            ISHESEVCharger proxySHES = channelSHES.CreateChannel();

            while (true)
            {
                rezimSHES = EVChargerSHES.rezimBuffer;
                rezimGUI = EVChargerGUI.rezimBuffer;

                Trace.TraceInformation("Sent from SHES: " + rezimSHES.ToString());
                Trace.TraceInformation("Sent from GUI: " + EVChargerGUI.rezimBuffer.ToString());

                //kreirati proxy ka SHESu da mu se posalje trazeni rezim rada sa GUI-ja
                //proveriti da li je promenjeno stanje i tek ako jeste poslati tu info shesu kao bool
                if (plug != EVChargerGUI.plugBuffer || rezimSHES != EVChargerGUI.rezimBuffer)
                {
                    plug = EVChargerGUI.plugBuffer;
                    if(rezimGUI == Enums.BatteryRezim.PRAZNJENJE)
                        proxySHES.SendRegime(plug, false);
                    else if (rezimGUI == Enums.BatteryRezim.PUNJENJE)
                        proxySHES.SendRegime(plug, true);
                }

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
