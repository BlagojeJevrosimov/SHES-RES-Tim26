using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Common.Enums;

namespace Battery
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {

            Thread server = new Thread(Server);
            server.Start();

            ChannelFactory<ISHESBattery> channel = new ChannelFactory<ISHESBattery>("ISHESBattery");
            ISHESBattery proxy = channel.CreateChannel();

            
            while (true)
            {
                foreach (Common.Battery b in BatterySHES.batteries) { 

                    b.State = BatterySHES.bufferRezim[b.Id];

                    if ( b.State == BatteryRezim.PUNJENJE && b.Capacity <= 0.99 )
                    {
                        b.Capacity+=0.01;
                        
                    }
                    else if (b.State == BatteryRezim.PRAZNJENJE && b.Capacity >= 1)
                    {
                        b.Capacity -= 0.01;
                        
                    }
                    try { proxy.SendData(b.Id, b.Capacity, b.State); }
                    catch (Exception e) {
                        break;
                    }
                }
                
                Thread.Sleep(300);
            }
        }

        [ExcludeFromCodeCoverage]
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
