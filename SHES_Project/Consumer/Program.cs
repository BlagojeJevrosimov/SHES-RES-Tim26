using Common;
using SHES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {

            ChannelFactory<ISHESConsumer> channel = new ChannelFactory<ISHESConsumer>("ISHESConsumer");
            ISHESConsumer proxy = channel.CreateChannel();
             List<Common.Consumer> consumers = new List<Common.Consumer>() {
            new Common.Consumer("c1",50),
            new Common.Consumer("c2",100),
            new Common.Consumer("c3",200)

             };
            double total = 0;
            foreach (var c in consumers) {

                total += c.EnergyConsumption;

            }
            proxy.sendEnergyConsumption(total);
            

        }
    }
}
