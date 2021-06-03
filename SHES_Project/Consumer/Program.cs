using Common;
using SHES;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {

            Thread serverGUI = new Thread(ServerGUI);
            serverGUI.Start();

            Thread serverSHES = new Thread(ServerSHES);
            serverSHES.Start();

            ChannelFactory<ISHESConsumer> channel = new ChannelFactory<ISHESConsumer>("ISHESConsumer");
            ISHESConsumer proxy = channel.CreateChannel();

            while (true)
            {
                if(ConsumerGUI.changed == true)
                {
                    int i = 0;
                    foreach (var c in ConsumerSHES.consumersList)
                    {
                        c.Rezim = ConsumerGUI.rezimBuffer[i];
                        i++;
                    }
                    proxy.sendEnergyConsumption(ConsumerGUI.total,ConsumerSHES.consumersList);
                    ConsumerGUI.changed = false;
                }
            }
        }

        public static void ServerGUI()
        {
            using (ServiceHost host = new ServiceHost(typeof(ConsumerGUI)))
            {
                host.Open();
                while (true) ;
            }
        }

        public static void ServerSHES()
        {
            using (ServiceHost host = new ServiceHost(typeof(ConsumerSHES)))
            {
                host.Open();
                while (true) ;
            }
        }
    }
}
