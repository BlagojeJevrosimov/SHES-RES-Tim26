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
            double capacities = 0;
            Rezim rezimRada = Rezim.Idle;

            Thread server = new Thread(Server);
            server.Start();

            ChannelFactory<ISHESBattery> channel = new ChannelFactory<ISHESBattery>("ISHESBattery");
            ISHESBattery proxy = channel.CreateChannel();
            int counter = 0;
            while (true)
            {
                capacities = 0;
                rezimRada = Batteries.bufferRezim;

                foreach (Common.Battery battery in Batteries.batteries)
                {
                    capacities += battery.Capacity;

                    if (rezimRada == Rezim.Punjenje && battery.Capacity <= battery.MaxPower - 1 && counter == 60)
                    {
                        battery.Capacity++;
                        counter = 0;
                    }
                    else if (rezimRada == Rezim.Praznjenje && battery.Capacity >= 1 && counter == 60)
                    {
                        battery.Capacity--;
                        counter = 0;
                    }
                    proxy.SendData(capacities, rezimRada);
                }
                Console.WriteLine("Kapacitet baterija: " + capacities);
                Console.WriteLine("Rezim rada baterija: " + rezimRada.ToString());
                counter++;
                Thread.Sleep(3000);
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
