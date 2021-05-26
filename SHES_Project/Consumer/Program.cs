using Common;
using SHES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //zameniti ovaj broj nakon inicijalizacije sistema sa stvarnim brojem consumera
            Enums.ConsumerRezim[] rezim = new Enums.ConsumerRezim[5];

            Thread serverGUI = new Thread(ServerGUI);
            serverGUI.Start();

            ChannelFactory<ISHESConsumer> channel = new ChannelFactory<ISHESConsumer>("ISHESConsumer");
            ISHESConsumer proxy = channel.CreateChannel();

            //inicijalizacija se salje iz SHESa
            List<Common.Consumer> consumers = new List<Common.Consumer>() {
            new Common.Consumer("c1",50),
            new Common.Consumer("c2",100),
            new Common.Consumer("c3",200)
            };

            while (true)
            {
                rezim = ConsumerGUI.rezimBuffer;
                double total = 0;
                for (int i = 0; i < consumers.Count(); i++)
                {
                    if(rezim[i] == Enums.ConsumerRezim.ON)
                        total += consumers[i].EnergyConsumption;
                }
                proxy.sendEnergyConsumption(total);
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
    }
}
