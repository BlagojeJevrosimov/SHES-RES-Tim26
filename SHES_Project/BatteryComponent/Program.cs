using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Common.Enums;

namespace BatteryComponent
{
    class Program
    {
        static void Main(string[] args)
        {
            //preuzeti vrednosti iz bafera iz baterije i dodeliti pravim poljima
            Thread server = new Thread(Server);
            server.Start();

            ChannelFactory<ISHESBattery> channel = new ChannelFactory<ISHESBattery>("IBatteryClient");
            ISHESBattery proxy = channel.CreateChannel();

            while (true)
            {
                Batteries.rezimRada = Batteries.bufferRezim;
                double[] capacities = { };
                int i = 0;
                foreach (Common.Battery battery in Batteries.batteries)
                {
                    capacities[i++] = battery.Capacity;
                    //da li svaka baterija ima drugaciji rezim ???
                    //napravljeno tako da se za sve salju odjednom podaci
                    //baterije se pune ili prazne
                    if (Batteries.rezimRada == Rezim.Punjenje && battery.Capacity <= battery.MaxPower - 1)
                    {
                        battery.Capacity++;
                    }
                    else if (Batteries.rezimRada == Rezim.Praznjenje && battery.Capacity >= 1)
                    {
                        battery.Capacity--;
                    }
                    proxy.SendData(capacities, Batteries.rezimRada);
                }
                Thread.Sleep(1000);
            }
        }

        static void Server()
        {
            using (ServiceHost host = new ServiceHost(typeof(Batteries)))
            {
                host.Open();
                while (true) ;
            }
        }
    }
}
