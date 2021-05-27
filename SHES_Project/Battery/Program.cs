using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Common.Enums;

namespace Battery
{
    class Program
    {
        static void Main(string[] args)
        {

            Thread server = new Thread(Server);
            server.Start();

            ChannelFactory<ISHESBattery> channel = new ChannelFactory<ISHESBattery>("ISHESBattery");
            ISHESBattery proxy = channel.CreateChannel();


            int counter = 0;
            while (true)
            {
                foreach (Common.Battery b in BatterySHES.batteries) { 

                    b.State = BatterySHES.bufferRezim[b.Id];

                    if ( b.State == BatteryRezim.PUNJENJE && counter == 60)
                    {
                        b.Capacity+=0.01;
                        counter = 0;
                    }
                    else if (b.State == BatteryRezim.PRAZNJENJE && counter == 60)
                    {
                        b.Capacity -= 0.01;
                        counter = 0;
                    }
                    proxy.SendData(b.Id,b.Capacity,b.State);
                }
                counter++;
                Thread.Sleep(50);
            }
        }

        static void Server()
        {
            using (ServiceHost host = new ServiceHost(typeof(BatterySHES)))
            {
                host.Open();
                while (true) ;
            }
        }
    }
}
